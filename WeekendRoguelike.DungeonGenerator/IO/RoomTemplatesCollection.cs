﻿using System.Collections.Generic;
using System.IO;
using WeekendRoguelike.DungeonGenerator.DataTypes;

namespace WeekendRoguelike.DungeonGenerator.IO
{
    public class RoomTemplatesCollection
    {
        #region Private Fields

        private Dictionary<string, HashSet<RoomTemplate>> roomTemplateByName =
            new Dictionary<string, HashSet<RoomTemplate>>();

        private Dictionary<string, HashSet<RoomTemplate>> roomTemplateByType =
            new Dictionary<string, HashSet<RoomTemplate>>();

        #endregion Private Fields

        #region Public Methods

        public IEnumerable<KeyValuePair<string, HashSet<RoomTemplate>>> GetAllNameRoomTemplatesPairs()
        {
            foreach (var kvp in roomTemplateByName)
                yield return kvp;
        }

        public IEnumerable<KeyValuePair<string, HashSet<RoomTemplate>>> GetAllTypeRoomTemplatesPairs()
        {
            foreach (var kvp in roomTemplateByType)
                yield return kvp;
        }

        public IReadOnlyCollection<RoomTemplate> GetRoomTemplatesByName(string name)
        {
            return
                (IReadOnlyCollection<RoomTemplate>)
                roomTemplateByName[name.ToUpper()];
        }

        public IReadOnlyCollection<RoomTemplate> GetRoomTemplatesByType(string type)
        {
            return
                (IReadOnlyCollection<RoomTemplate>)
                roomTemplateByType[type.ToUpper()];
        }

        public void LoadRoomTemplates(IRoomTemplateReader reader)
        {
            void AddRoomtemplate(string key, RoomTemplate nextRoomTemplate,
                Dictionary<string, HashSet<RoomTemplate>> roomLookup)
            {
                if (roomLookup.TryGetValue(key.ToUpper(), out var set) == false)
                {
                    set = new HashSet<RoomTemplate>();
                    roomLookup[nextRoomTemplate.Name.ToUpper()] = set;
                }
                set.Add(nextRoomTemplate);
            }

            while (reader.EndOfSet == false)
            {
                if (reader.TryReadNextRoomTemplate(out var nextRoomTemplate))
                {
                    AddRoomtemplate(nextRoomTemplate.Name, nextRoomTemplate,
                        roomTemplateByName);
                    AddRoomtemplate(nextRoomTemplate.Type, nextRoomTemplate,
                        roomTemplateByType);
                }
            }
        }

        public void LoadRoomTemplates(string filename)
        {
            LoadRoomTemplates(new RoomTemplateReader(File.OpenRead(filename)));
        }

        #endregion Public Methods
    }
}
