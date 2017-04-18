using System;

namespace WeekendRoguelike.MapSystem
{
    [Flags]
    public enum BlockDirections
    {
        None = 0,
        Cardinal = 1,
        Diagonal = 0x2
    }

    public static class BlocksDirectionExtensions
    {
        #region Public Methods

        public static BlockDirections Match(this BlockDirections match, BlockDirections with)
        {
            return match & with;
        }

        public static bool MatchAll(this BlockDirections match, BlockDirections with)
        {
            return match.Match(with) == with;
        }

        public static bool MatchAny(this BlockDirections match, BlockDirections with)
        {
            return match.Match(with) != BlockDirections.None ||
                with == BlockDirections.None;
        }

        #endregion Public Methods
    }
}
