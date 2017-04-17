using System;

namespace WeekendRoguelike
{
    public class CombatHelper
    {
        #region Public Methods

        public static void SingleAttack(IMob attacker, Character defender)
        {
            int attackerDamage = FormulaHelper.GetDamage(attacker);
            int defenderArmour = FormulaHelper.GetArmour(defender);

            int damageTotal = Math.Max(0, (attackerDamage * attackerDamage) / defenderArmour);

            defender.ReceiveDamage(damageTotal);
        }

        #endregion Public Methods
    }
}
