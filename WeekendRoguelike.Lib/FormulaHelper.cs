using System;

namespace WeekendRoguelike
{
    public class FormulaHelper
    {
        #region Public Methods

        public static int GetArmour(IMob defender)
        {
            return defender.EntityData.Stats.GetStatValue(
                CharacterDetail.StatType.Strength);
        }

        public static int GetAttack(IMob defender)
        {
            return defender.EntityData.Stats.GetStatValue(
                CharacterDetail.StatType.Coordination);
        }

        public static int GetDamage(IMob attacker)
        {
            return attacker.EntityData.Stats.GetStatValue(
                CharacterDetail.StatType.Strength);
        }

        public static int GetDefence(IMob defender)
        {
            return defender.EntityData.Stats.GetStatValue(
                CharacterDetail.StatType.Agility);
        }

        #endregion Public Methods
    }
}
