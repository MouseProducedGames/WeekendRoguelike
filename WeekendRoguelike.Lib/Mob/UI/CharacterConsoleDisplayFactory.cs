using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
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
