using System;
using WeekendRoguelike.AI.Mob;
using WeekendRoguelike.Mob;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.AI.Monster
{
    public class MonsterCommand : IMobCommand<WRCommand>
    {
        private Point? wanderPoint;

        #region Public Methods

        public WRCommand GetCommand(IMob mob)
        {
            IMob nearestEenmy = mob.VisibleMobs().GetNearestEnemyOrNull(ofCharacter: mob);
            if (nearestEenmy != null)
            {
                return SingleStepTo(mob, nearestEenmy.Position);
            }
            // Wander!
            else
            {
                if (wanderPoint.HasValue == false ||
                    mob.Position == wanderPoint.Value)
                {
                    wanderPoint = GenerateWanderPoint(mob);
                }
                WRCommand command = SingleStepTo(mob, wanderPoint.Value);
                if (command == WRCommand.None)
                    wanderPoint = GenerateWanderPoint(mob);
                return command;
            }
        }

        private Point GenerateWanderPoint(IMob mob)
        {
            return mob.OnMap.GetRandomValidPoint(mob);
        }

        private WRCommand SingleStepTo(IMob mob, Point goal)
        {
            Displacement disp = mob.Position - goal;
            if (Math.Abs(disp.X) <= 1 &&
                Math.Abs(disp.Y) <= 1)
            {
                return (goal - mob.Position).ToCommand();
            }
            else if (mob.OnMap.TryGetSingleStep(mob.Position, goal, out var step))
            {
                return (step - mob.Position).ToCommand();
            }
            else
                return WRCommand.None;
        }

        #endregion Public Methods
    }
}
