namespace WeekendRoguelike
{
    public interface ICharacterClassReader
    {
        bool EndOfSet { get; }

        bool TryReadNextClass(out CharacterClass output);
    }
}