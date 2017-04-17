using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public class MobController : IMobController
    {
        #region Private Fields

        private static Dictionary<WRCommand, Func<IMob, bool>> commandActions =
            new Dictionary<WRCommand, Func<IMob, bool>>();

        private ICommandInput commandProvider;

        #endregion Private Fields

        #region Public Constructors

        static MobController()
        {
            commandActions.Add(
                WRCommand.MoveNorth,
                mob => TryMove(mob, mob.Position + Displacement.North));
            commandActions.Add(
                WRCommand.MoveNorthEast,
                mob => TryMove(mob, mob.Position + Displacement.NorthEast));
            commandActions.Add(
                WRCommand.MoveEast,
                mob => TryMove(mob, mob.Position + Displacement.East));
            commandActions.Add(
                WRCommand.MoveSouthEast,
                mob => TryMove(mob, mob.Position + Displacement.SouthEast));
            commandActions.Add(
                WRCommand.MoveSouth,
                mob => TryMove(mob, mob.Position + Displacement.South));
            commandActions.Add(
                WRCommand.MoveSouthWest,
                mob => TryMove(mob, mob.Position + Displacement.SouthWest));
            commandActions.Add(
                WRCommand.MoveWest,
                mob => TryMove(mob, mob.Position + Displacement.West));
            commandActions.Add(
                WRCommand.MoveNorthWest,
                mob => TryMove(mob, mob.Position + Displacement.NorthWest));
        }

        #endregion Public Constructors

        #region Public Properties

        public ICommandInput CommandProvider { get => commandProvider; set => commandProvider = value; }

        #endregion Public Properties

        #region Public Methods

        public void Update(IMob mob)
        {
            if (commandActions.TryGetValue(CommandProvider.GetCommand(), out var func))
                func(mob);
        }

        #endregion Public Methods

        #region Private Methods

        private static bool TryMove(IMob mob, Point newPosition)
        {
            if (mob.TryMove(newPosition) == false)
            {
                if (mob.OnMap.TryGetCharacterAt(newPosition, out var otherCharacter) == true &&
                    mob.IsEnemy(otherCharacter) == true)
                {
                    CombatHelper.SingleAttack(mob, otherCharacter);
                    return true;
                }
                return false;
            }
            return true;
        }

        #endregion Private Methods
    }
}
