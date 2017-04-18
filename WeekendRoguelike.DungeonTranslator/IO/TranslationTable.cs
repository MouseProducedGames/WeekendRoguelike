using System.Collections.Generic;
using System.IO;
using WeekendRoguelike.DungeonGenerator.DataTypes;

namespace WeekendRoguelike.DungeonTranslation.IO
{
    public class TranslationTable
    {
        #region Private Fields

        private static readonly Dictionary<string, TranslationTable> instanceLookup =
            new Dictionary<string, TranslationTable>();

        private Dictionary<string, string> translationLookup =
            new Dictionary<string, string>();

        #endregion Private Fields

        #region Private Constructors

        private TranslationTable(string filename)
        {
            LoadTranslationTable(new TranslationReader(File.OpenRead(filename)));
        }

        #endregion Private Constructors

        #region Public Methods

        public static implicit operator TranslationTable(string filename)
        {
            if (instanceLookup.TryGetValue(filename, out var table) == false)
            {
                table = new TranslationTable(filename);
                instanceLookup.Add(filename, table);
            }
            return table;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllTranslationPairs()
        {
            foreach (var kvp in translationLookup)
                yield return kvp;
        }

        public string GetTranslation(string name)
        {
            return translationLookup[name];
        }

        public void LoadTranslationTable(ITranslationReader reader)
        {
            while (reader.EndOfSet == false)
            {
                if (reader.TryReadNextTranslation(out var translateKVP))
                {
                    translationLookup.Add(translateKVP.Key, translateKVP.Value);
                }
            }
        }

        #endregion Public Methods
    }
}
