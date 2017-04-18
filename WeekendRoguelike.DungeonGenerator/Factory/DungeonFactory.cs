using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.DungeonGenerator.DataTypes;
using WeekendRoguelike.DungeonGenerator.IO;

namespace WeekendRoguelike.DungeonGenerator.Factory
{
    public abstract class DungeonFactory
    {
        #region Private Fields

        private readonly RoomTemplatesCollection roomTemplates;

        #endregion Private Fields

        #region Protected Constructors

        protected DungeonFactory(string dataFilename)
        {
            roomTemplates = new RoomTemplatesCollection();
            roomTemplates.LoadRoomTemplates(dataFilename);
        }

        #endregion Protected Constructors

        #region Protected Properties

        protected RoomTemplatesCollection RoomTemplates => roomTemplates;

        #endregion Protected Properties

        #region Public Methods

        public abstract TileMap Create(int width, int length);

        #endregion Public Methods
    }
}
