﻿namespace WeekendRoguelike
{
    public interface IMob
    {
        #region Public Properties

        CharacterEntity EntityData { get; }
        Map OnMap { get; }
        Point Position { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryMove(Point newPosition);

        #endregion Public Methods
    }
}
