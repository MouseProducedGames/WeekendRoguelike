using System;
using System.IO;

namespace WeekendRoguelike
{
    public class CharacterClassReader : ICharacterClassReader
    {
        #region Private Fields

        private bool endOfFile = false;
        private StreamReader reader;

        #endregion Private Fields

        #region Public Constructors

        public CharacterClassReader(Stream stream)
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

        public bool TryReadNextClass(out CharacterClass output)
        {
            output = new CharacterClass();
            if (reader.EndOfStream == true)
            {
                endOfFile = true;
                return false;
            }
            while (reader.EndOfStream == false)
            {
                string line = reader.ReadLine().Trim();
                if (line[0] != '[')
                    continue;

                output.Name = line.Substring(1, line.Length - 2);
                CharacterStats stats = new CharacterStats();
                while (string.IsNullOrEmpty(line = reader.ReadLine().Trim()) == false)
                {
                    // Line is a comment.
                    if (line.Length >= 2 &&
                        line[0] == '/' &&
                        line[1] == '/')
                        continue;
                    string[] split = line.Split(':');
                    // Not a stat line.
                    if (split.Length != 2)
                        continue;
                    stats.SetStatValue(CharacterDetail.StatTypeFromString(split[0]), int.Parse(split[1]));
                }
                output.Stats = stats;
                return true;
            }
            return false;
        }

        #endregion Public Methods
    }
}