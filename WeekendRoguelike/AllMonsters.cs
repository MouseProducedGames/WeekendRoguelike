using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public static class AllMonsters
    {
        private static Dictionary<string, Monster> monstersLookup =
            new Dictionary<string, Monster>();

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
    }
}