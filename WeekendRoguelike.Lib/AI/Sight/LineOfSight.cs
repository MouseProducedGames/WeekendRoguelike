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

        private void Octant(double posY, int signX, int signY, int range, int maxRange, double innerSlope = 1.0, double outerSlope = 0.0, bool swap = false)
        {
            signX = Math.Sign(signX);
            signY = Math.Sign(signY);
            bool CheckBlocking(double x, double y)
            {
                if (swap == true)
                {
                    Point p = GetSwappedAbsoluteCoordinates((int)x, (int)y);
                    return character.OnMap.SightBlocked(p);
                }
                else
                {
                    Point p = GetAbsoluteCoordinates((int)x, (int)y);
                    return character.OnMap.SightBlocked(p);
                }
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

            Point GetSwappedAbsoluteCoordinates(int x, int y)
            {
                Displacement relSigned =
                        GetRelativeSignedCoordinates(
                            new Displacement(x, y));
                return character.Position +
                    new Displacement(relSigned.Y, relSigned.X);
            }

            for (; range <= maxRange; ++range, ++posY)
            {
                double relPosY = GetRelativeUnsignedCoordinates(0, (int)posY).Y + 0.5;
                int startX = (int)(relPosY * innerSlope) + character.Position.X;
                int endX = (int)(relPosY * outerSlope) + character.Position.X;
                bool lastWasBlocker = CheckBlocking(endX + 0.5, posY);
                for (int x = startX; x >= endX; --x)
                {
                    bool isCurrentBlocker = CheckBlocking(x + 0.5, posY);
                    if (lastWasBlocker)
                    {
                        if (isCurrentBlocker)
                        {
                            Point pos;
                            if (swap == true)
                            {
                                pos = GetSwappedAbsoluteCoordinates(x, (int)posY);
                            }
                            else
                            {
                                pos = GetAbsoluteCoordinates(x, (int)posY);
                            }
                            if (pos.X >= 0 && pos.X < width &&
                                pos.Y >= 0 && pos.Y < length)
                                visibilityMap[pos.Y, pos.X] = VisibilityState.Visible;

                            if (x > endX)
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
                            Point pos;
                            if (swap == true)
                            {
                                pos = GetSwappedAbsoluteCoordinates(x, (int)posY);
                            }
                            else
                            {
                                pos = GetAbsoluteCoordinates(x, (int)posY);
                            }
                            if (pos.X >= 0 && pos.X < width &&
                                pos.Y >= 0 && pos.Y < length)
                                visibilityMap[pos.Y, pos.X] = VisibilityState.Visible;
                            Displacement slopeDisp = GetRelativeUnsignedCoordinates(x, (int)(posY + 1.0));
                            innerSlope = (slopeDisp.X) / (slopeDisp.Y);
                        }
                    }
                    else
                    {
                        if (isCurrentBlocker)
                        {
                            Point pos;
                            if (swap == true)
                            {
                                pos = GetSwappedAbsoluteCoordinates(x, (int)posY);
                            }
                            else
                            {
                                pos = GetAbsoluteCoordinates(x, (int)posY);
                            }
                            if (pos.X >= 0 && pos.X < width &&
                                pos.Y >= 0 && pos.Y < length)
                                visibilityMap[pos.Y, pos.X] = VisibilityState.Visible;

                            Displacement slopeDisp = GetRelativeUnsignedCoordinates(x, (int)(posY));
                            Octant(posY + 1, signX, signY, range + 1, maxRange, innerSlope, (double)(slopeDisp.X) / (slopeDisp.Y), swap: swap);
                            if (x > endX)
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
                            Point pos;
                            if (swap == true)
                            {
                                pos = GetSwappedAbsoluteCoordinates(x, (int)posY);
                            }
                            else
                            {
                                pos = GetAbsoluteCoordinates(x, (int)posY);
                            }
                            if (pos.X >= 0 && pos.X < width &&
                                pos.Y >= 0 && pos.Y < length)
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

            for (int y = -1; y <= 1; y += 2)
            {
                for (int x = -1; x <= 1; x += 2)
                {
                    Octant(
                        posY: character.Position.Y + 0.5,
                        signX: x,
                        signY: y,
                        range: 0,
                        maxRange: 7);

                    Octant(
                        posY: character.Position.Y + 0.5,
                        signX: x,
                        signY: y,
                        range: 0,
                        maxRange: 7,
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
