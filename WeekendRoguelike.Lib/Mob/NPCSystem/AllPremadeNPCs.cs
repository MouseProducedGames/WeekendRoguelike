using System.Collections.Generic;
using System.IO;
using WeekendRoguelike.Mob.IO;

namespace WeekendRoguelike.Mob.NPCSystem
{
    public static class AllPremadeNPCs
    {
        #region Private Fields

        private static Dictionary<string, PremadeNPCData> premadeNPCLookup =
            new Dictionary<string, PremadeNPCData>();

        #endregion Private Fields

        #region Public Methods

        public static IEnumerable<KeyValuePair<string, PremadeNPCData>> GetAllNamePremadeNPCPairs()
        {
            foreach (var kvp in premadeNPCLookup)
                yield return kvp;
        }

        public static PremadeNPCData GetPremadeNPC(string name)
        {
            return premadeNPCLookup[name.ToUpper()];
        }

        public static void LoadPremadeNPCs(string filename)
        {
            LoadPremadeNPCs(new PremadeNPCReader(File.OpenRead(filename)));
        }

        public static void LoadPremadeNPCs(IPremadeNPCReader reader)
        {
            while (reader.EndOfSet == false)
            {
                if (reader.TryReadNextPremadeNPC(out var nextPremadeNPC))
                {
                    premadeNPCLookup[nextPremadeNPC.Name.ToUpper()] = nextPremadeNPC;
                }
            }
        }

        #endregion Public Methods
    }
}
