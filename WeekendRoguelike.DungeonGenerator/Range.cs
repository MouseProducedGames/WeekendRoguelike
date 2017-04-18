namespace WeekendRoguelike.DungeonGenerator
{
    public struct Range
    {
        #region Private Fields

        private int max;
        private int min;

        #endregion Private Fields

        #region Public Constructors

        public Range(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Max
        {
            get => max;
            set => max = value;
        }

        public int Min
        {
            get => min;
            set => min = value;
        }

        public int RandomValueInRange
        {
            get => Rand.NextInt(Min, Max + 1);
        }

        #endregion Public Properties

        #region Public Methods

        public void Normalize()
        {
            if (max > min)
            {
                int temp = min;
                min = max;
                max = temp;
            }
        }

        #endregion Public Methods
    }
}
