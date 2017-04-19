using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike.CharacterSystem
{
    public interface ICharacterCollection : IReadOnlyCollection<ICharacter>
    {
        #region Public Methods

        IEnumerable<ICharacter> AllCharacters();

        #endregion Public Methods
    }
}
