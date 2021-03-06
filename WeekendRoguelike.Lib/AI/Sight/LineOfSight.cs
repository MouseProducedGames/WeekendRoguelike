﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.MapSystem;
using WeekendRoguelike.CharacterSystem.Base;

namespace WeekendRoguelike.AI.Sight
{
    public class LineOfSight
    {
        #region Private Fields

        private Character character;
        private int length;
        private VisibilityState[,] visibilityMap;
        private int width;

        #endregion Private Fields

        #region Public Constructors

        public LineOfSight(Character character)
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

        public void Update(Character character)
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

        private void GenerateAll(Character character)
        {
            width = character.OnMap.Width;
            length = character.OnMap.Length;
            visibilityMap = new VisibilityState[length, width];

            Scan(character);
        }

        private void Octant(int range, int signX, int signY, double maxRange, double leftSlope = 0.0, double rightSlope = 1.0, bool swap = false)
        {
            signX = Math.Sign(signX);
            signY = Math.Sign(signY);
            bool CheckBlocking(int x, int y)
            {
                return character.OnMap.SightBlocked(x, y);
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

            Point GetMapCoordinates(int x, int y)
            {
                if (swap == true)
                {
                    Displacement relSigned =
                            GetRelativeSignedCoordinates(
                                new Displacement(x, y));
                    return character.Position +
                        new Displacement(relSigned.Y, relSigned.X);
                }
                else
                {
                    return character.Position +
                        GetRelativeSignedCoordinates(new Displacement(x, y));
                }
            }

            for (; range <= maxRange; ++range)
            {
                if (range * range > maxRange * maxRange)
                    break;
                int relPosY = range;
                int posY = relPosY + character.Position.Y;
                int iPosY = relPosY + character.Position.Y;
                int relendX = (int)(relPosY * leftSlope);
                int relstartX = (int)Math.Ceiling(relPosY * rightSlope);
                int endX = relendX + character.Position.X;
                int startX = relstartX + character.Position.X;
                bool lastWasBlocker = false;
                double savedLeftSlope = leftSlope;
                for (int x = startX, rx = relstartX; x >= endX; --x, --rx)
                {
                    if (relPosY * relPosY + rx * rx > maxRange * maxRange)
                        continue;
                    double bottomRightTileSlope = (rx + 0.5) / (relPosY - 0.5);
                    double topLeftTileSlope = (rx - 0.5) / (relPosY + 0.5);

                    // Clears up edge cases.
                    // We've yet to reach the scan area.
                    if (topLeftTileSlope > rightSlope)
                    {
                        continue;
                    }
                    // We've passed out of the scan area.
                    else if (bottomRightTileSlope < leftSlope)
                    {
                        break;
                    }

                    Point p = GetMapCoordinates(x, iPosY);
                    if (character.OnMap.PointInMap(p))
                        visibilityMap[p.Y, p.X] = VisibilityState.Visible;
                    
                    bool isCurrentBlocker = CheckBlocking(p.X, p.Y);
                    if (lastWasBlocker)
                    {
                        if (isCurrentBlocker)
                        {
                            savedLeftSlope = topLeftTileSlope;
                        }
                        else
                        {
                            rightSlope = savedLeftSlope;
                            lastWasBlocker = false;
                        }
                    }
                    else
                    {
                        if (isCurrentBlocker)
                        {
                            if (bottomRightTileSlope <= rightSlope)
                                Octant(range + 1, signX, signY,
                                    maxRange, bottomRightTileSlope, rightSlope, swap: swap);

                            savedLeftSlope = topLeftTileSlope;
                            lastWasBlocker = true;
                        }
                    }

                    // lastWasBlocker = isCurrentBlocker;
                }

                if (lastWasBlocker)
                    return;
            }
        }

        private void Scan(Character character)
        {
            int sightRange = character.EntityData.Stats.SightRange;
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

            visibilityMap[character.Position.Y, character.Position.X] =
                VisibilityState.Visible;
            for (int y = -1; y <= 1; y += 2)
            {
                for (int x = -1; x <= 1; x += 2)
                {
                    Octant(
                        range: 0,
                        signX: x,
                        signY: y,
                        maxRange: sightRange + 0.5);

                    Octant(
                        range: 0,
                        signX: x,
                        signY: y,
                        maxRange: sightRange + 0.5,
                        swap: true);
                }
            }

            /* OctantVertical(
                posY: character.Position.Y + 0.5,
                signX: 1,
                signY: 1,
                range: 0,
                maxRange: 7); */
        }

        #endregion Private Methods
    }
}
