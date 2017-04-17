using System;

namespace WeekendRoguelike
{
    public struct CharacterStats
    {
        #region Public Fields

        public const int Count = 2;

        #endregion Public Fields

        #region Private Fields

        private int agility;
        private int strength;

        #endregion Private Fields

        #region Public Properties

        public int Agility { get => agility; set => agility = value; }
        public int Strength { get => strength; set => strength = value; }

        #endregion Public Properties

        #region Public Methods

        public static CharacterStats operator +(CharacterStats left, CharacterStats right)
        {
            CharacterStats output = new CharacterStats();
            for (int i = 0; i < Count; ++i)
            {
                output.SetStatValue(
                    (CharacterDetail.StatType)i,
                    left.GetStatValue((CharacterDetail.StatType)i) +
                    right.GetStatValue((CharacterDetail.StatType)i)
                    );
            }
            return output;
        }

        public void Copy(CharacterStats from)
        {
            for (int i = 0; i < Count; ++i)
            {
                SetStatValue((CharacterDetail.StatType)i, from.GetStatValue((CharacterDetail.StatType)i));
            }
        }

        public CharacterStats GetCopy()
        {
            CharacterStats output = new CharacterStats();
            output.Copy(this);
            return output;
        }

        public int GetStatValue(CharacterDetail.StatType stat)
        {
            switch (stat)
            {
                case CharacterDetail.StatType.Strength: return strength;
                case CharacterDetail.StatType.Agility: return agility;
                default: throw new ArgumentOutOfRangeException(stat.ToString());
            }
        }

        public void SetStatValue(CharacterDetail.StatType stat, int value)
        {
            switch (stat)
            {
                case CharacterDetail.StatType.Strength: strength = value; return;
                case CharacterDetail.StatType.Agility: agility = value; return;
                default: throw new ArgumentOutOfRangeException(stat.ToString());
            }
        }

        #endregion Public Methods
    }
}
