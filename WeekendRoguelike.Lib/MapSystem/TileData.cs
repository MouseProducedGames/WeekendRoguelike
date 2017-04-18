using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike.MapSystem
{
    public struct TileData : IEquatable<TileData>
    {
        #region Private Fields

        private readonly BlockDirections blocksMovement;
        private readonly BlockDirections blocksSight;
        private readonly string name;

        #endregion Private Fields

        #region Public Constructors

        public TileData(string name,
            BlockDirections blocksMovement,
            BlockDirections blocksSight)
        {
            if (string.IsNullOrWhiteSpace(name) == true)
                throw new ArgumentOutOfRangeException("name", "TileData name must not be null or whitespace.");
            this.name = string.Intern(name);
            this.blocksMovement = blocksMovement;
            this.blocksSight = blocksSight;
        }

        #endregion Public Constructors

        #region Public Properties

        public BlockDirections BlocksMovement => blocksMovement;

        public BlockDirections BlocksSight => blocksSight;

        public string Name => name;

        #endregion Public Properties

        #region Public Methods

        public static bool operator !=(TileData left, TileData right)
        {
            return (left == right) == false;
        }

        public static bool operator ==(TileData left, TileData right)
        {
            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            return obj is TileData td && Equals(td);
        }

        public bool Equals(TileData other)
        {
            return name == other.name;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override string ToString()
        {
            return name;
        }

        #endregion Public Methods
    }
}
