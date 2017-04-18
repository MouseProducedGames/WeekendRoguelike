using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.DungeonGenerator;

namespace WeekendRoguelike.DungeonViewer
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 26);

            string startPath = Directory.GetCurrentDirectory();
            string filename = "RoomAndCorridor.txt";
            DungeonFactory factory = new RoomAndCorridorFactory(
                Path.Combine(startPath, "Data", "DungeonGenerator", filename));

            TileMap map = factory.Create(80, 25);
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < map.Length; ++y)
            {
                for (int x = 0; x < map.Width; ++x)
                {
                    switch (map[x, y])
                    {
                        case Tile.Door:
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write('+');
                                break;
                            }
                        case Tile.Floor:
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write('#');
                                break;
                            }
                        case Tile.Wall:
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write('#');
                                break;
                            }
                    }
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.ReadKey(true);
        }

        #endregion Private Methods
    }
}
