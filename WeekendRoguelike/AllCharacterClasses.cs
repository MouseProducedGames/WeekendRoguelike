using System.Collections.Generic;
using System.IO;

namespace WeekendRoguelike
{
    public static class AllCharacterClasses
    {
        #region Private Fields

        private static Dictionary<string, CharacterClass> classesLookup =
            new Dictionary<string, CharacterClass>();

        #endregion Private Fields

        #region Public Methods

        public static CharacterClass GetCharacterClass(string name)
        {
            return classesLookup[name.ToUpper()];
        }

        public static Dictionary<string, CharacterClass>.Enumerator GetEnumerator()
        {
            return classesLookup.GetEnumerator();
        }

        public static void LoadClasses(string filename)
        {
            LoadClasses(new CharacterClassReader(File.OpenRead(filename)));
        }

        public static void LoadClasses(ICharacterClassReader reader)
        {
            while (reader.EndOfSet == false)
            {
                if (reader.TryReadNextClass(out var nextClass))
                {
                    classesLookup[nextClass.Name.ToUpper()] = nextClass;
                }
            }
        }

        #endregion Public Methods
    }
}