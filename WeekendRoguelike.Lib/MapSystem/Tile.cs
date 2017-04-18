using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike.MapSystem
{
    public struct Tile : IEquatable<Tile>
    {
        #region Private Fields

        private int id;

        #endregion Private Fields

        #region Public Constructors

        public Tile(int id)
        {
            this.id = id;
        }

        #endregion Public Constructors

        #region Public Properties

        public int ID { get => id; set => id = value; }

        #endregion Public Properties

        #region Public Methods

        public static bool operator !=(Tile left, Tile right)
        {
            return (left == right) == false;
        }

        public static bool operator ==(Tile left, Tile right)
        {
            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            return
                obj is Tile t &&
                Equals(t);
        }

        public bool Equals(Tile other)
        {
            return ID == other.ID;
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public override string ToString()
        {
            return ID.ToString();
        }

        #endregion Public Methods
    }
}
