using System;
using WeekendRoguelike.AI.CharacterSystem;
using WeekendRoguelike.CharacterSystem;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.AI.NPCSystem
{
    public class NPCCommand : ICharacterCommand<WRCommand>
    {
        private Point? wanderPoint;

        #region Public Methods

        public WRCommand GetCommand(ICharacter character)
        {
            ICharacter nearestEenmy = character.VisibleCharacters().GetNearestEnemyOrNull(ofCharacter: character);
            if (nearestEenmy != null)
            {
                return SingleStepTo(character, nearestEenmy.Position);
            }
            // Wander!
            else
            {
                if (wanderPoint.HasValue == false ||
                    character.Position == wanderPoint.Value)
                {
                    wanderPoint = GenerateWanderPoint(character);
                }
                WRCommand command = SingleStepTo(character, wanderPoint.Value);
                if (command == WRCommand.None)
                    wanderPoint = GenerateWanderPoint(character);
                return command;
            }
        }

        private Point GenerateWanderPoint(ICharacter character)
        {
            return character.OnMap.GetRandomValidPoint(character);
        }

        private WRCommand SingleStepTo(ICharacter character, Point goal)
        {
            Displacement disp = character.Position - goal;
            if (Math.Abs(disp.X) <= 1 &&
                Math.Abs(disp.Y) <= 1)
            {
                return (goal - character.Position).ToCommand();
            }
            else if (character.OnMap.TryGetSingleStep(character.Position, goal, out var step))
            {
                return (step - character.Position).ToCommand();
            }
            else
                return WRCommand.None;
        }

        #endregion Public Methods
    }
}
