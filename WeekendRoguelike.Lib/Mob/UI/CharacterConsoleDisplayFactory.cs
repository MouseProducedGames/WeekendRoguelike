using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.Mob.Character;
using WeekendRoguelike.UI;
using WeekendRoguelike.UI.ConsoleUI;

namespace WeekendRoguelike.Mob.UI
{
    public class CharacterConsoleDisplayFactory : CharacterDisplayFactory
    {
        #region Public Methods

        public override Display.ICharacterGraphicsWrapper Create(CharacterEntity fromCharacter)
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
