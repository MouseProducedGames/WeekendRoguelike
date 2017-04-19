using WeekendRoguelike.CharacterSystem;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.AI.CharacterSystem
{
    public interface ICharacterController
    {
        #region Public Methods

        void Update(ICharacter character);

        #endregion Public Methods
    }

    public interface ICharacterController<TCommand> : ICharacterController
    {
        #region Public Properties

        ICharacterCommand<TCommand> CommandProvider { get; set; }

        #endregion Public Properties
    }
}
