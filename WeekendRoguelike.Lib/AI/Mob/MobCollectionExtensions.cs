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
            this IEnumerable<IMob> mobCollection,
            IMob ofCharacter
            )
        {
            return
                mobCollection
                .Where(other => ofCharacter.IsEnemy(other));
        }

        public static IEnumerable<IMob> GetEnemiesByDistance(
            this IEnumerable<IMob> mobCollection,
            IMob ofCharacter
            )
        {
            return
                mobCollection
                .GetEnemies(ofCharacter: ofCharacter)
                .OrderBy(
                    other => (ofCharacter.Position - other.Position)
                    .MagnitudeSquared);
        }

        public static IMob GetNearestEnemyOrNull(
            this IEnumerable<IMob> mobCollection,
            IMob ofCharacter
            )
        {
            return
                mobCollection
                .GetEnemiesByDistance(ofCharacter: ofCharacter)
                .FirstOrDefault();
        }

        #endregion Public Methods
    }
}
