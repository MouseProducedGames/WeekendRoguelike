namespace WeekendRoguelike
{
    public interface IMonsterReader
    {
        #region Public Properties

        bool EndOfSet { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryReadNextMonster(out Monster output);

        #endregion Public Methods
    }
}