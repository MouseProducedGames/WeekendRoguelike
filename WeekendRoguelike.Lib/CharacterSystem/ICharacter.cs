using System.Collections.Generic;
using WeekendRoguelike.AI.FactionSystem;
using WeekendRoguelike.MapSystem;
using WeekendRoguelike.CharacterSystem.Base;

namespace WeekendRoguelike.CharacterSystem
{
    public interface ICharacter
    {
        #region Public Properties

        CharacterData EntityData { get; }

        IReadOnlyCollection<Faction> Factions { get; }

        Map OnMap { get; }
        Point Position { get; }

        #endregion Public Properties

        #region Public Methods

        bool IsEnemy(ICharacter otherCharacter);

        bool TryMove(Point newPosition);

        IEnumerable<ICharacter> VisibleCharacters();

        #endregion Public Methods
    }
}
