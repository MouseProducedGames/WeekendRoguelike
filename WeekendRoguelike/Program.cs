using System;
using System.IO;
using System.Linq;
using WeekendRoguelike.AI.FactionSystem;
using WeekendRoguelike.DungeonGenerator.Factory;
using WeekendRoguelike.DungeonTranslation;
using WeekendRoguelike.MapSystem;
using WeekendRoguelike.MapSystem.UI;
using WeekendRoguelike.Mob.Character;
using WeekendRoguelike.Mob.Monster;
using WeekendRoguelike.UI;

namespace WeekendRoguelike
{
    internal class Program
    {
        #region Private Methods

        private static void DrawScreen(Map map, CharacterEntity playerCharacter)
        {
            map.Draw(playerCharacter);
            Console.SetCursorPosition(1, 29);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(
                "{0} {1}: {2}",
                playerCharacter.CharacterRace.Name,
                playerCharacter.CharacterClass.Name,
                playerCharacter.EntityData.StatString()
                );
            Console.SetCursorPosition(0, 0);
        }

        private static void Main(string[] args)
        {
            string startPath = Directory.GetCurrentDirectory();

            AllRaces.LoadRaces(Path.Combine(startPath, "Data", "Races.txt"));
            AllCharacterClasses.LoadClasses(Path.Combine(startPath, "Data", "Classes.txt"));
            AllMonsters.LoadMonsters(Path.Combine(startPath, "Data", "Monsters.txt"));
            AllFactions.LoadFactions(Path.Combine(startPath, "Data", "Factions.txt"));
            MapConsoleGraphics.LoadGraphics(Path.Combine(startPath, "Data", "ConsoleTileGraphics.txt"));
            AllTileData.LoadTileData(Path.Combine(startPath, "Data", "TileData.txt"));

            Display.SetInstance(new UI.ConsoleUI.ConsoleDisplay(Path.Combine(startPath, "Data", "ConsoleCharacterGraphics.txt")));

            string dungeonFilename = "RoomAndCorridor.txt";
            DungeonFactory dungeonFactory = new RoomAndCorridorFactory(
                Path.Combine(startPath, "Data", "DungeonGenerator",
                dungeonFilename));

            string dungeonTranslateFilename = "TranslateTable.txt";
            DungeonTranslator dungeonTranslator =
                Path.Combine(startPath, "Data", "DungeonTranslator",
                dungeonTranslateFilename);
            Map map = new Map(dungeonTranslator.Convert(dungeonFactory.Create(80, 80)));

            CharacterEntity playerCharacter;
            {
                var info = new CharacterFactory.FactoryInfo();
                {
                    Listbox listbox = Display.Instance.CreateListbox();
                    listbox.Items = AllRaces.GetAllNameRacePairs().Select(a => (object)a.Value).ToArray();
                    while (listbox.Confirmed == false)
                    {
                        listbox.Draw();
                        Input.Update();
                        listbox.Update();
                    }
                    info.RaceData = (Race)listbox.SelectedItem;
                }
                {
                    Listbox listbox = Display.Instance.CreateListbox();
                    listbox.Items = AllCharacterClasses.GetAllNameClassPairs().Select(a => (object)a.Value).ToArray();
                    while (listbox.Confirmed == false)
                    {
                        listbox.Draw();
                        Input.Update();
                        listbox.Update();
                    }
                    info.ClassData = (CharacterClass)listbox.SelectedItem;
                }
                Console.Clear();

                var factory = new CharacterFactory();

                playerCharacter = factory.Create(info);
                playerCharacter.OnMap = map;
                playerCharacter.Position = map.GetRandomValidPoint(playerCharacter);
            }

            void CreateMonster(string monsterName)
            {
                var info = new MonsterFactory.FacctoryInfo()
                {
                    MonsterData = AllMonsters.GetMonster(monsterName)
                };

                var factory = new MonsterFactory();

                CharacterEntity monsterCharacter = factory.Create(info);
                monsterCharacter.OnMap = map;
                monsterCharacter.Position = map.GetRandomValidPoint(monsterCharacter);

                /* var info = new MonsterFactory.FacctoryInfo();
                info.MonsterData = AllMonsters.GetAllNameMonsterPairs().First().Value;

                var factory = new MonsterFactory();

                CharacterEntity monsterCharacter = factory.Create(info);
                monsterCharacter.OnMap = map;
                monsterCharacter.Position = map.GetRandomValidPoint(monsterCharacter); */
            }

            for (int rat = 0; rat < 10; ++rat)
            {
                CreateMonster("Large Rat");
            }

            for (int zombie = 0; zombie < 3; ++zombie)
            {
                CreateMonster("Zombie");
            }

            for (int skeletalWarrior = 0; skeletalWarrior < 1; ++skeletalWarrior)
            {
                CreateMonster("Skeletal Warrior");
            }

            while (Input.GetInput.Key != ConsoleKey.Escape)
            {
                DrawScreen(map, playerCharacter);
                Display.Instance.Update();
                Input.Update();
                map.Update();

                /* if (map.AllMobs().Count() <= 1)
                {
                    DrawScreen(map, playerCharacter);
                    break;
                } */
            }
            if (playerCharacter.Alive)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(36, 15);
                Console.WriteLine("You won!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(36, 15);
                Console.WriteLine("You lost.");
            }
            Console.ReadKey(true);
        }

        #endregion Private Methods
    }
}
