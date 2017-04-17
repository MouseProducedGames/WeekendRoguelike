using System;
using WeekendRoguelike.Mob;
using WeekendRoguelike.Mob.Character;

namespace WeekendRoguelike
{
    public class CombatHelper
    {
        #region Public Methods

        public static void SingleAttack(IMob attacker, CharacterData defender)
        {
            int attackerAttack = FormulaHelper.GetAttack(attacker);
            int defenderDefence = FormulaHelper.GetDefence(defender);

            // Miss!
            if (Rand.NextInt(attackerAttack + defenderDefence) < defenderDefence)
                return;

            int attackerDamage = FormulaHelper.GetDamage(attacker);
            int defenderArmour = FormulaHelper.GetArmour(defender);

            int damageTotal = Math.Max(0, (attackerDamage * attackerDamage) / defenderArmour);

            defender.ReceiveDamage(damageTotal);
        }

        #endregion Public Methods
    }
}
