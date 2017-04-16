using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public class MonsterReader : IMonsterReader
    {
        private StreamReader reader;
        private bool endOfFile = false;

        public MonsterReader(Stream stream)
        {
            if (stream.CanRead == false)
                throw new ArgumentException("Cannot read from stream.");
            reader = new StreamReader(stream);
        }

        public bool EndOfSet { get => endOfFile; }

        public bool TryReadNextMonster(out Monster output)
        {
            output = new Monster();
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
    }
}