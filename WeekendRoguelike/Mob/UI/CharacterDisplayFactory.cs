using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public abstract class CharacterDisplayFactory
    {
        #region Public Methods

        public abstract Display.ICharacterGraphicsWrapper Create(Character fromCharacter);

        public abstract Display.ICharacterGraphicsWrapper Create(string name);

        #endregion Public Methods
    }
}
