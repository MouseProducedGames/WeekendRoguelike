using WeekendRoguelike.CharacterSystem;
using WeekendRoguelike.CharacterSystem.Base;

namespace WeekendRoguelike
{
    public class FormulaHelper
    {
        #region Public Methods

        public static int GetArmour(ICharacter defender)
        {
            return defender.EntityData.Stats.GetStatValue(
                CharacterDetail.StatType.Strength);
        }

        public static int GetAttack(ICharacter defender)
        {
            return defender.EntityData.Stats.GetStatValue(
                CharacterDetail.StatType.Coordination);
        }

        public static int GetDamage(ICharacter attacker)
        {
            return attacker.EntityData.Stats.GetStatValue(
                CharacterDetail.StatType.Strength);
        }

        public static int GetDefence(ICharacter defender)
        {
            return defender.EntityData.Stats.GetStatValue(
                CharacterDetail.StatType.Agility);
        }

        #endregion Public Methods
    }
}
