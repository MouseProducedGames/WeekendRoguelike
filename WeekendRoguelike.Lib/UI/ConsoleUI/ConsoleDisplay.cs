using System;
using WeekendRoguelike.MapSystem;
using WeekendRoguelike.MapSystem.UI;
using WeekendRoguelike.Mob.Character;
using WeekendRoguelike.Mob.UI;

namespace WeekendRoguelike.UI.ConsoleUI
{
    public class ConsoleDisplay : Display<ConsoleDisplay.Graphics>
    {
        #region Public Constructors

        public ConsoleDisplay(string characterGraphicsFilename)
        {
            Console.SetWindowSize(80, 30);
            Console.SetBufferSize(80, 31);
            Console.CursorSize = 1;
            Console.CursorVisible = false;

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
            var data = item.Data;
            Console.SetCursorPosition(item.Position.X, item.Position.Y);
            Console.BackgroundColor = data.BackgroundColour;
            Console.ForegroundColor = data.ForegroundColour;
            Console.Write(data.Symbol);
        }

        #endregion Public Methods

        #region Public Structs

        public struct Graphics
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
                Console.SetCursorPosition(0, 0);
                for (int y = 0; y < length; ++y)
                {
                    Console.SetCursorPosition(0, y);
                    for (int x = 0; x < width; ++x)
                    {
                        Console.BackgroundColor = graphicsMap[y, x].BackgroundColour;
                        Console.ForegroundColor = graphicsMap[y, x].ForegroundColour;
                        Console.Write(graphicsMap[y, x].Symbol);
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
