using System;

namespace WeekendRoguelike.Mob.Character
{
    public static class CharacterDetail
    {
        #region Public Enums

        public enum StatType
        {
            Health,
            Strength,
            Agility,
            Coordination,
            SightRange
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
