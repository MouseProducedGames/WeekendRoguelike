using System.Collections.Generic;
using System.IO;

namespace WeekendRoguelike
{
    public static class AllMonsters
    {
        #region Private Fields

        private static Dictionary<string, Monster> monstersLookup =
            new Dictionary<string, Monster>();

        #endregion Private Fields

        #region Public Methods

        public static Dictionary<string, Monster>.Enumerator GetEnumerator()
        {
            return monstersLookup.GetEnumerator();
        }

        public static Monster GetMonster(string name)
        {
            return monstersLookup[name.ToUpper()];
        }

        public static void LoadMonsters(string filename)
        {
            LoadMonsters(new MonsterReader(File.OpenRead(filename)));
        }

        public static void LoadMonsters(IMonsterReader reader)
        {
            while (reader.EndOfSet == false)
            {
                if (reader.TryReadNextMonster(out var nextMonster))
                {
                    monstersLookup[nextMonster.Name.ToUpper()] = nextMonster;
                }
            }
        }

        #endregion Public Methods
    }
}