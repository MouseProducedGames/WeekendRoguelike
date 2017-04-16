namespace WeekendRoguelike
{
    public interface IRaceReader
    {
        bool EndOfSet { get; }

        bool TryReadNextRace(out Race output);
    }
}