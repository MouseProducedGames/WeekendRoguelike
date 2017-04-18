namespace WeekendRoguelike
{
    public enum WRCommand
    {
        None,
        MoveNorth,
        MoveNorthEast,
        MoveEast,
        MoveSouthEast,
        MoveSouth,
        MoveSouthWest,
        MoveWest,
        MoveNorthWest
    }

    public static class WRCommandExtensions
    {
        #region Public Methods

        public static Displacement ToDisplacement(this WRCommand command)
        {
            switch (command)
            {
                case WRCommand.MoveNorth: return new Displacement(0, -1);
                case WRCommand.MoveNorthEast: return new Displacement(1, -1);
                case WRCommand.MoveEast: return new Displacement(1, 0);
                case WRCommand.MoveSouthEast: return new Displacement(1, 1);
                case WRCommand.MoveSouth: return new Displacement(0, 1);
                case WRCommand.MoveSouthWest: return new Displacement(-1, 1);
                case WRCommand.MoveWest: return new Displacement(-1, 0);
                case WRCommand.MoveNorthWest: return new Displacement(-1, -1);
                default: return new Displacement();
            }
        }

        #endregion Public Methods
    }
}
