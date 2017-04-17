using System.IO;
using System.Linq;

namespace WeekendRoguelike
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            string startPath = Directory.GetCurrentDirectory();
            AllRaces.LoadRaces(Path.Combine(startPath, "Data", "Races.txt"));
            AllCharacterClasses.LoadClasses(Path.Combine(startPath, "Data", "Classes.txt"));
            AllMonsters.LoadMonsters(Path.Combine(startPath, "Data", "Monsters.txt"));

            {
                var info = new CharacterFactory.FactoryInfo();
                info.RaceData = AllRaces.GetAllNameRacePairs().First().Value;
                info.ClassData = AllCharacterClasses.GetAllNameClassPairs().First().Value;

                var factory = new CharacterFactory();

                Character playerEntity = factory.Create(info);
            }

            {
                var info = new MonsterFactory.FacctoryInfo();
                info.MonsterData = AllMonsters.GetAllNameMonsterPairs().First().Value;

                var factory = new MonsterFactory();

                Character monsterEntity = factory.Create(info);
            }
        }

        #endregion Private Methods
    }
}
