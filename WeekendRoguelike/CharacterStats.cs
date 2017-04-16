﻿using System;

namespace WeekendRoguelike
{
    public struct CharacterStats
    {
        private int strength;

        public int Strength { get => strength; set => strength = value; }

        public const int Count = 1;

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
            switch(stat)
            {
                case CharacterDetail.StatType.Strength: return strength;
                default: throw new ArgumentOutOfRangeException(stat.ToString());
            }
        }
        public void SetStatValue(CharacterDetail.StatType stat, int value)
        {
            switch(stat)
            {
                case CharacterDetail.StatType.Strength: strength = value; return;
                default: throw new ArgumentOutOfRangeException(stat.ToString());
            }
        }
    }
}