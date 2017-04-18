using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeekendRoguelike.DungeonGenerator.DataTypes;

namespace WeekendRoguelike.DungeonTranslator.IO
{
    public class TranslationReader : ITranslationReader
    {
        #region Private Fields

        private bool endOfFile = false;
        private StreamReader reader;

        #endregion Private Fields

        #region Public Constructors

        public TranslationReader(Stream stream)
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

        public bool TryReadNextTranslation(out KeyValuePair<string, string> output)
        {
            if (reader.EndOfStream == true)
            {
                endOfFile = true;
                output = new KeyValuePair<string, string>();
                return false;
            }
            while (reader.EndOfStream == false)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (line[0] != '[')
                    continue;

                string name = line.Substring(1, line.Length - 2).Trim();
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");
                string translation = null;
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
                        case "TRANSLATION": translation = split[1].Trim(); break;
                    }
                }
                if (string.IsNullOrWhiteSpace(translation))
                    throw new ArgumentNullException("translation");
                output = new KeyValuePair<string, string>(name, translation);
                return true;
            }
            output = new KeyValuePair<string, string>();
            return false;
        }

        #endregion Public Methods

        #region Private Methods

        private static Range ReadRange(string toSplit)
        {
            Range output;
            string[] minMaxString = toSplit.Split(',');
            output = new Range(int.Parse(minMaxString[0]), int.Parse(minMaxString[1]));
            return output;
        }

        #endregion Private Methods
    }
}
