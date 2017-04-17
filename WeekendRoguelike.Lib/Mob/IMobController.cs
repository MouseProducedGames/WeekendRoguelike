namespace WeekendRoguelike
{
    public interface IMobController
    {
        #region Public Properties

        ICommandInput CommandProvider { get; set; }

        #endregion Public Properties

        #region Public Methods

        void Update(IMob mob);

        #endregion Public Methods
    }
}
