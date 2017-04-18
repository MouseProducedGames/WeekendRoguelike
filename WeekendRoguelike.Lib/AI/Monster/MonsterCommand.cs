using WeekendRoguelike.AI.Mob;
using WeekendRoguelike.Mob;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.AI.Monster
{
    public class MonsterCommand : IMobCommand<WRCommand>
    {
        #region Public Methods

        public WRCommand GetCommand(IMob mob)
        {
            IMob nearestEenmy = mob.VisibleMobs().GetNearestEnemyOrNull(ofCharacter: mob);
            if (nearestEenmy != null)
            {
                return
                    (mob.OnMap.GetSingleStep(
                        mob.Position,
                        nearestEenmy.Position)
                    - mob.Position)
                    .ToCommand();
            }
            else
                return WRCommand.None;
        }

        #endregion Public Methods
    }
}
