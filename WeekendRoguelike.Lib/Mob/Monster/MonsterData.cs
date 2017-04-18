using System.Collections.Generic;
using System.Linq;
using WeekendRoguelike.AI.FactionSystem;
using WeekendRoguelike.Mob.Character;

namespace WeekendRoguelike.Mob.Monster
{
    public struct MonsterData
    {
        #region Private Fields

        private HashSet<Faction> factions;

        private string name;
        private CharacterStats stats;

        #endregion Private Fields

        #region Public Properties

        public Faction[] Factions { get => factions.ToArray(); set => factions = new HashSet<Faction>(value); }
        public string Name { get => name; set => name = value; }
        public CharacterStats Stats { get => stats; set => stats = value; }

        #endregion Public Properties
    }
}
