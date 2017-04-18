using System;

namespace WeekendRoguelike
{
    public static class Rand
    {
        #region Private Fields

        private static readonly Random rand = new System.Random();

        #endregion Private Fields

        #region Public Methods

        public static bool NextBool()
        {
            return rand.Next(2) == 1;
        }

        public static int NextInt()
        {
            return rand.Next();
        }

        public static int NextInt(int max)
        {
            return rand.Next(max);
        }

        public static int NextInt(int min, int max)
        {
            return rand.Next(min, max);
        }

        public static Point NextPoint()
        {
            return new Point(NextInt(), NextInt());
        }

        public static Point NextPoint(int width, int length)
        {
            return new Point(NextInt(width), NextInt(length));
        }

        #endregion Public Methods
    }
}
