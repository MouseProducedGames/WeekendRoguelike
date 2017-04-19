using System;
using System.IO;
using System.Linq;
using WeekendRoguelike.AI.FactionSystem;
using WeekendRoguelike.DungeonGenerator.Factory;
using WeekendRoguelike.DungeonTranslation;
using WeekendRoguelike.MapSystem;
using WeekendRoguelike.MapSystem.UI;
using WeekendRoguelike.CharacterSystem.Base;
using WeekendRoguelike.CharacterSystem.NPCSystem;
using WeekendRoguelike.UI;

namespace WeekendRoguelike
{
    internal class Program
    {
        #region Private Methods

        private static void DrawScreen(Map map, Character playerCharacter)
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

            AllRaces.LoadRaces(Path.Combine(startPath, "Data", "Races", "Animals.txt"));
            AllRaces.LoadRaces(Path.Combine(startPath, "Data", "Races", "Humanoids.txt"));
            AllRaces.LoadRaces(Path.Combine(startPath, "Data", "Races", "Undead.txt"));
            AllCharacterClasses.LoadClasses(Path.Combine(startPath, "Data", "Classes.txt"));
            AllPremadeNPCs.LoadPremadeNPCs(Path.Combine(startPath, "Data", "NPCs", "Animals.txt"));
            AllPremadeNPCs.LoadPremadeNPCs(Path.Combine(startPath, "Data", "NPCs", "Premade.txt"));
            AllPremadeNPCs.LoadPremadeNPCs(Path.Combine(startPath, "Data", "NPCs", "Undead.txt"));
            AllFactions.LoadFactions(Path.Combine(startPath, "Data", "Factions.txt"));
            MapConsoleGraphics.LoadGraphics(Path.Combine(startPath, "Data", "ConsoleGraphics", "Tiles.txt"));
            AllTileData.LoadTileData(Path.Combine(startPath, "Data", "TileData.txt"));

            Display.SetInstance(new UI.ConsoleUI.ConsoleDisplay(
                Path.Combine(startPath, "Data", "ConsoleGraphics", "Animals.txt"),
            Path.Combine(startPath, "Data", "ConsoleGraphics", "Classes.txt"),
            Path.Combine(startPath, "Data", "ConsoleGraphics", "PremadeNPcs.txt"),
            Path.Combine(startPath, "Data", "ConsoleGraphics", "Undead.txt")));

            string dungeonFilename = "RoomAndCorridor.txt";
            DungeonFactory dungeonFactory = new RoomAndCorridorFactory(
                Path.Combine(startPath, "Data", "DungeonGenerator",
                dungeonFilename));

            string dungeonTranslateFilename = "TranslateTable.txt";
            DungeonTranslator dungeonTranslator =
                Path.Combine(startPath, "Data", "DungeonTranslator",
                dungeonTranslateFilename);
            Map map = new Map(dungeonTranslator.Convert(dungeonFactory.Create(40, 40)));

            Character playerCharacter;
            {
                var info = new CharacterFactory.FactoryInfo();
                {
                    Listbox listbox = Display.Instance.CreateListbox();
                    listbox.Items = AllRaces.GetAllNameRacePairs()
                        .Where(a => a.Value.StartingRace == true)
                        .Select(a => (object)a.Value).ToArray();
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
                    listbox.Items = AllCharacterClasses.GetAllNameClassPairs()
                        .Where(a => a.Value.StartingClass == true)
                        .Select(a => (object)a.Value).ToArray();
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

            void CreatePremadeNPC(string premadeNPCName)
            {
                var info = new PremadeNPCFactory.FactoryInfo()
                {
                    NPCData = AllPremadeNPCs.GetPremadeNPC(premadeNPCName)
                };

                var factory = new PremadeNPCFactory();

                Character nonPlayerCharacter = factory.Create(info);
                nonPlayerCharacter.OnMap = map;
                nonPlayerCharacter.Position =
                    map.GetRandomValidPoint(nonPlayerCharacter);
            }

            for (int rat = 0; rat < 20; ++rat)
            {
                CreatePremadeNPC("Large Rat");
            }

            for (int skeleton = 0; skeleton < 8; ++skeleton)
            {
                CreatePremadeNPC("Skeleton");
            }

            for (int zombie = 0; zombie < 5; ++zombie)
            {
                CreatePremadeNPC("Zombie");
            }

            for (int skeletalWarrior = 0; skeletalWarrior < 3; ++skeletalWarrior)
            {
                CreatePremadeNPC("Skeletal Warrior");
            }

            for (int dwarvenBarbarian = 0; dwarvenBarbarian < 1; ++dwarvenBarbarian)
            {
                CreatePremadeNPC("Dwarven Barbarian");
            }

            for (int orcBarbarian = 0; orcBarbarian < 1; ++orcBarbarian)
            {
                CreatePremadeNPC("Orc Barbarian");
            }

            for (int elvenRogue = 0; elvenRogue < 1; ++elvenRogue)
            {
                CreatePremadeNPC("Elven Rogue");
            }

            for (int halflingAssassin = 0; halflingAssassin < 1; ++halflingAssassin)
            {
                CreatePremadeNPC("Halfling Assassin");
            }

            for (int humanFighter = 0; humanFighter < 1; ++humanFighter)
            {
                CreatePremadeNPC("Human Fighter");
            }

            while (Input.GetInput.Key != ConsoleKey.Escape)
            {
                DrawScreen(map, playerCharacter);
                Display.Instance.Update();
                Input.Update();
                map.Update();

                if (map.AllCharacters().Count() <= 1 ||
                    playerCharacter.Alive == false)
                {
                    DrawScreen(map, playerCharacter);
                    break;
                }
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
