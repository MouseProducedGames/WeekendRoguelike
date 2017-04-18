using System;
using WeekendRoguelike.MapSystem;
using WeekendRoguelike.MapSystem.UI;
using WeekendRoguelike.Mob.Character;
using WeekendRoguelike.Mob.UI;

namespace WeekendRoguelike.UI.ConsoleUI
{
    public class ConsoleDisplay : Display<ConsoleDisplay.Graphics>
    {
        #region Private Fields

        private Graphics[,] backBuffer;
        private Graphics[,] frontBuffer;
        private int length;
        private int width;

        #endregion Private Fields

        #region Public Constructors

        public ConsoleDisplay(string characterGraphicsFilename)
        {
            Console.SetWindowSize(80, 30);
            Console.SetBufferSize(80, 31);
            Console.CursorSize = 1;
            Console.CursorVisible = false;
            backBuffer = new Graphics[30, 80];
            frontBuffer = new Graphics[30, 80];
            length = 30;
            width = 80;

            CharacterDisplayFactory = new CharacterConsoleDisplayFactory();
            MapDisplayFactory = new MapConsoleDisplayFactory();
            AllCharacterConsoleGraphics.LoadGraphics(characterGraphicsFilename);
        }

        #endregion Public Constructors

        #region Public Methods

        public override IGraphicsWrapper CreateGraphicsWrapper()
        {
            return new GraphicsWrapperImpl(new Graphics());
        }

        public override Listbox CreateListbox()
        {
            return new ConsoleListbox();
        }

        public void Draw(GraphicsWrapperImpl item)
        {
            Draw(item.Position, item.Data);
        }

        public void Draw(Point p, Graphics g)
        {
            Draw(p.X, p.Y, g);
        }

        public void Draw(int x, int y, Graphics g)
        {
            backBuffer[y, x] = g;
        }

        public override void Update()
        {
            var temp = frontBuffer;
            frontBuffer = backBuffer;
            backBuffer = temp;

            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < length; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    if (frontBuffer[y, x] != backBuffer[y, x])
                    {
                        backBuffer[y, x] = frontBuffer[y, x];
                        Console.BackgroundColor = frontBuffer[y, x].BackgroundColour;
                        Console.ForegroundColor = frontBuffer[y, x].ForegroundColour;
                        Console.SetCursorPosition(x, y);
                        Console.Write(frontBuffer[y, x].Symbol);
                    }
                }
            }
            Console.SetCursorPosition(0, 0);
        }

        #endregion Public Methods

        #region Public Structs

        public struct Graphics : IEquatable<Graphics>
        {
            #region Private Fields

            private ConsoleColor backgroundColour;
            private ConsoleColor foregroundColour;
            private char symbol;

            #endregion Private Fields

            #region Public Constructors

            public Graphics(
                char symbol,
                ConsoleColor foregroundColour,
                ConsoleColor backgroundColour
                )
            {
                this.symbol = symbol;
                this.backgroundColour = backgroundColour;
                this.foregroundColour = foregroundColour;
            }

            #endregion Public Constructors

            #region Public Properties

            public ConsoleColor BackgroundColour { get => backgroundColour; set => backgroundColour = value; }
            public ConsoleColor ForegroundColour { get => foregroundColour; set => foregroundColour = value; }
            public char Symbol { get => symbol; set => symbol = value; }

            #endregion Public Properties

            #region Public Methods

            public static bool operator !=(Graphics left, Graphics right)
            {
                return (left == right) == false;
            }

            public static bool operator ==(Graphics left, Graphics right)
            {
                return left.Equals(right);
            }

            public override bool Equals(object obj)
            {
                return obj is Graphics g && Equals(g);
            }

            public bool Equals(Graphics other)
            {
                return
                    backgroundColour == other.backgroundColour &&
                    foregroundColour == other.foregroundColour &&
                    symbol == other.symbol;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return symbol.ToString();
            }

            #endregion Public Methods
        }

        #endregion Public Structs

        #region Public Classes

        public class CharacterGraphicsWrapperImpl : GraphicsWrapperImpl, ICharacterGraphicsWrapper
        {
            #region Public Constructors

            public CharacterGraphicsWrapperImpl(Graphics data) : base(data)
            {
            }

            #endregion Public Constructors

            #region Public Methods

            public void Update(CharacterEntity forCharacter)
            {
                Position = forCharacter.Position;
            }

            #endregion Public Methods
        }

        public class GraphicsWrapperImpl : IGraphicsWrapper
        {
            #region Private Fields

            private Graphics data;
            private Point position;

            #endregion Private Fields

            #region Public Constructors

            public GraphicsWrapperImpl(Graphics data)
            {
                this.data = data;
            }

            #endregion Public Constructors

            #region Public Properties

            public Graphics Data { get => data; set => data = value; }
            public Point Position { get => position; set => position = value; }

            #endregion Public Properties

            #region Public Methods

            public void Draw()
            {
                Display.GetInstanceAs<ConsoleDisplay>().Draw(this);
            }

            #endregion Public Methods
        }

        public class MapGraphicsWrapper : IMapGraphicsWrapper
        {
            #region Private Fields

            private Graphics[,] graphicsMap;
            private int length;
            private int width;

            #endregion Private Fields

            #region Public Constructors

            public MapGraphicsWrapper(Map map)
            {
                Update(map);
            }

            #endregion Public Constructors

            #region Public Methods

            public void Draw()
            {
                ConsoleDisplay instance =
                    Display.GetInstanceAs<ConsoleDisplay>();
                for (int y = 0; y < length; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        instance.Draw(x, y, graphicsMap[y, x]);
                    }
                }
            }

            public void Update(Map forMap)
            {
                if (graphicsMap == null ||
                    width != forMap.Width ||
                    length != forMap.Length)
                    DoAll(forMap);
                if (forMap.TileMapChanged == false)
                    return;

                for (int y = 0; y < forMap.Length; ++y)
                {
                    for (int x = 0; x < forMap.Width; ++x)
                    {
                        if (forMap.Changed(x, y))
                        {
                            graphicsMap[y, x] = MapConsoleGraphics.GetMapGraphics(forMap[y, x].ID);
                        }
                    }
                }
            }

            #endregion Public Methods

            #region Private Methods

            private void DoAll(Map forMap)
            {
                graphicsMap = new Graphics[forMap.Length, forMap.Width];
                width = forMap.Width;
                length = forMap.Length;

                for (int y = 0; y < forMap.Length; ++y)
                {
                    for (int x = 0; x < forMap.Width; ++x)
                    {
                        graphicsMap[y, x] = MapConsoleGraphics.GetMapGraphics(forMap[x, y].ID);
                    }
                }
            }

            #endregion Private Methods
        }

        #endregion Public Classes
    }
}
