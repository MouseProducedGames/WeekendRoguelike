using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public static class AllCharacterClasses
    {
        private static Dictionary<string, CharacterClass> classesLookup =
            new Dictionary<string, CharacterClass>();

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

        public static CharacterClass GetCharacterClass(string name)
        {
            return classesLookup[name.ToUpper()];
        }
    }
}