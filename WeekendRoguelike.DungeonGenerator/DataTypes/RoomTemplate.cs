using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike.DungeonGenerator.DataTypes
{
    public class RoomTemplate
    {
        #region Private Fields

        private readonly Range doors;
        private readonly Range length;
        private readonly string name;

        private readonly HashSet<string> nextSet;

        private readonly string type;
        private readonly Range width;

        #endregion Private Fields

        #region Public Constructors

        public RoomTemplate(string name, string type, Range width, Range length, Range doors, IReadOnlyCollection<string> nextSet)
        {
            this.name = name;
            this.type = type;
            this.width = width;
            this.length = length;
            this.doors = doors;
            this.nextSet = new HashSet<string>(nextSet);
        }

        #endregion Public Constructors

        #region Public Properties

        public Range Doors => doors;
        public Range Length => length;
        public string Name => name;

        public IReadOnlyCollection<string> NextSet => (IReadOnlyCollection<string>)nextSet;
        public string Type => type;

        public Range Width => width;

        #endregion Public Properties
    }
}
