using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.DungeonTranslation.IO;

namespace WeekendRoguelike.DungeonTranslation
{
    public class DungeonTranslator
    {
        #region Private Fields

        private static Dictionary<string, DungeonTranslator> instanceLookup =
            new Dictionary<string, DungeonTranslator>();

        private readonly Dictionary<DungeonGenerator.DataTypes.Tile,
            MapSystem.Tile> translationLookup =
            new Dictionary<DungeonGenerator.DataTypes.Tile, MapSystem.Tile>();

        private readonly TranslationTable translationTable;

        #endregion Private Fields

        #region Private Constructors

        private DungeonTranslator(string filename)
        {
            translationTable = filename;

            foreach (var kvp in translationTable.GetAllTranslationPairs())
            {
                translationLookup.Add(
                    (DungeonGenerator.DataTypes.Tile)
                    Enum.Parse(typeof(DungeonGenerator.DataTypes.Tile),
                    kvp.Key, ignoreCase: true),
                    new MapSystem.Tile(
                        MapSystem.AllTileData.GetTileDataIndexByName(
                            kvp.Value))
                    );
            }
        }

        #endregion Private Constructors

        #region Public Indexers

        public MapSystem.Tile this[DungeonGenerator.DataTypes.Tile from]
        {
            get
            {
                return translationLookup[from];
            }
        }

        #endregion Public Indexers

        #region Public Methods

        public static implicit operator DungeonTranslator(string filename)
        {
            if (instanceLookup.TryGetValue(filename, out var table) == false)
            {
                table = new DungeonTranslator(filename);
                instanceLookup.Add(filename, table);
            }
            return table;
        }

        public MapSystem.Tile[,] Convert(DungeonGenerator.DataTypes.TileMap from)
        {
            var output = new MapSystem.Tile[from.Length, from.Width];

            for (int y = 0; y < from.Length; ++y)
            {
                for (int x = 0; x < from.Width; ++x)
                {
                    output[y, x] = this[from[x, y]];
                }
            }

            return output;
        }

        #endregion Public Methods
    }
}
