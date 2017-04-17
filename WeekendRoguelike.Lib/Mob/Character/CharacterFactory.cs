using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.UI;
using WeekendRoguelike.UI.Player;

namespace WeekendRoguelike.Mob.Character
{
    public class CharacterFactory
    {
        #region Public Methods

        public CharacterData Create(FactoryInfo info)
        {
            CharacterData output = new CharacterData();

            output.CharacterRace = info.RaceData;
            output.CharacterClass = info.ClassData;

            CharacterEntity entityData = new CharacterEntity();

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
