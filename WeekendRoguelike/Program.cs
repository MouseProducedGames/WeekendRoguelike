using System;
using System.IO;
using System.Linq;

namespace WeekendRoguelike
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 26);
            Console.CursorSize = 1;
            Console.CursorVisible = false;

            string startPath = Directory.GetCurrentDirectory();
            AllRaces.LoadRaces(Path.Combine(startPath, "Data", "Races.txt"));
            AllCharacterClasses.LoadClasses(Path.Combine(startPath, "Data", "Classes.txt"));
            AllMonsters.LoadMonsters(Path.Combine(startPath, "Data", "Monsters.txt"));

            Map map = new Map(80, 25);

            {
                var info = new CharacterFactory.FactoryInfo();
                info.RaceData = AllRaces.GetAllNameRacePairs().First().Value;
                info.ClassData = AllCharacterClasses.GetAllNameClassPairs().First().Value;

                var factory = new CharacterFactory();

                Character playerEntity = factory.Create(info);
                playerEntity.OnMap = map;
                playerEntity.Position = map.GetRandomValidPoint(playerEntity);
            }

            {
                var info = new MonsterFactory.FacctoryInfo();
                info.MonsterData = AllMonsters.GetAllNameMonsterPairs().First().Value;

                var factory = new MonsterFactory();

                Character monsterEntity = factory.Create(info);
                monsterEntity.OnMap = map;
                monsterEntity.Position = map.GetRandomValidPoint(monsterEntity);
            }

            while (Input.GetInput.Key != ConsoleKey.Escape)
            {
                Console.Clear();
                foreach (var character in map.AllCharacters())
                {
                    Point drawPoint = character.Position;
                    Console.SetCursorPosition(drawPoint.X, drawPoint.Y);
                    switch (character.Controller.CommandProvider)
                    {
                        case PlayerCommandInput pci: Console.Write('@'); break;
                        default: Console.Write('z'); break;
                    }
                }
                Console.SetCursorPosition(0, 0);
                Input.Update();
                map.Update();
            }
        }

        #endregion Private Methods
    }
}
