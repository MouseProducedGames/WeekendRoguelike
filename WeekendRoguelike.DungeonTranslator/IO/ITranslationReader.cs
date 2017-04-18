using System.Collections.Generic;
using WeekendRoguelike.DungeonGenerator.DataTypes;

namespace WeekendRoguelike.DungeonTranslator.IO
{
    public interface ITranslationReader
    {
        #region Public Properties

        bool EndOfSet { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryReadNextTranslation(out KeyValuePair<string, string> output);

        #endregion Public Methods
    }
}
