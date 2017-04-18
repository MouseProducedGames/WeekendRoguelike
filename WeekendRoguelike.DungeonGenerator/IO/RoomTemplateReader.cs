using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeekendRoguelike.DungeonGenerator.DataTypes;

namespace WeekendRoguelike.DungeonGenerator.IO
{
    public class RoomTemplateReader : IRoomTemplateReader
    {
        #region Private Fields

        private bool endOfFile = false;
        private StreamReader reader;

        #endregion Private Fields

        #region Public Constructors

        public RoomTemplateReader(Stream stream)
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

        public bool TryReadNextRoomTemplate(out RoomTemplate output)
        {
            if (reader.EndOfStream == true)
            {
                endOfFile = true;
                output = null;
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
                string type = null;
                Range width = new Range();
                Range length = new Range();
                Range doors = new Range();
                List<string> nextSet = new List<string>();
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
                        case "TYPE": type = split[1].Trim(); break;
                        case "WIDTH": width = ReadRange(split[1]); break;
                        case "LENGTH": length = ReadRange(split[1]); break;
                        case "DOORS": doors = ReadRange(split[1]); break;
                        case "NEXTSET": nextSet.AddRange(split[1].Split(',').Select(s => s.Trim())); break;
                    }
                }
                output = new RoomTemplate(
                        name: name,
                        type: type,
                        width: width,
                        length: length,
                        doors: doors,
                        nextSet: nextSet);
                return true;
            }
            output = null;
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
