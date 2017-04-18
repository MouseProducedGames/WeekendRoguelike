using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike.DungeonGenerator
{
    public abstract class DungeonFactory
    {
        #region Public Methods

        public abstract TileMap Create(int width, int length);

        #endregion Public Methods
    }
}
