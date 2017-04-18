using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike.DungeonGenerator
{
    public class Room
    {
        #region Private Fields

        private Point position;

        private readonly HashSet<Point> doors;
        private readonly int length;

        private readonly RoomTemplate template;

        private readonly int width;

        #endregion Private Fields

        #region Public Constructors

        public Room(RoomTemplate template)
        {
            width = template.Width.RandomValueInRange;
            length = template.Length.RandomValueInRange;
            this.template = template;

            void AddDoor(Func<Point> RandDoor)
            {
                var currentDoor = RandDoor();
                bool failure = true;
                int maxCount = 50;
                while (--maxCount >= 0 &&
                    (failure = doors.Any(otherDoor => (currentDoor - otherDoor).MagnitudeSquared == 1)) == true)
                {
                    currentDoor = RandDoor();
                }
                if (failure == false)
                    doors.Add(currentDoor);
            }

            doors = new HashSet<Point>();
            int doorCount = template.Doors.RandomValueInRange;
            for (int i = 0; i < doorCount; ++i)
            {
                Side side = (Side)Rand.NextInt(1, 5);
                switch (side)
                {
                    case Side.North:
                        {
                            Point RandDoor()
                            {
                                return new Point(Rand.NextInt(0, width), -1);
                            }

                            AddDoor(RandDoor);
                            break;
                        }

                    case Side.South:
                        {
                            Point RandDoor()
                            {
                                return new Point(Rand.NextInt(0, width), length);
                            }

                            AddDoor(RandDoor);
                            break;
                        }
                    case Side.West:
                        {
                            Point RandDoor()
                            {
                                return new Point(-1, Rand.NextInt(0, length));
                            }

                            AddDoor(RandDoor);
                            break;
                        }
                    case Side.East:
                        {
                            Point RandDoor()
                            {
                                return new Point(width, Rand.NextInt(0, length));
                            }

                            AddDoor(RandDoor);
                            break;
                        }
                }
            }
        }
        public Room(
            int width,
            int length,
            IReadOnlyCollection<Point> doors,
            RoomTemplate template)
        {
            this.width = width;
            this.length = length;
            this.doors = new HashSet<Point>(doors);
            this.template = template;
        }

        #endregion Public Constructors

        #region Public Properties

        public IReadOnlyCollection<Point> Doors
        {
            get => (IReadOnlyCollection<Point>)doors;
        }
        public int Length => length;

        public int Width => width;

        public RoomTemplate Template => template;

        public Point Position { get => position; set => position = value; }

        #endregion Public Properties

        public bool Intersects(Room other)
        {
            return
                (position.X > other.position.X + other.width ||
                position.X + other.width < other.position.X ||
                position.Y > other.position.Y + other.length ||
                position.Y + other.length < other.position.Y)
                == false;
        }
    }
}
