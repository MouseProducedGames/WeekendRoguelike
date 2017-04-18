using WeekendRoguelike.Mob;

namespace WeekendRoguelike.UI
{
    public interface IMobCommand<TCommand>
    {
        #region Public Methods

        TCommand GetCommand(IMob mob);

        #endregion Public Methods

    }
}