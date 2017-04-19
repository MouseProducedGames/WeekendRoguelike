using WeekendRoguelike.CharacterSystem.Base;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.CharacterSystem.UI
{
    public abstract class CharacterDisplayFactory
    {
        #region Public Methods

        public abstract Display.ICharacterGraphicsWrapper Create(Character fromCharacter);

        public abstract Display.ICharacterGraphicsWrapper Create(string name);

        #endregion Public Methods
    }
}
