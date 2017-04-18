using WeekendRoguelike.UI.ConsoleUI;

namespace WeekendRoguelike.MapSystem.UI.IO
{
    public interface ITileDataReader
    {
        #region Public Properties

        bool EndOfSet { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryReadNextTileData(out string name, out TileData output);

        #endregion Public Methods
    }
}
