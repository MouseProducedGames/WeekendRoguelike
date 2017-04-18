using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike.DungeonGenerator
{
    public class RoomAndCorridorFactory : DungeonFactory
    {
        #region Public Constructors

        public RoomAndCorridorFactory(string dataFilename) : base(dataFilename)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override TileMap Create(int width, int length)
        {
            HashSet<Room> rooms = new HashSet<Room>();

            bool AddRoom(Room addRoom)
            {
                if (rooms.Any(otherRoom => addRoom.Intersects(otherRoom)))
                {
                    rooms.Add(addRoom);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            Room RandomRoomFromRooms()
            {
                return rooms.ElementAt(Rand.NextInt(rooms.Count));
            }

            IReadOnlyCollection<RoomTemplate> NextTypeSet(Room fromRoom)
            {
                return
                    RoomTemplates.GetRoomTemplatesByType(
                        fromRoom.Template.NextSet.ElementAt(
                            Rand.NextInt(fromRoom.Template.NextSet.Count)));
            }

            Room RoomFromRandomTemplate(
                IReadOnlyCollection<RoomTemplate> fromCollection)
            {
                return
                    new Room(
                        fromCollection.ElementAt(
                            Rand.NextInt(fromCollection.Count)));
            }

            IReadOnlyCollection<RoomTemplate> currentTypeSet =
                (IReadOnlyCollection<RoomTemplate>)
                RoomTemplates.GetAllNameRoomTemplatesPairs().First().Value;
            RoomTemplate currentTemplate =
                currentTypeSet.ElementAt(Rand.NextInt(currentTypeSet.Count));
            Room currentRoom = new Room(currentTemplate);
            currentRoom.Position =
                new Point(
                Rand.NextInt(1, width - 1 - currentRoom.Width),
                Rand.NextInt(1, length - 1 - currentRoom.Length));
            if (AddRoom(currentRoom) == false)
                throw new InvalidOperationException(
                    "The first room added to the set collided with nothing." +
                    " This should never happen.");

            int tries = 50;
            while (--tries >= 0)
            {
                Room parentRoom = RandomRoomFromRooms();
                currentRoom =
                    RoomFromRandomTemplate(NextTypeSet(parentRoom));

                Point door = parentRoom.Doors.ElementAt(
                    Rand.NextInt(parentRoom.Doors.Count));

                bool success = false;
                foreach (var parentDoor in parentRoom.Doors)
                {
                    foreach (var currentDoor in currentRoom.Doors)
                    {
                        currentRoom.Position =
                            parentDoor +
                            new Displacement(currentDoor.X, currentDoor.Y);
                        if (AddRoom(currentRoom) == true)
                        {
                            success = true;
                            break;
                        }
                    }
                    if (success == true)
                        break;
                }
            }

            TileMap output = new TileMap(width, length);

            foreach (var room in rooms)
            {
                for (int y = 0; y < room.Width; ++y)
                {
                    for (int x = 0; x < room.Length; ++x)
                    {
                        output[x, y] = Tile.Floor;
                    }
                }
                foreach (var door in room.Doors)
                {
                    output[door.X, door.Y] = Tile.Door;
                }
            }

            return output;
        }

        #endregion Public Methods
    }
}
