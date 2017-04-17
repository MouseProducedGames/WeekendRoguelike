using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
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

            public void Update(Character forCharacter)
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

        #endregion Public Classes
    }
}
