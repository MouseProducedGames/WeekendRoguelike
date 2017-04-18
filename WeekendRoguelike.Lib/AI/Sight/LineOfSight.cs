using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.MapSystem;
using WeekendRoguelike.Mob.Character;

namespace WeekendRoguelike.AI.Sight
{
    public class LineOfSight
    {
        #region Private Fields

        private CharacterEntity character;
        private int length;
        private bool[,] visibilityMap;
        private int width;

        #endregion Private Fields

        #region Public Constructors

        public LineOfSight(CharacterEntity character)
        {
            Update(character);
        }

        #endregion Public Constructors

        #region Public Indexers

        public bool this[int x, int y]
        {
            get
            {
                return visibilityMap[y, x];
            }
        }

        #endregion Public Indexers

        #region Public Methods

        public void Update(CharacterEntity character)
        {
            this.character = character;
            if (visibilityMap == null ||
                character.OnMap.Width != width ||
                character.OnMap.Length != length)
            {
                GenerateAll(character);
                return;
            }

            int sightRange = 7;
            int sightRangeSquared = sightRange * sightRange;
            for (int y = -sightRange - 1; y <= sightRange + 1; ++y)
            {
                int yp = character.Position.Y + y;
                if (yp < 0 || yp >= character.OnMap.Length)
                    continue;
                for (int x = -sightRange - 1; x <= sightRange + 1; ++x)
                {
                    int distSqr = x * x + y * y;
                    int xp = character.Position.X + x;
                    if (xp < 0 || xp >= character.OnMap.Width)
                        continue;
                    visibilityMap[yp, xp] = distSqr <= sightRangeSquared;
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void GenerateAll(CharacterEntity character)
        {
            width = character.OnMap.Width;
            length = character.OnMap.Length;
            visibilityMap = new bool[length, width];

            int sightRange = 7;
            int sightRangeSquared = sightRange * sightRange;
            for (int y = -sightRange - 1; y <= sightRange + 1; ++y)
            {
                int yp = character.Position.Y + y;
                if (yp < 0 || yp >= character.OnMap.Length)
                    continue;
                for (int x = -sightRange - 1; x <= sightRange + 1; ++x)
                {
                    int distSqr = x * x + y * y;
                    int xp = character.Position.X + x;
                    if (xp < 0 || xp >= character.OnMap.Width)
                        continue;
                    visibilityMap[yp, xp] = distSqr <= sightRangeSquared;
                }
            }
        }

        #endregion Private Methods
    }
}
