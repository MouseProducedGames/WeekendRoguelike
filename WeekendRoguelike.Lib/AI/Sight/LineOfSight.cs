﻿using System;
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
        private VisibilityState[,] visibilityMap;
        private int width;

        #endregion Private Fields

        #region Public Constructors

        public LineOfSight(CharacterEntity character)
        {
            Update(character);
        }

        #endregion Public Constructors

        #region Private Enums

        private enum VisibleBlockState
        {
            NotStarted = 0,
            StartedVisibleBlock,
            StartedNonvisibleBlock
        }

        #endregion Private Enums

        #region Public Indexers

        public VisibilityState this[int x, int y]
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

            Scan(character);
        }

        #endregion Public Methods

        #region Private Methods

        private void GenerateAll(CharacterEntity character)
        {
            width = character.OnMap.Width;
            length = character.OnMap.Length;
            visibilityMap = new VisibilityState[length, width];

            Scan(character);
        }

        private void OctantVertical(double posY, int signX, int signY, int range, int maxRange, double innerSlope = 1.0, double outerSlope = 0.0)
        {
            signX = Math.Sign(signX);
            signY = Math.Sign(signY);
            bool CheckBlocking(double x, double y)
            {
                Point p = GetAbsoluteCoordinates((int)x, (int)y);
                return character.OnMap.SightBlocked(p);
            }

            Displacement GetRelativeUnsignedCoordinates(int x, int y)
            {
                return new Displacement(
                    x - character.Position.X,
                    y - character.Position.Y);
            }

            Displacement GetRelativeSignedCoordinates(Displacement disp)
            {
                disp = GetRelativeUnsignedCoordinates(disp.X, disp.Y);
                return new Displacement(disp.X * signX, disp.Y * signY);
            }

            Point GetAbsoluteCoordinates(int x, int y)
            {
                return character.Position +
                    GetRelativeSignedCoordinates(new Displacement(x, y));
            }

            double relPosY = GetRelativeUnsignedCoordinates(0, (int)posY).Y;
            int startX = (int)((relPosY + 0.5) * innerSlope);
            int endX = (int)((relPosY + 0.5) * outerSlope);

            for (; range <= maxRange; ++range, ++posY)
            {
                relPosY = GetRelativeUnsignedCoordinates(0, (int)posY).Y;
                startX = (int)(relPosY * innerSlope);
                endX = (int)(relPosY * outerSlope);
                bool lastWasBlocker = CheckBlocking(character.Position.X + startX,
                    posY);
                for (int x = startX + character.Position.X; x >= endX + character.Position.X; --x)
                {
                    bool isCurrentBlocker = CheckBlocking(x + 0.5, posY);
                    if (lastWasBlocker)
                    {
                        if (isCurrentBlocker)
                        {
                            Point pos = GetAbsoluteCoordinates(x, (int)posY);
                            if (pos.X >= 0 || pos.X < width &&
                                pos.Y >= 0 || pos.Y < length)
                                visibilityMap[pos.Y, pos.X] = VisibilityState.Visible;

                            if (x > endX + character.Position.X)
                            {
                                continue;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            Point pos = GetAbsoluteCoordinates(x, (int)posY);
                            if (pos.X >= 0 || pos.X < width &&
                                pos.Y >= 0 || pos.Y < length)
                                visibilityMap[pos.Y, pos.X] = VisibilityState.Visible;
                            Displacement slopeDisp = GetRelativeUnsignedCoordinates(x, (int)(posY + 1.0));
                            innerSlope = (double)slopeDisp.X / slopeDisp.Y;
                        }
                    }
                    else
                    {
                        if (isCurrentBlocker)
                        {

                            Point pos = GetAbsoluteCoordinates(x, (int)posY);
                            if (pos.X >= 0 || pos.X < width &&
                                pos.Y >= 0 || pos.Y < length)
                                visibilityMap[pos.Y, pos.X] = VisibilityState.Visible;

                            Displacement slopeDisp = GetRelativeUnsignedCoordinates(x, (int)posY);
                            OctantVertical(posY + 1, signX, signY, range + 1, maxRange, innerSlope, (double)slopeDisp.X / slopeDisp.Y);

                            if (x > endX + character.Position.X)
                            {
                                continue;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            Point pos = GetAbsoluteCoordinates(x, (int)posY);
                            if (pos.X >= 0 || pos.X < width &&
                                pos.Y >= 0 || pos.Y < length)
                                visibilityMap[pos.Y, pos.X] = VisibilityState.Visible;
                        }
                    }
                    lastWasBlocker = isCurrentBlocker;
                }
            }
        }

        private void Scan(CharacterEntity character)
        {
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
                    /* if (distSqr <= sightRangeSquared)
                    {
                        visibilityMap[yp, xp] = VisibilityState.Visible;
                    }
                    else */
                    if (visibilityMap[yp, xp] == VisibilityState.Visible)
                    {
                        visibilityMap[yp, xp] = VisibilityState.Seen;
                    }
                }
            }

            OctantVertical(
                posY: character.Position.Y + 0.5,
                signX: 1,
                signY: 1,
                range: 0,
                maxRange: 7);
        }

        #endregion Private Methods
    }
}
