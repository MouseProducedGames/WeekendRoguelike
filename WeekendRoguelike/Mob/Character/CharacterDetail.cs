using System;

namespace WeekendRoguelike
{
    public static class CharacterDetail
    {
        #region Public Enums

        public enum StatType
        {
            Strength,
            Agility,
            Health,
            Coordination
        }

        #endregion Public Enums

        #region Public Methods

        public static StatType StatTypeFromString(string name)
        {
            return (StatType)Enum.Parse(typeof(StatType), name, ignoreCase: true);
        }

        #endregion Public Methods
    }
}
