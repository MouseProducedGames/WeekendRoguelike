using System.Collections.Generic;
using WeekendRoguelike.AI.FactionSystem;
using WeekendRoguelike.AI.CharacterSystem;
using WeekendRoguelike.UI;
using WeekendRoguelike.UI.Player;

namespace WeekendRoguelike.CharacterSystem.Base
{
    public class CharacterFactory
    {
        #region Public Methods

        public Character Create(FactoryInfo info)
        {
            Character output = new Character();

            output.CharacterRace = info.RaceData;
            output.CharacterClass = info.ClassData;

            CharacterData entityData = new CharacterData();

            CharacterStats maxStats = new CharacterStats();
            maxStats = info.RaceData.Stats + info.ClassData.Stats;
            entityData.MaxStats = maxStats;
            entityData.Stats = maxStats;

            output.EntityData = entityData;

            output.Controller = new CharacterController()
            {
                CommandProvider = new PlayerCommand(),
            };

            output.Graphics = Display.Instance.CreateGraphicsWrapper(output);

            output.Factions = info.RaceData.Factions;

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
