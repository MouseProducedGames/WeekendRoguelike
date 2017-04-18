using System.Collections.Generic;
using System.IO;
using WeekendRoguelike.MapSystem.UI.IO;
using WeekendRoguelike.UI.ConsoleUI;

namespace WeekendRoguelike.MapSystem.UI
{
    public static class AllTileData
    {
        #region Private Fields

        private static Dictionary<int, string> tileDataIndexedNameLookup =
            new Dictionary<int, string>();

        private static List<TileData> tileDataList =
            new List<TileData>();

        private static
            Dictionary<string, TileData> tileDataLookup =
            new Dictionary<string, TileData>();

        private static Dictionary<string, int> tileDataNamedIndexLookup =
            new Dictionary<string, int>();

        #endregion Private Fields

        #region Public Methods

        public static IEnumerable<KeyValuePair<string, TileData>> GetAllNameGraphicsPairs()
        {
            foreach (var kvp in tileDataLookup)
                yield return kvp;
        }

        public static TileData GetTileData(string name)
        {
            return tileDataLookup[name.ToUpper()];
        }

        public static TileData GetTileData(int index)
        {
            return tileDataList[index];
        }

        public static int GetTileDataIndexByName(string name)
        {
            return tileDataNamedIndexLookup[name.ToUpper()];
        }

        public static string GetTileDataNameByIndex(int index)
        {
            return tileDataIndexedNameLookup[index];
        }

        public static void LoadTileData(string filename)
        {
            LoadTileData(new TileDataReader(File.OpenRead(filename)));
        }

        public static void LoadTileData(ITileDataReader reader)
        {
            while (reader.EndOfSet == false)
            {
                if (reader.TryReadNextTileData(out var name, out var nextTileData))
                {
                    tileDataLookup[name.ToUpper()] = nextTileData;
                    tileDataNamedIndexLookup[name.ToUpper()] = tileDataList.Count;
                    tileDataIndexedNameLookup[tileDataList.Count] = name.ToUpper();
                    tileDataList.Add(nextTileData);
                }
            }
        }

        public static bool TryGetTileData(string name, out ConsoleDisplay.Graphics g)
        {
            return tileDataLookup.TryGetValue(name.ToUpper(), out g);
        }

        #endregion Public Methods
    }
}
