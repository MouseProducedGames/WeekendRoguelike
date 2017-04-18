using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.DungeonGenerator.DataTypes;

namespace WeekendRoguelike.DungeonGenerator.Factory
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
                if (rooms.Any(otherRoom => addRoom.Intersects(otherRoom)) == false)
                {
                    if (addRoom.Position.X < 1 ||
                        addRoom.Position.X + addRoom.Width >= width - 1 ||
                        addRoom.Position.Y < 1 ||
                        addRoom.Position.Y + addRoom.Length >= length - 1)
                    {
                        return false;
                    }
                    foreach (var door in addRoom.Doors)
                    {
                        if (door.Position.X < 1 || door.Position.X >= width - 1 ||
                            door.Position.Y < 1 || door.Position.Y >= length - 1)
                        {
                            return false;
                        }
                    }
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
                var useRooms = rooms.Where(
                    room => room.RelativeDoors.Any(
                        door => door.Filled == false));
                return useRooms.ElementAt(Rand.NextInt(useRooms.Count()));
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
            Room currentRoom =
                new Room(currentTemplate, maxDoors: true);
            currentRoom.Position =
                new Point(
                    width / 2 - currentRoom.Width / 2,
                    length / 2 - currentRoom.Length / 2);
            if (AddRoom(currentRoom) == false)
                throw new InvalidOperationException(
                    "The first room added to the set collided with nothing." +
                    " This should never happen.");

            int tries = (width * length) / 4;
            while (--tries >= 0)
            {
                Room parentRoom = RandomRoomFromRooms();
                
                currentRoom =
                    RoomFromRandomTemplate(NextTypeSet(parentRoom));

                bool success = false;
                foreach (var parentDoor in parentRoom.RelativeDoors)
                {
                    if (parentDoor.Filled == true)
                        continue;
                    foreach (var currentDoor in currentRoom.RelativeDoors)
                    {
                        if (currentDoor.Filled == true)
                            continue;
                        if (parentDoor.Direction.X +
                            currentDoor.Direction.X != 0 ||
                            parentDoor.Direction.Y +
                            currentDoor.Direction.Y != 0)
                            continue;

                        if (parentDoor.Direction.X > 0)
                        {
                            int j = 0;
                            ++j;
                        }

                        currentRoom.Position =
                            parentRoom.Position +
                            parentDoor.Position -
                            currentDoor.Position;
                        /* if (currentRoom.Position + currentDoor.Position - currentDoor.Direction !=
                            parentRoom.Position + parentDoor.Position - parentDoor.Direction)
                            continue; */
                        if (AddRoom(currentRoom) == true)
                        {
                            parentDoor.Fill();
                            currentDoor.Fill();
                            success = true;
                            // tries = -50;
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
                for (int y = 0; y < room.Length; ++y)
                {
                    int py = y + room.Position.Y;
                    for (int x = 0; x < room.Width; ++x)
                    {
                        int px = x + room.Position.X;
                        output[px, py] = Tile.Floor;
                    }
                }
                foreach (var door in room.Doors)
                {
                    if (door.Filled == false)
                        continue;
                    output[door.Position.X, door.Position.Y] = Tile.Door;
                }
            }

            return output;
        }

        #endregion Public Methods
    }
}
