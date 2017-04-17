using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike.UI.Monster
{
    public class MonsterCommandInput : ICommandInput<WRCommand>
    {
        #region Private Fields

        private WRCommand[] randomCommandSet =
            new WRCommand[8]
            {
                WRCommand.MoveNorth,
                WRCommand.MoveNorthEast,
                WRCommand.MoveEast,
                WRCommand.MoveSouthEast,
                WRCommand.MoveSouth,
                WRCommand.MoveSouthWest,
                WRCommand.MoveWest,
                WRCommand.MoveNorthWest
            };

        #endregion Private Fields

        #region Public Methods

        public WRCommand GetCommand()
        {
            return randomCommandSet[Rand.NextInt(randomCommandSet.Length)];
        }

        #endregion Public Methods
    }
}
