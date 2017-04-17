using System;
using System.IO;
using System.Linq;

namespace WeekendRoguelike
{
    internal class Program
    {
        #region Private Methods

        private static void DrawScreen(Map map, Character playerCharacter)
        {
            Console.Clear();
            map.Draw();
            Console.SetCursorPosition(1, 29);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(playerCharacter.EntityData.StatString());
            Console.SetCursorPosition(0, 0);
        }

        private static void Main(string[] args)
        {
            string startPath = Directory.GetCurrentDirectory();

            AllRaces.LoadRaces(Path.Combine(startPath, "Data", "Races.txt"));
            AllCharacterClasses.LoadClasses(Path.Combine(startPath, "Data", "Classes.txt"));
            AllMonsters.LoadMonsters(Path.Combine(startPath, "Data", "Monsters.txt"));

            Display.SetInstance(new ConsoleDisplay(Path.Combine(startPath, "Data", "ConsoleCharacterGraphics.txt")));

            Map map = new Map(80, 25);

            Character playerCharacter;
            {
                var info = new CharacterFactory.FactoryInfo();
                info.RaceData = AllRaces.GetAllNameRacePairs().First().Value;
                info.ClassData = AllCharacterClasses.GetAllNameClassPairs().First().Value;

                var factory = new CharacterFactory();

                playerCharacter = factory.Create(info);
                playerCharacter.OnMap = map;
                playerCharacter.Position = map.GetRandomValidPoint(playerCharacter);
            }

            {
                var info = new MonsterFactory.FacctoryInfo();
                info.MonsterData = AllMonsters.GetAllNameMonsterPairs().First().Value;

                var factory = new MonsterFactory();

                Character monsterCharacter = factory.Create(info);
                monsterCharacter.OnMap = map;
                monsterCharacter.Position = map.GetRandomValidPoint(monsterCharacter);
            }

            while (Input.GetInput.Key != ConsoleKey.Escape)
            {
                DrawScreen(map, playerCharacter);
                Input.Update();
                map.Update();

                if (map.AllCharacters().Count() == 1)
                {
                    DrawScreen(map, playerCharacter);
                    break;
                }
            }
            if (playerCharacter.Alive)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(36, 20);
                Console.WriteLine("You won!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(36, 20);
                Console.WriteLine("You lost.");
            }
            Console.ReadKey(true);
        }

        #endregion Private Methods
    }
}
