namespace WeekendRoguelike.UI
{
    public abstract class Listbox
    {
        #region Private Fields

        private bool confirmed;
        private object[] items;
        private int selectedIndex;

        #endregion Private Fields

        #region Public Properties

        public bool Confirmed => confirmed;
        public virtual object[] Items { get => items; set => items = value; }

        public virtual int SelectedIndex
        {
            get => selectedIndex;
            set => selectedIndex = value;
        }

        public object SelectedItem
        {
            get => items[selectedIndex];
        }

        #endregion Public Properties

        #region Public Methods

        public abstract void Draw();

        public abstract void Update();

        #endregion Public Methods

        #region Protected Methods

        protected void Confirm()
        {
            confirmed = true;
        }

        #endregion Protected Methods
    }
}
