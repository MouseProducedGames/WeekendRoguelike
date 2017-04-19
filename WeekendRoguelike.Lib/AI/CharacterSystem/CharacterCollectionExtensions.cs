using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.CharacterSystem;
using WeekendRoguelike.CharacterSystem.Base;

namespace WeekendRoguelike.AI.CharacterSystem
{
    public static class CharacterCollectionExtensions
    {
        #region Public Methods

        public static IEnumerable<ICharacter> GetEnemies(
            this IEnumerable<ICharacter> characterCollection,
            ICharacter ofCharacter
            )
        {
            return
                characterCollection
                .Where(other => ofCharacter.IsEnemy(other));
        }

        public static IEnumerable<ICharacter> GetEnemiesByDistance(
            this IEnumerable<ICharacter> characterCollection,
            ICharacter ofCharacter
            )
        {
            return
                characterCollection
                .GetEnemies(ofCharacter: ofCharacter)
                .OrderBy(
                    other => (ofCharacter.Position - other.Position)
                    .MagnitudeSquared);
        }

        public static ICharacter GetNearestEnemyOrNull(
            this IEnumerable<ICharacter> characterCollection,
            ICharacter ofCharacter
            )
        {
            return
                characterCollection
                .GetEnemiesByDistance(ofCharacter: ofCharacter)
                .FirstOrDefault();
        }

        #endregion Public Methods
    }
}
