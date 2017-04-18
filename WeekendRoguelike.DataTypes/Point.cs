using System;

namespace WeekendRoguelike
{
    public struct Point : IEquatable<Point>
    {
        #region Public Fields

        public readonly int X;
        public readonly int Y;

        #endregion Public Fields

        #region Public Constructors

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion Public Constructors

        #region Public Methods

        public static explicit operator Point(Displacement p)
        {
            return new Point(p.X, p.Y);
        }

        public static Point operator -(Point left, Displacement right)
        {
            return new Point(
                left.X - right.X,
                left.Y - right.Y
                );
        }

        public static Displacement operator -(Point left, Point right)
        {
            return new Displacement(
                left.X - right.X,
                left.Y - right.Y
                );
        }

        public static bool operator !=(Point left, Point right)
        {
            return left.Equals(right) == false;
        }

        public static Point operator +(Point left, Displacement right)
        {
            return new Point(
                left.X + right.X,
                left.Y + right.Y
                );
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            return
                obj is Point p &&
                Equals(p);
        }

        public bool Equals(Point other)
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
