using WeekendRoguelike.MapSystem;
using WeekendRoguelike.Mob.Character;

namespace WeekendRoguelike.Mob
{
    public interface IMob
    {
        #region Public Properties

        CharacterData EntityData { get; }

        IReadOnlyCollection<Faction> Factions { get; }

        Map OnMap { get; }
        Point Position { get; }

        #endregion Public Properties

        #region Public Methods

        bool IsEnemy(IMob otherCharacter);

        bool TryMove(Point newPosition);

        IMobCollection VisibleMobs();

        #endregion Public Methods
    }
}
