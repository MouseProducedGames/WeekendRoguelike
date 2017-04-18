namespace WeekendRoguelike.DungeonGenerator
{
    public class TileMap
    {
        #region Private Fields

        private readonly int length;
        private readonly Tile[,] tiles;
        private readonly int width;

        #endregion Private Fields

        #region Public Constructors

        public TileMap(int width, int length)
        {
            this.width = width;
            this.length = length;
            tiles = new Tile[length, width];
        }

        #endregion Public Constructors

        #region Public Properties

        public int Length => length;

        public int Width => width;

        #endregion Public Properties

        #region Public Indexers

        public Tile this[int x, int y]
        {
            get
            {
                return tiles[y, x];
            }
            set
            {
                tiles[y, x] = value;
            }
        }

        #endregion Public Indexers
    }
}
