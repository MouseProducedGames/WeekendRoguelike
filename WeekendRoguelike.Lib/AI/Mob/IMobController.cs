using WeekendRoguelike.Mob;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.AI.Mob
{
    public interface IMobController
    {
        #region Public Methods

        void Update(IMob mob);

        #endregion Public Methods
    }

    public interface IMobController<TCommand> : IMobController
    {
        #region Public Properties

        ICommandInput<TCommand> CommandProvider { get; set; }

        #endregion Public Properties
    }
}
