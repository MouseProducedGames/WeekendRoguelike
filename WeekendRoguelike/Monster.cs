using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public struct Monster
    {
        private string name;
        private CharacterStats stats;

        public CharacterStats Stats { get => stats; set => stats = value; }
        public string Name { get => name; set => name = value; }
    }
}