using WeekendRoguelike.UI;

namespace WeekendRoguelike.Mob
{
    public interface IMobController
    {
        void Update(IMob mob);
    }

    public interface IMobController<TCommand> : IMobController
    {
        #region Public Properties

        ICommandInput<TCommand> CommandProvider { get; set; }

        #endregion Public Properties
    }
}
