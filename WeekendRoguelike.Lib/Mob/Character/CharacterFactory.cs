using System.Collections.Generic;
using WeekendRoguelike.AI.Mob;
using WeekendRoguelike.UI;
using WeekendRoguelike.UI.Player;

namespace WeekendRoguelike.Mob.Character
{
    public class CharacterFactory
    {
        #region Public Methods

        public CharacterEntity Create(FactoryInfo info)
        {
            CharacterEntity output = new CharacterEntity();

            output.CharacterRace = info.RaceData;
            output.CharacterClass = info.ClassData;

            CharacterData entityData = new CharacterData();

            CharacterStats maxStats = new CharacterStats();
            maxStats = info.RaceData.Stats + info.ClassData.Stats;
            entityData.MaxStats = maxStats;
            entityData.Stats = maxStats;

            output.EntityData = entityData;

            output.Controller = new MobController()
            {
                CommandProvider = new PlayerCommandInput(),
            };

            output.Graphics = Display.Instance.CreateGraphicsWrapper(output);

            output.Factions = new HashSet<AI.FactionSystem.Faction>(info.RaceData.Factions);

            return output;
        }

        #endregion Public Methods

        #region Public Structs

        public struct FactoryInfo
        {
            #region Private Fields

            private CharacterClass classData;
            private Race raceData;

            #endregion Private Fields

            #region Public Properties

            public CharacterClass ClassData { get => classData; set => classData = value; }
            public Race RaceData { get => raceData; set => raceData = value; }

            #endregion Public Properties
        }

        #endregion Public Structs
    }
}
