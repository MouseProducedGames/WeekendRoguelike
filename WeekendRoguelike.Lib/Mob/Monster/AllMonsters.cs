using System.Collections.Generic;
using System.IO;
using WeekendRoguelike.Mob.IO;

namespace WeekendRoguelike.Mob.Monster
{
    public static class AllMonsters
    {
        #region Private Fields

        private static Dictionary<string, MonsterData> monstersLookup =
            new Dictionary<string, MonsterData>();

        #endregion Private Fields

        #region Public Methods

        public static IEnumerable<KeyValuePair<string, MonsterData>> GetAllNameMonsterPairs()
        {
            foreach (var kvp in monstersLookup)
                yield return kvp;
        }

        public static MonsterData GetMonster(string name)
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
