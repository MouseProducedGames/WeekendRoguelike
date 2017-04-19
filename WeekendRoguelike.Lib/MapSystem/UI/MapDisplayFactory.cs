using WeekendRoguelike.CharacterSystem.Base;
using WeekendRoguelike.UI;

namespace WeekendRoguelike.MapSystem.UI
{
    public abstract class MapDisplayFactory
    {
        #region Public Methods

        public abstract Display.IMapGraphicsWrapper Create(Map fromMap);

        #endregion Public Methods
    }
}
