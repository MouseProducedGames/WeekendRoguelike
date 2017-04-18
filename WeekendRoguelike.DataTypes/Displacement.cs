using System;

namespace WeekendRoguelike
{
    public struct Displacement : IEquatable<Displacement>
    {
        #region Public Fields

        public int X;
        public int Y;

        #endregion Public Fields

        #region Public Constructors

        public Displacement(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion Public Constructors

        #region Public Properties

        public static Displacement East => new Displacement(1, 0);
        public static Displacement North => new Displacement(0, -1);
        public static Displacement NorthEast => new Displacement(1, -1);
        public static Displacement NorthWest => new Displacement(-1, -1);
        public static Displacement South => new Displacement(0, 1);
        public static Displacement SouthEast => new Displacement(1, 1);
        public static Displacement SouthWest => new Displacement(-1, 1);
        public static Displacement West => new Displacement(-1, 0);
        public double Magnitude => Math.Sqrt(MagnitudeSquared);
        public int MagnitudeSquared => X * X + Y * Y;

        #endregion Public Properties

        #region Public Methods

        public static explicit operator Displacement(Point p)
        {
            return new Displacement(p.X, p.Y);
        }

        public static Displacement operator -(Displacement left, Displacement right)
        {
            return new Displacement(
                    left.X - right.X,
                    left.Y - right.Y
                    );
        }

        public static bool operator !=(Displacement left, Displacement right)
        {
            return left.Equals(right) == false;
        }

        public static Displacement operator +(Displacement left, Displacement right)
        {
            return new Displacement(
                    left.X + right.X,
                    left.Y + right.Y
                    );
        }

        public static bool operator ==(Displacement left, Displacement right)
        {
            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            return
                obj is Displacement d &&
                Equals(d);
        }

        public bool Equals(Displacement other)
        {
            return
                X == other.X &&
                Y == other.Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}", X, Y);
        }

        #endregion Public Methods
    }
}
