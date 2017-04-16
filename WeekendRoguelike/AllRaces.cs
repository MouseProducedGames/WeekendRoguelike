using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public static class AllRaces
    {
        private static Dictionary<string, Race> racesLookup =
            new Dictionary<string, Race>();

        public static Dictionary<string, Race>.Enumerator GetEnumerator()
        {
            return racesLookup.GetEnumerator();
        }

        public static void LoadRaces(string filename)
        {
            LoadRaces(new RaceReader(File.OpenRead(filename)));
        }

        public static void LoadRaces(IRaceReader reader)
        {
            while (reader.EndOfSet == false)
            {
                if (reader.TryReadNextRace(out var nextRace))
                {
                    racesLookup[nextRace.Name.ToUpper()] = nextRace;
                }
            }
        }

        public static Race GetRace(string name)
        {
            return racesLookup[name.ToUpper()];
        }
    }
}