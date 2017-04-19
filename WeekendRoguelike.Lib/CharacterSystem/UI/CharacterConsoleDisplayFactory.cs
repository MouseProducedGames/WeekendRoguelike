using WeekendRoguelike.CharacterSystem.Base;
using WeekendRoguelike.UI;
using WeekendRoguelike.UI.ConsoleUI;

namespace WeekendRoguelike.CharacterSystem.UI
{
    public class CharacterConsoleDisplayFactory : CharacterDisplayFactory
    {
        #region Public Methods

        public override Display.ICharacterGraphicsWrapper Create(Character fromCharacter)
        {
            return Create(fromCharacter.CharacterClass.Name);
            /* (ConsoleDisplay.CharacterGraphicsWrapper)
            Display.GetInstanceAs<ConsoleDisplay>()
            .CreateGraphicsWrapper(fromCharacter); */
        }

        public override Display.ICharacterGraphicsWrapper Create(string name)
        {
            var output =
                new ConsoleDisplay.CharacterGraphicsWrapperImpl(
                    AllCharacterConsoleGraphics.GetCharacterGraphics(name));
            return output;
        }

        #endregion Public Methods
    }
}
