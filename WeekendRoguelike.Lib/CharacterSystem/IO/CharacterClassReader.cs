﻿using System;
using System.IO;
using WeekendRoguelike.CharacterSystem.Base;

namespace WeekendRoguelike.CharacterSystem.IO
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
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (line[0] != '[')
                    continue;

                output.Name = line.Substring(1, line.Length - 2);
                CharacterStats stats = new CharacterStats();
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
                    switch(split[0].Trim().ToUpper())
                    {
                        case "STARTINGCLASS": output.StartingClass = bool.Parse(split[1].ToLower()); break;
                        default: stats.SetStatValue(
                            CharacterDetail.StatTypeFromString(
                                split[0]), int.Parse(split[1])); break;
                    }
                }
                output.Stats = stats;
                return true;
            }
            return false;
        }

        #endregion Public Methods
    }
}
