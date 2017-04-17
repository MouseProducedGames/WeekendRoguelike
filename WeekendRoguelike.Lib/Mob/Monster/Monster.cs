namespace WeekendRoguelike
{
    public struct Monster
    {
        #region Private Fields

        private string name;
        private CharacterStats stats;

        #endregion Private Fields

        #region Public Properties

        public string Name { get => name; set => name = value; }
        public CharacterStats Stats { get => stats; set => stats = value; }

        #endregion Public Properties
    }
}