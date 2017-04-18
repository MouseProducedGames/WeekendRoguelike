using WeekendRoguelike.Mob.Monster;

namespace WeekendRoguelike.Mob.IO
{
    public interface IMonsterReader
    {
        #region Public Properties

        bool EndOfSet { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryReadNextMonster(out MonsterData output);

        #endregion Public Methods
    }
}
