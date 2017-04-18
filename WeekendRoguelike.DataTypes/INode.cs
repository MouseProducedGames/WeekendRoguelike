namespace WeekendRoguelike
{
    public interface INode
    {
        #region Public Properties

        int F { get; }
        int G { get; set; }
        int H { get; set; }

        #endregion Public Properties
    }
}
