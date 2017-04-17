using System;

namespace WeekendRoguelike
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
            get => Stats.GetStatValue(CharacterDetail.StatType.Strength) > 0;
        }

        public CharacterStats MaxStats { get => maxStats; set => maxStats = value; }
        public CharacterStats Stats { get => stats.GetCopy(); set => stats.Copy(value); }

        #endregion Public Properties

        #region Public Methods

        public void ReceiveDamage(int damageTotal)
        {
            stats.SetStatValue(
                CharacterDetail.StatType.Strength,
                stats.GetStatValue(CharacterDetail.StatType.Strength) -
                damageTotal);
        }

        #endregion Public Methods
    }
}
