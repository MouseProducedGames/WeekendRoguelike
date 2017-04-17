namespace WeekendRoguelike.UI
{
    public interface ICommandInput<TCommand>
    {
        TCommand GetCommand();
    }
}