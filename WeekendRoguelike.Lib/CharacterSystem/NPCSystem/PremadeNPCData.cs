using System.Collections.Generic;
using System.Linq;
using WeekendRoguelike.AI.FactionSystem;
using WeekendRoguelike.CharacterSystem.Base;

namespace WeekendRoguelike.CharacterSystem.NPCSystem
{
    public struct PremadeNPCData
    {
        #region Private Fields

        private HashSet<Faction> factions;

        private string name;
        private Race baseRace;
        private CharacterClass baseClass;

        #endregion Private Fields

        #region Public Properties

        public Faction[] Factions { get => factions.ToArray(); set => factions = new HashSet<Faction>(value); }
        public string Name { get => name; set => name = value; }
        public Race BaseRace { get => baseRace; set => baseRace = value; }
        public CharacterClass BaseClass { get => baseClass; set => baseClass = value; }

        #endregion Public Properties
    }
}
