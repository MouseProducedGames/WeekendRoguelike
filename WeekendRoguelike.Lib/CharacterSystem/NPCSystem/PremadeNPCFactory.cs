using System.Collections.Generic;
using WeekendRoguelike.AI.FactionSystem;
using WeekendRoguelike.AI.CharacterSystem;
using WeekendRoguelike.AI.NPCSystem;
using WeekendRoguelike.CharacterSystem.Base;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.CharacterSystem.NPCSystem
{
    public class PremadeNPCFactory
    {
        #region Public Methods

        public Character Create(FactoryInfo info)
        {
            var output = new Character();

            var entityData = new Base.CharacterData();

            CharacterStats maxStats = new CharacterStats();

            maxStats = info.NPCData.BaseRace.Stats +
                info.NPCData.BaseClass.Stats;

            output.CharacterRace = info.NPCData.BaseRace;
            output.CharacterClass = info.NPCData.BaseClass;

            entityData.MaxStats = maxStats;
            entityData.Stats = maxStats;

            output.EntityData = entityData;

            output.Controller = new CharacterController()
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
