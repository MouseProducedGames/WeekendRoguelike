using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.AI.FactionSystem;
using WeekendRoguelike.Mob.Character;
using WeekendRoguelike.UI;
using WeekendRoguelike.UI.Monster;

namespace WeekendRoguelike.Mob.Monster
{
    public class MonsterFactory
    {
        #region Public Methods

        public CharacterEntity Create(FacctoryInfo info)
        {
            var output = new CharacterEntity();

            var entityData = new CharacterData();

            CharacterStats maxStats = new CharacterStats();

            maxStats = info.MonsterData.Stats;

            entityData.MaxStats = maxStats;
            entityData.Stats = maxStats;

            output.EntityData = entityData;

            output.Controller = new MobController()
            {
                CommandProvider = new MonsterCommandInput()
            };

            output.Graphics = Display.Instance.CreateGraphicsWrapper(info.MonsterData.Name);

            output.Factions = new HashSet<Faction>(info.MonsterData.Factions);

            return output;
        }

        #endregion Public Methods

        #region Public Structs

        public struct FacctoryInfo
        {
            #region Private Fields

            private MonsterData monsterData;

            #endregion Private Fields

            #region Public Properties

            public MonsterData MonsterData { get => monsterData; set => monsterData = value; }

            #endregion Public Properties
        }

        #endregion Public Structs
    }
}
