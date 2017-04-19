using System.Collections.Generic;
using WeekendRoguelike.AI.FactionSystem;
using WeekendRoguelike.AI.Mob;
using WeekendRoguelike.AI.NPCSystem;
using WeekendRoguelike.Mob.Character;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.Mob.NPCSystem
{
    public class PremadeNPCFactory
    {
        #region Public Methods

        public CharacterEntity Create(FactoryInfo info)
        {
            var output = new CharacterEntity();

            var entityData = new CharacterData();

            CharacterStats maxStats = new CharacterStats();

            maxStats = info.NPCData.BaseRace.Stats +
                info.NPCData.BaseClass.Stats;

            output.CharacterRace = info.NPCData.BaseRace;
            output.CharacterClass = info.NPCData.BaseClass;

            entityData.MaxStats = maxStats;
            entityData.Stats = maxStats;

            output.EntityData = entityData;

            output.Controller = new MobController()
            {
                CommandProvider = new NPCCommand()
            };

            output.Graphics = Display.Instance.CreateGraphicsWrapper(info.NPCData.Name);

            output.Factions = info.NPCData.Factions;

            return output;
        }

        #endregion Public Methods

        #region Public Structs

        public struct FactoryInfo
        {
            #region Private Fields

            private PremadeNPCData npcData;

            #endregion Private Fields

            #region Public Properties

            public PremadeNPCData NPCData { get => npcData; set => npcData = value; }

            #endregion Public Properties
        }

        #endregion Public Structs
    }
}
