using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeekendRoguelike.AI.FactionSystem;

namespace WeekendRoguelike.Mob.Character
{
    public struct Race
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

        #region Public Methods

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Name.PadRight(10));
            stringBuilder.Append(": ");

            for (int i = 0; i < CharacterStats.Count; ++i)
            {
                int curStat = stats.GetStatValue((CharacterDetail.StatType)i);
                stringBuilder.Append(
                    ((CharacterDetail.StatType)i).ToString().Substring(0, 2));
                stringBuilder.Append(": ");
                stringBuilder.Append(curStat.ToString().PadRight(3));
                stringBuilder.Append(' ');
            }

            stringBuilder.Length -= 1;

            return stringBuilder.ToString();
        }

        #endregion Public Methods
    }
}
