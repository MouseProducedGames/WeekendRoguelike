namespace WeekendRoguelike
{
    public interface ICharacterConsoleGraphicsReader
    {
        #region Public Properties

        bool EndOfSet { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryReadNextGraphics(out string name, out ConsoleDisplay.Graphics output);

        #endregion Public Methods
    }
}
