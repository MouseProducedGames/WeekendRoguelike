using System;
using System.IO;
using WeekendRoguelike.UI.ConsoleUI;

namespace WeekendRoguelike.MapSystem.IO
{
    public class TileDataReader : ITileDataReader
    {
        #region Private Fields

        private bool endOfFile = false;
        private StreamReader reader;

        #endregion Private Fields

        #region Public Constructors

        public TileDataReader(Stream stream)
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

        public bool TryReadNextTileData(out string name, out TileData output)
        {
            if (reader.EndOfStream == true)
            {
                name = null;
                endOfFile = true;
                output = new TileData();
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
                BlockDirections blocksMovement = BlockDirections.None;
                BlockDirections blocksSight = BlockDirections.None;
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
                        case "BLOCKSMOVEMENT":
                            blocksMovement |=
                                (BlockDirections)
                                Enum.Parse(typeof(BlockDirections), split[1].Trim(),
                                ignoreCase: true);
                            break;

                        case "BLOCKSSIGHT":
                            blocksSight |=
                                (BlockDirections)
                                Enum.Parse(typeof(BlockDirections), split[1].Trim(),
                                ignoreCase: true);
                            break;
                    }
                }
                output =
                    new TileData(
                        name: name,
                        blocksMovement: blocksMovement,
                        blocksSight: blocksSight);
                return true;
            }
            name = null;
            output = new TileData();
            return false;
        }

        #endregion Public Methods
    }
}
