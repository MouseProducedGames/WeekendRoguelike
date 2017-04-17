namespace WeekendRoguelike
{
    public interface IRaceReader
    {
        #region Public Properties

        bool EndOfSet { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryReadNextRace(out Race output);

        #endregion Public Methods
    }
}