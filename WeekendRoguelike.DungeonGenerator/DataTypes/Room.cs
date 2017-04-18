using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike.DungeonGenerator.DataTypes
{
    public class Room
    {
        #region Private Fields

        private readonly HashSet<Door> doors;
        private readonly int length;
        private readonly RoomTemplate template;
        private readonly int width;
        private Point position;

        #endregion Private Fields

        #region Public Constructors

        public Room(RoomTemplate template, bool maxDoors = false)
        {
            width = template.Width.RandomValueInRange;
            length = template.Length.RandomValueInRange;
            this.template = template;

            bool AddDoor(Func<Door> RandDoor)
            {
                var currentDoor = RandDoor();
                bool failure = true;
                int maxCount = 50;
                while (--maxCount >= 0 &&
                    (failure = doors.Any(otherDoor => (currentDoor.Position - otherDoor.Position).MagnitudeSquared == 1)) == true)
                {
                    currentDoor = RandDoor();
                }
                if (failure == false)
                {
                    doors.Add(currentDoor);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            doors = new HashSet<Door>();
            int doorCount = template.Doors.RandomValueInRange;
            if (maxDoors)
                doorCount = template.Doors.Max;
            for (int i = 0; i < doorCount; ++i)
            {
                int tries = 10;
                bool success = false;
                while (--tries >= 0 && success == false)
                {
                    Side side = (Side)Rand.NextInt(1, 5);
                    switch (side)
                    {
                        case Side.North:
                            {
                                Door RandDoor()
                                {
                                    return
                                        new Door(
                                            new Displacement(Rand.NextInt(0, width), -1),
                                            new Displacement(0, -1));
                                }

                                if (AddDoor(RandDoor))
                                    success = true;
                                break;
                            }

                        case Side.South:
                            {
                                Door RandDoor()
                                {
                                    return
                                        new Door(
                                            new Displacement(Rand.NextInt(0, width), length),
                                            new Displacement(0, 1));
                                }

                                if (AddDoor(RandDoor))
                                    success = true;
                                break;
                            }
                        case Side.West:
                            {
                                Door RandDoor()
                                {
                                    return
                                        new Door(
                                            new Displacement(-1, Rand.NextInt(0, length)),
                                            new Displacement(-1, 0));
                                }

                                if (AddDoor(RandDoor))
                                    success = true;
                                break;
                            }
                        case Side.East:
                            {
                                Door RandDoor()
                                {
                                    return
                                        new Door(
                                            new Displacement(width, Rand.NextInt(0, length)),
                                            new Displacement(1, 0));
                                }

                                if (AddDoor(RandDoor))
                                    success = true;
                                break;
                            }
                    }
                }
            }
        }

        public Room(
            int width,
            int length,
            IReadOnlyCollection<Door> doors,
            RoomTemplate template)
        {
            this.width = width;
            this.length = length;
            this.doors = new HashSet<Door>(doors);
            this.template = template;
        }

        #endregion Public Constructors

        #region Public Properties

        public IEnumerable<Door> Doors
        {
            get => (doors.Select(door => new Door((Displacement)(position + door.Position), door.Direction, filled: door.Filled)));
        }

        public int Length => length;

        public Point Position { get => position; set => position = value; }

        public IReadOnlyCollection<Door> RelativeDoors
        {
            get => (IReadOnlyCollection<Door>)doors;
        }

        public RoomTemplate Template => template;
        public int Width => width;

        #endregion Public Properties

        #region Public Methods

        public bool Intersects(Room other)
        {
            return
                (position.X > other.position.X + other.width ||
                position.X + other.width + 1 < other.position.X ||
                position.Y > other.position.Y + other.length ||
                position.Y + other.length + 1 < other.position.Y)
                == false;
        }

        #endregion Public Methods
    }
}
