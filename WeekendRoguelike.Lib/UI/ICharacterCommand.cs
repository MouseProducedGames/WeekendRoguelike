using WeekendRoguelike.CharacterSystem;

namespace WeekendRoguelike.UI
{
    public interface ICharacterCommand<TCommand>
    {
        #region Public Methods

        TCommand GetCommand(ICharacter character);

        #endregion Public Methods

    }
}