using System;
using System.Collections.Generic;
using WeekendRoguelike.CharacterSystem;

namespace WeekendRoguelike.UI.Player
{
    public class PlayerCommand : ICharacterCommand<WRCommand>
    {
        #region Private Fields

        private static HashSet<InputTranslator> inputTranslators =
            new HashSet<InputTranslator>();

        #endregion Private Fields

        #region Public Constructors

        static PlayerCommand()
        {
            inputTranslators.Add(new InputTranslator(
                WRCommand.MoveNorth,
                requiredKey: ConsoleKey.NumPad8));
            inputTranslators.Add(new InputTranslator(
                WRCommand.MoveNorthEast,
                requiredKey: ConsoleKey.NumPad9));
            inputTranslators.Add(new InputTranslator(
                WRCommand.MoveEast,
                requiredKey: ConsoleKey.NumPad6));
            inputTranslators.Add(new InputTranslator(
                WRCommand.MoveSouthEast,
                requiredKey: ConsoleKey.NumPad3));
            inputTranslators.Add(new InputTranslator(
                WRCommand.MoveSouth,
                requiredKey: ConsoleKey.NumPad2));
            inputTranslators.Add(new InputTranslator(
                WRCommand.MoveSouthWest,
                requiredKey: ConsoleKey.NumPad1));
            inputTranslators.Add(new InputTranslator(
                WRCommand.MoveWest,
                requiredKey: ConsoleKey.NumPad4));
            inputTranslators.Add(new InputTranslator(
                WRCommand.MoveNorthWest,
                requiredKey: ConsoleKey.NumPad7));
        }

        #endregion Public Constructors

        #region Public Methods

        public WRCommand GetCommand(ICharacter character)
        {
            return Translate(Input.GetInput);
        }

        #endregion Public Methods

        #region Private Methods

        private WRCommand Translate(ConsoleKeyInfo keyInfo)
        {
            foreach (var inputTranslator in inputTranslators)
            {
                if (inputTranslator.TryGetVerify(keyInfo, out var command) == true)
                    return command;
            }
            return WRCommand.None;
        }

        #endregion Private Methods

        #region Private Structs

        private struct InputTranslator
        {
            #region Public Fields

            public readonly WRCommand Command;
            public readonly ConsoleKey RequiredKey;
            public readonly ConsoleModifiers RequiredModifiers;

            #endregion Public Fields

            #region Public Constructors

            public InputTranslator(WRCommand command, ConsoleKey requiredKey)
                : this(command: command, requiredKey: requiredKey, requiredModifiers: (ConsoleModifiers)0)
            {
            }

            public InputTranslator(WRCommand command, ConsoleKey requiredKey, ConsoleModifiers requiredModifiers)
            {
                Command = command;
                RequiredKey = requiredKey;
                RequiredModifiers = requiredModifiers;
            }

            #endregion Public Constructors

            #region Public Methods

            public bool TryGetVerify(ConsoleKeyInfo keyInfo, out WRCommand command)
            {
                if (keyInfo.Key == RequiredKey &&
                    keyInfo.Modifiers == RequiredModifiers)
                {
                    command = Command;
                    return true;
                }
                command = new WRCommand();
                return false;
            }

            #endregion Public Methods
        }

        #endregion Private Structs
    }
}
