using System;
using System.Text;

namespace WeekendRoguelike.Mob.Character
{
    public class CharacterEntity
    {
        #region Private Fields

        private CharacterStats maxStats;
        private CharacterStats stats;

        #endregion Private Fields

        #region Public Properties

        public bool Alive
        {
            get => Stats.GetStatValue(CharacterDetail.StatType.Health) > 0;
        }

        public CharacterStats MaxStats { get => maxStats; set => maxStats = value; }
        public CharacterStats Stats { get => stats.GetCopy(); set => stats.Copy(value); }

        #endregion Public Properties

        #region Public Methods

        public void ReceiveDamage(int damageTotal)
        {
            stats.SetStatValue(
                CharacterDetail.StatType.Health,
                stats.GetStatValue(CharacterDetail.StatType.Health) -
                damageTotal);
        }

        public string StatString()
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < CharacterStats.Count; ++i)
            {
                int curStat = stats.GetStatValue((CharacterDetail.StatType)i);
                int maxStat = maxStats.GetStatValue((CharacterDetail.StatType)i);
                stringBuilder.Append(
                    ((CharacterDetail.StatType)i).ToString().Substring(0, 2));
                stringBuilder.Append(": ");
                stringBuilder.Append(curStat);
                if (curStat != maxStat)
                {
                    stringBuilder.Append('/');
                    stringBuilder.Append(maxStat);
                }
                stringBuilder.Append(' ');
            }

            stringBuilder.Length -= 1;

            return stringBuilder.ToString();
        }

        #endregion Public Methods
    }
}
