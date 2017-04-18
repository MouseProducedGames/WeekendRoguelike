using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike.UI.ConsoleUI
{
    public static class ConsoleColorExtensions
    {
        #region Public Methods

        public static ConsoleColor Darker(this ConsoleColor from)
        {
            switch (from)
            {
                case ConsoleColor.Gray: return ++from;
                default: return (ConsoleColor)Math.Max(0, (int)from - 8);
            }
        }

        #endregion Public Methods
    }
}
