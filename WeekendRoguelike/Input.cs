using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public static class Input
    {
        #region Private Fields

        private static ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

        #endregion Private Fields

        #region Public Properties

        public static ConsoleKeyInfo GetInput
        {
            get => keyInfo;
        }

        #endregion Public Properties

        #region Public Methods

        public static void Update()
        {
            keyInfo = Console.ReadKey(intercept: true);
        }

        #endregion Public Methods
    }
}
