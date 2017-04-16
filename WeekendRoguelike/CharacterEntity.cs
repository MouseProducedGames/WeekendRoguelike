using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public class CharacterEntity
    {
        private CharacterStats stats;

        public CharacterStats Stats { get => stats.GetCopy(); set => stats.Copy(value); }
    }
}
