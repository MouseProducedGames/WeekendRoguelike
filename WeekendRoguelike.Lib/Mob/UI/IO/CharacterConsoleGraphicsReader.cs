using System;
using System.IO;
using WeekendRoguelike.UI.ConsoleUI;

namespace WeekendRoguelike.Mob.UI.IO
{
    public class CharacterConsoleGraphicsReader : ICharacterConsoleGraphicsReader
    {
        #region Private Fields

        private bool endOfFile = false;
        private StreamReader reader;

        #endregion Private Fields

        #region Public Constructors

        public CharacterConsoleGraphicsReader(Stream stream)
        {
            if (stream.CanRead == false)
                throw new ArgumentException("Cannot read from stream.");
            reader = new StreamReader(stream);
        }

        #endregion Public Constructors

        #region Public Properties

        public bool EndOfSet { get => endOfFile; }

        #endregion Public Properties

        #region Public Methods

        public bool TryReadNextGraphics(out string name, out ConsoleDisplay.Graphics output)
        {
            output = new ConsoleDisplay.Graphics();
            output.ForegroundColour = ConsoleColor.Gray;
            output.BackgroundColour = ConsoleColor.Black;
            if (reader.EndOfStream == true)
            {
                name = null;
                endOfFile = true;
                return false;
            }
            while (reader.EndOfStream == false)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (line[0] != '[')
                    continue;

                name = line.Substring(1, line.Length - 2);
                while (string.IsNullOrWhiteSpace(line = reader.ReadLine()) == false)
                {
                    line = line.Trim();
                    // Line is a comment.
                    if (line.Length >= 2 &&
                        line[0] == '/' &&
                        line[1] == '/')
                        continue;
                    string[] split = line.Split(':');
                    // Not a stat line.
                    if (split.Length != 2)
                        continue;

                    switch (split[0].Trim().ToUpper())
                    {
                        case "SYMBOL": output.Symbol = split[1].Trim()[0]; break;
                        case "FOREGROUND":
                            output.ForegroundColour =
                                (ConsoleColor)Enum.Parse(
                                    typeof(ConsoleColor),
                                    split[1].Trim(),
                                    ignoreCase: true);
                            break;

                        case "BACKGROUND":
                            output.BackgroundColour =
                                (ConsoleColor)Enum.Parse(
                                    typeof(ConsoleColor),
                                    split[1].Trim(),
                                    ignoreCase: true);
                            break;
                    }
                }
                return true;
            }
            name = null;
            return false;
        }

        #endregion Public Methods
    }
}
