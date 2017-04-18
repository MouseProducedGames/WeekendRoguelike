using System;
using System.IO;

namespace WeekendRoguelike.AI.FactionSystem.IO
{
    public class FactionReader : IFactionReader
    {
        #region Private Fields

        private bool endOfFile = false;
        private StreamReader reader;

        #endregion Private Fields

        #region Public Constructors

        public FactionReader(Stream stream)
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

        public bool TryReadNextFaction(out Faction output)
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

                output = line.Substring(1, line.Length - 2);
                Faction currentOtherFaction = null;
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
                        case "RELATIONSHIP": currentOtherFaction = split[1].Trim(); break;
                        case "VALUE": Faction.SetRelationship(output, currentOtherFaction, int.Parse(split[1])); break;
                    }
                }
                return true;
            }
            output = null;
            return false;
        }

        #endregion Public Methods
    }
}
