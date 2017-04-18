using System.Collections.Generic;
using System.IO;
using WeekendRoguelike.Mob.IO;

namespace WeekendRoguelike.Mob.Character
{
    public static class AllRaces
    {
        #region Private Fields

        private static Dictionary<string, Race> racesLookup =
            new Dictionary<string, Race>();

        #endregion Private Fields

        #region Public Methods

        public static IEnumerable<KeyValuePair<string, Race>> GetAllNameRacePairs()
        {
            foreach (var kvp in racesLookup)
                yield return kvp;
        }

        public static Race GetRace(string name)
        {
            return racesLookup[name.ToUpper()];
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

        #endregion Public Methods
    }
}
