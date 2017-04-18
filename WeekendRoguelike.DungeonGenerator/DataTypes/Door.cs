namespace WeekendRoguelike.DungeonGenerator.DataTypes
{
    public class Door
    {
        #region Private Fields

        private readonly Displacement direction;
        private readonly Displacement position;
        private bool filled = false;

        #endregion Private Fields

        #region Public Constructors

        public Door(Displacement position, Displacement direction, bool filled = false)
        {
            this.position = position;
            this.direction = direction;
            this.filled = filled;
        }

        #endregion Public Constructors

        #region Public Properties

        public Displacement Direction => direction;
        public bool Filled { get => filled; }
        public Displacement Position => position;

        #endregion Public Properties

        #region Public Methods

        public void Fill()
        {
            filled = true;
        }

        #endregion Public Methods
    }
}
