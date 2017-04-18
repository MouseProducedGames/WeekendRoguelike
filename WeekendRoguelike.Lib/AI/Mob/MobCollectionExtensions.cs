using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.Mob;
using WeekendRoguelike.Mob.Character;

namespace WeekendRoguelike.AI.Mob
{
    public static class MobCollectionExtensions
    {
        #region Public Methods

        public static IEnumerable<IMob> GetEnemies(
            this IMobCollection characterCollection,
            IMob ofCharacter
            )
        {
            return
                characterCollection
                .AllMobs()
                .Where(other => ofCharacter.IsEnemy(other));
        }

        public static IEnumerable<IMob> GetEnemiesByDistance(
            this IMobCollection characterCollection,
            IMob ofCharacter
            )
        {
            return
                characterCollection
                .GetEnemies(ofCharacter: ofCharacter)
                .OrderBy(
                    other => (ofCharacter.Position - other.Position)
                    .MagnitudeSquared);
        }

        public static IMob GetNearestEnemyOrNull(
            this IMobCollection characterCollection,
            IMob ofCharacter
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
