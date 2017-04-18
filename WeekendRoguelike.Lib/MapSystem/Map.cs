using System;
using System.Collections.Generic;
using WeekendRoguelike.AI.PlanningSystem;
using WeekendRoguelike.Mob;
using WeekendRoguelike.Mob.Character;

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
        private FindOnMap finder;
        private HashSet<CharacterEntity> removeCharacters = new HashSet<CharacterEntity>();

        #endregion Private Fields

        #region Public Constructors

        public Map(int width, int length)
        {
            Width = width;
            Length = length;
            finder = new FindOnMap(this);
        }

        #endregion Public Constructors

        #region Public Methods

        public bool AddCharacter(CharacterEntity newCharacter)
        {
            return addCharacters.Add(newCharacter);
        }

        public IEnumerable<IMob> AllMobs()
        {
            return (IReadOnlyCollection<CharacterEntity>)allCharacters;
        }

        public void Draw()
        {
            foreach (var character in allCharacters)
            {
                character.Draw();
            }
        }

        public Queue<Point> GetPath(Point start, Point end)
        {
            return finder.GetPath(start, end);
        }

        public Point GetRandomValidPoint(CharacterEntity forCharacter)
        {
            Point output;
            while (Occupied(output = Rand.NextPoint(Width, Length), out var occupant) == true ||
                PointInMap(output) == false) ;
            return output;
        }

        public Point GetSingleStep(Point start, Point end)
        {
            return finder.GetSingleStep(start, end);
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
