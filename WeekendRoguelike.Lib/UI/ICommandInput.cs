namespace WeekendRoguelike.UI
{
    public interface ICommandInput<TCommand>
    {
        #region Public Methods

        TCommand GetCommand();

        #endregion Public Methods
    }
}
