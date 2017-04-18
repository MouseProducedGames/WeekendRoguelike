using System;
using System.Collections.Generic;
using WeekendRoguelike.AI.FactionSystem;
using WeekendRoguelike.AI.Mob;
using WeekendRoguelike.MapSystem;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.Mob.Character
{
    public class CharacterEntity : IMob
    {
        #region Private Fields

        private CharacterClass characterClass;
        private Race characterRace;
        private IMobController<WRCommand> controller;
        private CharacterData entityData;
        private HashSet<Faction> factions;
        private Display.ICharacterGraphicsWrapper graphics;
        private Map onMap;
        private Point position;

        #endregion Private Fields

        #region Public Properties

        public bool Alive => EntityData.Alive;

        public CharacterClass CharacterClass { get => characterClass; set => characterClass = value; }
        public Race CharacterRace { get => characterRace; set => characterRace = value; }
        public IMobController<WRCommand> Controller { get => controller; set => controller = value; }

        public CharacterData EntityData { get => entityData; set => entityData = value; }

        public IReadOnlyCollection<Faction> Factions { get => (IReadOnlyCollection<Faction>)factions; set => factions = new HashSet<Faction>(value); }
        public Display.ICharacterGraphicsWrapper Graphics { get => graphics; set => graphics = value; }

        public Map OnMap
        {
            get => onMap;
            set
            {
                if (onMap != null)
                    onMap.RemoveCharacter(this);
                onMap = value;
                if (onMap != null)
                    onMap.AddCharacter(this);
            }
        }

        public Point Position { get => position; set => position = value; }

        #endregion Public Properties

        #region Public Methods

        public void Draw()
        {
            graphics.Update(this);
            graphics.Draw();
        }

        public int GetOpinionOn(IMob otherCharacter)
        {
            int sum = 0;
            int total = 0;
            foreach (var factionA in factions)
            {
                foreach (var factionB in otherCharacter.Factions)
                {
                    int value;
                    if (factionA.TryGetRelationshipValue(factionB, out value))
                    {
                        sum += value;
                        ++total;
                    }
                }
            }
            return Math.Max(1, total);
        }

        public bool IsEnemy(IMob otherCharacter)
        {
            int sum = 0;
            int total = 0;
            foreach (var factionA in factions)
            {
                foreach (var factionB in otherCharacter.Factions)
                {
                    int value;
                    if (factionA.TryGetRelationshipValue(factionB, out value))
                    {
                        sum += value;
                        ++total;
                    }
                }
            }
            sum /= Math.Max(1, total);
            return sum <= -50;
        }

        public void ReceiveDamage(int damageTotal)
        {
            entityData.ReceiveDamage(damageTotal);
            if (Alive == false)
                onMap.RemoveCharacter(this);
        }

        public bool TryMove(Point newPosition)
        {
            Displacement disp = newPosition - position;
            if (Math.Abs(disp.X) > 1 ||
                Math.Abs(disp.Y) > 1)
                return false;

            if (onMap.TryMove(this, newPosition) == false)
                return false;

            position = newPosition;
            return true;
        }

        public void Update()
        {
            controller.Update(this);
        }

        public IMobCollection VisibleMobs()
        {
            return OnMap;
        }

        #endregion Public Methods
    }
}
