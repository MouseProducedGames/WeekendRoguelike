using System.Text;

namespace WeekendRoguelike.Mob.Character
{
    public struct CharacterClass
    {
        #region Private Fields

        private string name;
        private CharacterStats stats;
        private bool startingClass;

        #endregion Private Fields

        #region Public Properties

        public string Name { get => name; set => name = value; }
        public CharacterStats Stats { get => stats; set => stats = value; }
        /// <summary>
        /// Indicates that the class should be included in the starting
        /// choices for a player.
        /// </summary>
        public bool StartingClass { get => startingClass; set => startingClass = value; }

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
