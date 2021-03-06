﻿using WeekendRoguelike.CharacterSystem.NPCSystem;

namespace WeekendRoguelike.CharacterSystem.IO
{
    public interface IPremadeNPCReader
    {
        #region Public Properties

        bool EndOfSet { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryReadNextPremadeNPC(out PremadeNPCData output);

        #endregion Public Methods
    }
}
