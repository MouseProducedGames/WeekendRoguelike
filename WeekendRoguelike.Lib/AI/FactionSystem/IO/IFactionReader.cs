namespace WeekendRoguelike.AI.FactionSystem.IO
{
    public interface IFactionReader
    {
        #region Public Properties

        bool EndOfSet { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryReadNextFaction(out Faction output);

        #endregion Public Methods
    }
}
