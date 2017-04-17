﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public class Character : IMob
    {
        #region Private Fields

        private IMobController controller;
        private CharacterEntity entityData;
        private Map onMap;
        private Point position;

        #endregion Private Fields

        #region Public Properties

        public bool Alive => EntityData.Alive;

        public IMobController Controller { get => controller; set => controller = value; }

        public CharacterEntity EntityData { get => entityData; set => entityData = value; }

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

        public bool IsEnemy(Character otherCharacter)
        {
            switch (Controller.CommandProvider)
            {
                case PlayerCommandInput pci:
                    return otherCharacter.Controller.CommandProvider is
                        MonsterCommandInput;

                default:
                    return (otherCharacter.Controller.CommandProvider is
                        MonsterCommandInput) == false;
            }
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

        #endregion Public Methods
    }
}