using WeekendRoguelike.Mob.Character;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.Mob.UI
{
    public abstract class CharacterDisplayFactory
    {
        #region Public Methods

        public abstract Display.ICharacterGraphicsWrapper Create(CharacterEntity fromCharacter);

        public abstract Display.ICharacterGraphicsWrapper Create(string name);

        #endregion Public Methods
    }
}
