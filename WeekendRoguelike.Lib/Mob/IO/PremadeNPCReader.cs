using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeekendRoguelike.AI.FactionSystem;
using WeekendRoguelike.Mob.Character;
using WeekendRoguelike.Mob.NPCSystem;

namespace WeekendRoguelike.Mob.IO
{
    public class PremadeNPCReader : IPremadeNPCReader
    {
        #region Private Fields

        private bool endOfFile = false;
        private StreamReader reader;

        #endregion Private Fields

        #region Public Constructors

        public PremadeNPCReader(Stream stream)
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

        public bool TryReadNextPremadeNPC(out PremadeNPCData output)
        {
            output = new PremadeNPCData();
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
                List<Faction> factions =
                    new List<Faction>();
                while (string.IsNullOrWhiteSpace(line = reader.ReadLine()) ==
                    false)
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
                        case "FACTION": factions.Add(split[1].Trim()); break;
                        case "RACE":
                            output.BaseRace = AllRaces.GetRace(
                                split[1].Trim()); break;
                        case "CLASS": output.BaseClass =
                                AllCharacterClasses.GetCharacterClass(
                                    split[1].Trim()); break;
                    }
                }
                output.Factions = factions.Union(output.BaseRace.Factions).ToArray();
                return true;
            }
            return false;
        }

        #endregion Public Methods
    }
}
