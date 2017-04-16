namespace WeekendRoguelike
{
    public class CharacterEntity
    {
        #region Private Fields

        private CharacterStats stats;

        #endregion Private Fields

        #region Public Properties

        public CharacterStats Stats { get => stats.GetCopy(); set => stats.Copy(value); }

        #endregion Public Properties
    }
}