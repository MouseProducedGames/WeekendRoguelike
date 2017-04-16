using System;

namespace WeekendRoguelike
{
    public static class CharacterDetail
    {
        public enum StatType
        {
            Strength
        }

        public static StatType StatTypeFromString(string name)
        {
            return (StatType)Enum.Parse(typeof(StatType), name, ignoreCase: true);
        }
    }
}