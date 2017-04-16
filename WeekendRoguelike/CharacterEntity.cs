namespace WeekendRoguelike
{
    public class CharacterEntity
    {
        #region Private Fields

        private CharacterStats maxStats;
        private CharacterStats stats;

        #endregion Private Fields

        #region Public Properties

        public CharacterStats MaxStats { get => maxStats; set => maxStats = value; }
        public CharacterStats Stats { get => stats.GetCopy(); set => stats.Copy(value); }

        #endregion Public Properties
    }
}