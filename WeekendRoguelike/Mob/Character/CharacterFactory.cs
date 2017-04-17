using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public class CharacterFactory
    {
        #region Public Methods

        public Character Create(FactoryInfo info)
        {
            Character output = new Character();

            CharacterEntity entityData = new CharacterEntity();

            CharacterStats maxStats = new CharacterStats();
            maxStats = info.RaceData.Stats + info.ClassData.Stats;
            entityData.MaxStats = maxStats;
            entityData.Stats = maxStats;

            output.EntityData = entityData;

            output.Controller = new MobController()
            {
                CommandProvider = new PlayerCommandInput()
            };
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
