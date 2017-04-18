using System.Collections.Generic;
using System.IO;
using WeekendRoguelike.AI.FactionSystem.IO;

namespace WeekendRoguelike.AI.FactionSystem
{
    public static class AllFactions
    {
        #region Private Fields

        private static Dictionary<string, Faction> factionsLookup =
            new Dictionary<string, Faction>();

        #endregion Private Fields

        #region Public Methods

        public static IEnumerable<KeyValuePair<string, Faction>> GetAllNameFactionPairs()
        {
            foreach (var kvp in factionsLookup)
                yield return kvp;
        }

        public static Faction GetCharacterClass(string name)
        {
            return factionsLookup[name.ToUpper()];
        }

        public static void LoadFactions(string filename)
        {
            LoadFactions(new FactionReader(File.OpenRead(filename)));
        }

        public static void LoadFactions(IFactionReader reader)
        {
            while (reader.EndOfSet == false)
            {
                if (reader.TryReadNextFaction(out var nextClass))
                {
                    factionsLookup[nextClass.Name.ToUpper()] = nextClass;
                }
            }
        }

        #endregion Public Methods
    }
}
