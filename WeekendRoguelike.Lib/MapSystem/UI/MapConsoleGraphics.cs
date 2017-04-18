using System.Collections.Generic;
using System.IO;
using WeekendRoguelike.MapSystem.UI.IO;
using WeekendRoguelike.UI.ConsoleUI;

namespace WeekendRoguelike.MapSystem.UI
{
    public static class MapConsoleGraphics
    {
        #region Private Fields

        private static Dictionary<int, string> graphicsIndexedNameLookup =
            new Dictionary<int, string>();

        private static List<ConsoleDisplay.Graphics> graphicsList =
            new List<ConsoleDisplay.Graphics>();

        private static
                            Dictionary<string, ConsoleDisplay.Graphics> graphicsLookup =
            new Dictionary<string, ConsoleDisplay.Graphics>();

        private static Dictionary<string, int> graphicsNamedIndexLookup =
            new Dictionary<string, int>();

        #endregion Private Fields

        #region Public Methods

        public static IEnumerable<KeyValuePair<string, ConsoleDisplay.Graphics>> GetAllNameGraphicsPairs()
        {
            foreach (var kvp in graphicsLookup)
                yield return kvp;
        }

        public static int GetGraphicsIndexByName(string name)
        {
            return graphicsNamedIndexLookup[name.ToUpper()];
        }

        public static string GetGraphicsNameByIndex(int index)
        {
            return graphicsIndexedNameLookup[index];
        }

        public static ConsoleDisplay.Graphics GetMapGraphics(string name)
        {
            return graphicsLookup[name.ToUpper()];
        }

        public static ConsoleDisplay.Graphics GetMapGraphics(int index)
        {
            return graphicsList[index];
        }

        public static void LoadGraphics(IMapConsoleGraphicsReader reader)
        {
            while (reader.EndOfSet == false)
            {
                if (reader.TryReadNextGraphics(out var name, out var nextGraphics))
                {
                    graphicsLookup[name.ToUpper()] = nextGraphics;
                    graphicsNamedIndexLookup[name.ToUpper()] = graphicsList.Count;
                    graphicsIndexedNameLookup[graphicsList.Count] = name.ToUpper();
                    graphicsList.Add(nextGraphics);
                }
            }
        }

        public static void LoadGraphics(string filename)
        {
            LoadGraphics(new MapConsoleGraphicsReader(File.OpenRead(filename)));
        }

        public static bool TryGetMapGraphics(string name, out ConsoleDisplay.Graphics g)
        {
            return graphicsLookup.TryGetValue(name.ToUpper(), out g);
        }

        #endregion Public Methods
    }
}
