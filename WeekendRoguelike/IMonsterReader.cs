namespace WeekendRoguelike
{
    public interface IMonsterReader
    {
        bool EndOfSet { get; }

        bool TryReadNextMonster(out Monster output);
    }
}