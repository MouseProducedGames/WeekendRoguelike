using System;
using System.IO;
using System.Linq;
using WeekendRoguelike.AI.FactionSystem;
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
            Console.Clear();
            map.Draw();
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

            Display.SetInstance(new UI.ConsoleUI.ConsoleDisplay(Path.Combine(startPath, "Data", "ConsoleCharacterGraphics.txt")));

            Map map = new Map(80, 25);

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

            {
                var info = new MonsterFactory.FacctoryInfo();
                info.MonsterData = AllMonsters.GetAllNameMonsterPairs().First().Value;

                var factory = new MonsterFactory();

                CharacterEntity monsterCharacter = factory.Create(info);
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
