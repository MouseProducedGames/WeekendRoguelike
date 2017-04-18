using System;
using System.Collections.Generic;
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
        private HashSet<CharacterEntity> removeCharacters = new HashSet<CharacterEntity>();
        private Tile[,] tileMap;
        private bool tileMapChanged = false;

        #endregion Private Fields

        #region Public Constructors

        public Map(int width, int length)
        {
            Width = width;
            Length = length;
            tileMap = new Tile[length, width];
            changeMap = new bool[length, width];
            mapGraphics = Display.Instance.CreateGraphicsWrapper(this);
        }

        public Map(Tile[,] tiles)
        {
            Width = tiles.GetLength(1);
            Length = tiles.GetLength(0);
            changeMap = new bool[Length, Width];
            TileMap = tiles;
        }

        #endregion Public Constructors

        #region Public Properties

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

        /// <summary>
        /// Checks if the tile blocks any movement.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Blocked(Point p)
        {
            return AllTileData.GetTileData(this[p.X, p.Y].ID).BlocksMovement
                != BlockDirections.None;
        }

        public bool Changed(int x, int y)
        {
            return changeMap[y, x];
        }

        public void Draw()
        {
            mapGraphics.Update(this);
            mapGraphics.Draw();
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
                character.Draw();
            }
        }

        public Point GetRandomValidPoint(CharacterEntity forCharacter)
        {
            Point output;
            while (Occupied(output = Rand.NextPoint(Width, Length), out var occupant) == true ||
                Blocked(output) == true ||
                PointInMap(output) == false) ;
            return output;
        }

        public Point GetSingleStep(Point start, Point end)
        {
            Displacement disp = end - start;
            return
                start + new Displacement(Math.Sign(disp.X), Math.Sign(disp.Y));
        }

        public TileData GetTileDataFor(Point to)
        {
            return AllTileData.GetTileData(this[to.X, to.Y].ID);
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
            return
                to.X >= 0 && to.X < Width &&
                to.Y >= 0 && to.Y < Length
                ;
        }

        public bool RemoveCharacter(CharacterEntity removeCharacter)
        {
            return removeCharacters.Add(removeCharacter);
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
            Displacement disp = to - moveCharacter.Position;

            // Not sure that's a move, but ok. Should TryMove be called if you
            // stay in the same position? Hmm...
            if (disp.X == 0 && disp.Y == 0)
                return true;

            // If it were > 2, then that would be a teleport. In which case, the
            // TileData restrictions on movement wouldn't matter.
            if (disp.MagnitudeSquared <= 2)
            {
                // Very simple check.
                if (Blocked(to))
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

                        Point linear = moveCharacter.Position +
                            new Displacement(0, disp.Y);
                        Point sideways = moveCharacter.Position +
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
