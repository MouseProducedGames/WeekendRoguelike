using System;
using System.Collections.Generic;
using WeekendRoguelike.CharacterSystem;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.AI.CharacterSystem
{
    public class CharacterController : ICharacterController<WRCommand>
    {
        #region Private Fields

        private static Dictionary<WRCommand, Func<ICharacter, bool>> commandActions =
            new Dictionary<WRCommand, Func<ICharacter, bool>>();

        private ICharacterCommand<WRCommand> commandProvider;

        #endregion Private Fields

        #region Public Constructors

        static CharacterController()
        {
            commandActions.Add(
                WRCommand.MoveNorth,
                character => TryMove(character, character.Position + Displacement.North));
            commandActions.Add(
                WRCommand.MoveNorthEast,
                character => TryMove(character, character.Position + Displacement.NorthEast));
            commandActions.Add(
                WRCommand.MoveEast,
                character => TryMove(character, character.Position + Displacement.East));
            commandActions.Add(
                WRCommand.MoveSouthEast,
                character => TryMove(character, character.Position + Displacement.SouthEast));
            commandActions.Add(
                WRCommand.MoveSouth,
                character => TryMove(character, character.Position + Displacement.South));
            commandActions.Add(
                WRCommand.MoveSouthWest,
                character => TryMove(character, character.Position + Displacement.SouthWest));
            commandActions.Add(
                WRCommand.MoveWest,
                character => TryMove(character, character.Position + Displacement.West));
            commandActions.Add(
                WRCommand.MoveNorthWest,
                character => TryMove(character, character.Position + Displacement.NorthWest));
        }

        #endregion Public Constructors

        #region Public Properties

        public ICharacterCommand<WRCommand> CommandProvider { get => commandProvider; set => commandProvider = value; }

        #endregion Public Properties

        #region Public Methods

        public void Update(ICharacter character)
        {
            if (commandActions.TryGetValue(CommandProvider.GetCommand(character), out var func))
                func(character);
        }

        #endregion Public Methods

        #region Private Methods

        private static bool TryMove(ICharacter character, Point newPosition)
        {
            if (character.TryMove(newPosition) == false)
            {
                if (character.OnMap.TryGetCharacterAt(newPosition, out var otherCharacter) == true &&
                    character.IsEnemy(otherCharacter) == true)
                {
                    CombatHelper.SingleAttack(character, otherCharacter);
                    return true;
                }
                return false;
            }
            return true;
        }

        #endregion Private Methods
    }
}
