using WeekendRoguelike.DungeonGenerator.DataTypes;

namespace WeekendRoguelike.DungeonGenerator.IO
{
    public interface IRoomTemplateReader
    {
        #region Public Properties

        bool EndOfSet { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryReadNextRoomTemplate(out RoomTemplate output);

        #endregion Public Methods
    }
}
