using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.DungeonTranslator.IO;

namespace WeekendRoguelike.DungeonTranslator
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
                    (MapSystem.Tile)
                    Enum.Parse(typeof(MapSystem.Tile),
                    kvp.Value, ignoreCase: true));
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

        #endregion Public Methods
    }
}
