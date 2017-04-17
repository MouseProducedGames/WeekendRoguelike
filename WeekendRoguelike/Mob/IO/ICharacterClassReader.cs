namespace WeekendRoguelike
{
    public interface ICharacterClassReader
    {
        #region Public Properties

        bool EndOfSet { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryReadNextClass(out CharacterClass output);

        #endregion Public Methods
    }
}