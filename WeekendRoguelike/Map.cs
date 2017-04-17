using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public class Map
    {
        #region Private Fields

        private HashSet<Character> allCharacters =
            new HashSet<Character>();

        #endregion Private Fields

        #region Public Methods

        public bool AddCharacter(Character newCharacter)
        {
            return allCharacters.Add(newCharacter);
        }

        public IEnumerable<Character> AllCharacters()
        {
            foreach (var character in allCharacters)
            {
                yield return character;
            }
        }

        public bool RemoveCharacter(Character removeCharacter)
        {
            return allCharacters.Remove(removeCharacter);
        }

        public bool TryGetCharacterAt(Point at, out Character characterAt)
        {
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

        public bool TryMove(Character moveCharacter, Point to)
        {
            foreach (var otherCharacter in allCharacters)
            {
                if (moveCharacter.Position == otherCharacter.Position &&
                    moveCharacter != otherCharacter)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion Public Methods
    }
}
