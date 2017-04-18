using WeekendRoguelike.Mob.Character;
using WeekendRoguelike.UI;
using WeekendRoguelike.UI.ConsoleUI;

namespace WeekendRoguelike.MapSystem.UI
{
    public class MapConsoleDisplayFactory : MapDisplayFactory
    {
        #region Public Methods

        public override Display.IMapGraphicsWrapper Create(Map fromMap)
        {
            return new ConsoleDisplay.MapGraphicsWrapper(fromMap);
        }

        #endregion Public Methods
    }
}
