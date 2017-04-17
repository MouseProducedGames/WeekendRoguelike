using WeekendRoguelike.Mob.Character;

namespace WeekendRoguelike.Mob
{
    public interface IMob
    {
        #region Public Properties

        CharacterData EntityData { get; }
        Map OnMap { get; }
        Point Position { get; }

        #endregion Public Properties

        #region Public Methods

        bool TryMove(Point newPosition);
        bool IsEnemy(CharacterEntity otherCharacter);

        #endregion Public Methods
    }
}
