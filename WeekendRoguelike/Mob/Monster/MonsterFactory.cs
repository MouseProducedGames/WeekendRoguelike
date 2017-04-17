using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public class MonsterFactory
    {
        #region Public Methods

        public Character Create(FacctoryInfo info)
        {
            var output = new Character();

            var entityData = new CharacterEntity();

            CharacterStats maxStats = new CharacterStats();

            maxStats = info.MonsterData.Stats;

            entityData.MaxStats = maxStats;
            entityData.Stats = maxStats;

            output.EntityData = entityData;

            output.Controller = new MobController()
            {
                CommandProvider = new MonsterCommandInput()
            };
            return output;
        }

        #endregion Public Methods

        #region Public Structs

        public struct FacctoryInfo
        {
            #region Private Fields

            private Monster monsterData;

            #endregion Private Fields

            #region Public Properties

            public Monster MonsterData { get => monsterData; set => monsterData = value; }

            #endregion Public Properties
        }

        #endregion Public Structs
    }
}
