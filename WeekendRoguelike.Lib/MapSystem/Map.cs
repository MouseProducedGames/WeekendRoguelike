using System;
using System.Collections;
using System.Collections.Generic;
using WeekendRoguelike.AI.PlanningSystem;
using WeekendRoguelike.Mob;
using WeekendRoguelike.Mob.Character;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.MapSystem
{
    public class Map : IMobCollection
    {
        #region Public Fields

        public readonly int Length;

        public readonly int Width;

        #endregion Public Fields

        #region Private Fields

        private HashSet<CharacterEntity> addCharacters = new HashSet<CharacterEntity>();
        private HashSet<CharacterEntity> allCharacters = new HashSet<CharacterEntity>();
        private bool[,] changeMap;
        private Display.IMapGraphicsWrapper mapGraphics;
        private PathFindOnMap pathFinder;
        private HashSet<CharacterEntity> removeCharacters = new HashSet<CharacterEntity>();
        private Tile[,] tileMap;
        private bool tileMapChanged = false;

        #endregion Private Fields

        #region Public Constructors

        public Map(int width, int length)
        {
            Width = width;
            Length = length;
            changeMap = new bool[length, width];
            TileMap = new Tile[length, width];
        }

        public Map(Tile[,] tiles)
        {
            Width = tiles.GetLength(1);
            Length = tiles.GetLength(0);
            changeMap = new bool[Length, Width];
            TileMap = tiles;
            pathFinder = new PathFindOnMap(this);
        }

        #endregion Public Constructors

        #region Public Properties

        int IReadOnlyCollection<IMob>.Count => throw new NotImplementedException();

        public Tile[,] TileMap
        {
            get => tileMap;
            set
            {
                tileMap = value;
                mapGraphics = Display.Instance.CreateGraphicsWrapper(this);
            }
        }

        public bool TileMapChanged { get => tileMapChanged; }

        #endregion Public Properties

        #region Public Indexers

        public Tile this[int x, int y]
        {
            get
            {
                return tileMap[y, x];
            }
            set
            {
                if (tileMap[y, x] != value)
                {
                    tileMapChanged = true;
                    tileMap[y, x] = value;
                    changeMap[y, x] = true;
                }
            }
        }

        #endregion Public Indexers

        #region Public Methods

        public bool AddCharacter(CharacterEntity newCharacter)
        {
            return addCharacters.Add(newCharacter);
        }

        public IEnumerable<IMob> AllMobs()
        {
            foreach (var character in allCharacters)
            {
                yield return character;
            }
        }

        public bool Changed(int x, int y)
        {
            return changeMap[y, x];
        }

        public void Draw(CharacterEntity viewpointCharacter)
        {
            mapGraphics.Update(this);
            mapGraphics.Draw(viewpointCharacter);
            if (TileMapChanged == true)
            {
                tileMapChanged = false;
                for (int y = 0; y < Length; ++y)
                {
                    for (int x = 0; x < Width; ++x)
                    {
                        changeMap[y, x] = false;
                    }
                }
            }

            foreach (var character in allCharacters)
            {
                character.Draw(viewpointCharacter);
            }
        }

        IEnumerator<IMob> IEnumerable<IMob>.GetEnumerator()
        {
            return allCharacters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return allCharacters.GetEnumerator();
        }

        public Stack<Point> GetPath(Point start, Point end)
        {
            return pathFinder.GetPath(start, end);
        }

        public Point GetRandomValidPoint(IMob forCharacter)
        {
            Point output;
            while (Occupied(output = Rand.NextPoint(Width, Length), out var occupant) == true ||
                MovementBlocked(output) == true ||
                PointInMap(output) == false) ;
            return output;
        }

        public bool TryGetSingleStep(Point start, Point end, out Point step)
        {
            return pathFinder.TryGetSingleStep(start, end, out step);
        }

        public TileData GetTileDataFor(Point to)
        {
            return AllTileData.GetTileData(this[to.X, to.Y].ID);
        }

        /// <summary>
        /// Checks if the tile blocks any movement.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool MovementBlocked(Point p)
        {
            return AllTileData.GetTileData(this[p.X, p.Y].ID).BlocksMovement
                != BlockDirections.None;
        }

        public bool Occupied(Point position, out CharacterEntity characterAt)
        {
            foreach (var otherCharacter in allCharacters)
            {
                if (position == otherCharacter.Position)
                {
                    characterAt = otherCharacter;
                    return true;
                }
            }
            characterAt = null;
            return false;
        }

        public bool PointInMap(Point to)
        {
            return PointInMap(to.X, to.Y);
        }

        public bool PointInMap(int x, int y)
        {
            return
                x >= 0 && x < Width &&
                y >= 0 && y < Length;
        }

        public bool RemoveCharacter(CharacterEntity removeCharacter)
        {
            return removeCharacters.Add(removeCharacter);
        }

        /// <summary>
        /// Checks if the tile blocks any sight.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool SightBlocked(Point p)
        {
            return SightBlocked(p.X, p.Y);
        }

        /// <summary>
        /// Checks if the tile blocks any sight.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool SightBlocked(int x, int y)
        {
            return 
                PointInMap(x, y) == false ||
                AllTileData.GetTileData(this[x, y].ID).BlocksSight
                != BlockDirections.None;
        }

        public bool TestMove(Point start, Point to)
        {
            Displacement disp = to - start;

            // Not sure that's a move, but ok. Should TryMove be called if you
            // stay in the same position? Hmm...
            if (disp.X == 0 && disp.Y == 0)
                return true;

            // If it were > 2, then that would be a teleport. In which case, the
            // TileData restrictions on movement wouldn't matter.
            if (disp.MagnitudeSquared <= 2)
            {
                // Very simple check.
                if (MovementBlocked(to))
                    return false;

                TileData tileData = GetTileDataFor(to);
                if (tileData.BlocksMovement != BlockDirections.None)
                {
                    BlockDirections movingDir;
                    // It has to be > 0, because we filtered that when we checked
                    // that we were moving at all. It has to be either a
                    // diagnonal or cardinal, as one is MS 2 and the other is MS
                    // 1.
                    if (disp.MagnitudeSquared == 2)
                    {
                        movingDir = BlockDirections.Diagonal;

                        Point linear = start +
                            new Displacement(0, disp.Y);
                        Point sideways = start +
                            new Displacement(disp.X, 0);
                        // "to" is be definition one tile away.
                        if (GetTileDataFor(linear)
                            .BlocksMovement.MatchAll(movingDir) &&
                            GetTileDataFor(sideways)
                            .BlocksMovement.MatchAll(movingDir))
                            return false;
                    }
                    else
                        movingDir = BlockDirections.Cardinal;

                    if (tileData.BlocksMovement == movingDir)
                        return false;
                }
            }

            if (PointInMap(to) == false)
                return false;
            foreach (var otherCharacter in allCharacters)
            {
                if (to == otherCharacter.Position)
                {
                    return false;
                }
            }
            return true;
        }

        public bool TryGetCharacterAt(Point at, out CharacterEntity characterAt)
        {
            if (PointInMap(at) == false)
            {
                characterAt = null;
                return false;
            }

            foreach (var otherCharacter in allCharacters)
            {
                if (at == otherCharacter.Position)
                {
                    characterAt = otherCharacter;
                    return true;
                }
            }
            characterAt = null;
            return false;
        }

        public bool TryMove(CharacterEntity moveCharacter, Point to)
        {
            Point start = moveCharacter.Position;
            if (TestMove(start, to) == false)
                return false;
            foreach (var otherCharacter in allCharacters)
            {
                if (to == otherCharacter.Position &&
                    moveCharacter != otherCharacter)
                {
                    return false;
                }
            }
            return true;
        }

        public void Update()
        {
            foreach (var removeCharacter in removeCharacters)
                allCharacters.Remove(removeCharacter);
            removeCharacters.Clear();
            foreach (var addCharacter in addCharacters)
                allCharacters.Add(addCharacter);
            addCharacters.Clear();

            foreach (var character in allCharacters)
            {
                character.Update();
            }
        }

        #endregion Public Methods
    }
}
