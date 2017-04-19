using System.Collections.Generic;
using System.IO;
using WeekendRoguelike.CharacterSystem.UI.IO;
using WeekendRoguelike.UI.ConsoleUI;

namespace WeekendRoguelike.CharacterSystem.UI
{
    public static class AllCharacterConsoleGraphics
    {
        #region Private Fields

        private static
            Dictionary<string, ConsoleDisplay.Graphics> graphicsLookup =
            new Dictionary<string, ConsoleDisplay.Graphics>();

        #endregion Private Fields

        #region Public Methods

        public static IEnumerable<KeyValuePair<string, ConsoleDisplay.Graphics>> GetAllNameGraphicsPairs()
        {
            foreach (var kvp in graphicsLookup)
                yield return kvp;
        }

        public static ConsoleDisplay.Graphics GetCharacterGraphics(string name)
        {
            return graphicsLookup[name.ToUpper()];
        }

        public static void LoadGraphics(ICharacterConsoleGraphicsReader reader)
        {
            while (reader.EndOfSet == false)
            {
                if (reader.TryReadNextGraphics(out var name, out var nextGraphics))
                {
                    graphicsLookup[name.ToUpper()] = nextGraphics;
                }
            }
        }

        public static void LoadGraphics(string filename)
        {
            LoadGraphics(new CharacterConsoleGraphicsReader(File.OpenRead(filename)));
        }

        public static bool TryGetCharacterGraphics(string name, out ConsoleDisplay.Graphics g)
        {
            return graphicsLookup.TryGetValue(name.ToUpper(), out g);
        }

        #endregion Public Methods
    }
}
