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
            return new GraphicsWrapperImpl(CreateGraphicsData<Graphics>());
        }

        public override Listbox CreateListbox()
        {
            return new ConsoleListbox();
        }

        public void Draw(Graphics item)
        {
            Console.SetCursorPosition(item.Position.X, item.Position.Y);
            Console.BackgroundColor = item.BackgroundColour;
            Console.ForegroundColor = item.ForegroundColour;
            Console.Write(item.Symbol);
        }

        #endregion Public Methods

        #region Public Structs

        public struct Graphics
        {
            #region Private Fields

            private ConsoleColor backgroundColour;
            private ConsoleColor foregroundColour;
            private Point position;
            private char symbol;

            #endregion Private Fields

            #region Public Constructors

            public Graphics(
                Point position,
                char symbol,
                ConsoleColor foregroundColour,
                ConsoleColor backgroundColour
                )
            {
                this.position = position;
                this.symbol = symbol;
                this.backgroundColour = backgroundColour;
                this.foregroundColour = foregroundColour;
            }

            #endregion Public Constructors

            #region Public Properties

            public ConsoleColor BackgroundColour { get => backgroundColour; set => backgroundColour = value; }
            public ConsoleColor ForegroundColour { get => foregroundColour; set => foregroundColour = value; }
            public Point Position { get => position; set => position = value; }
            public char Symbol { get => symbol; set => symbol = value; }

            #endregion Public Properties
        }

        #endregion Public Structs

        #region Public Classes

        public class CharacterGraphicsWrapperImpl : CharacterGraphicsWrapper
        {
            #region Public Constructors

            public CharacterGraphicsWrapperImpl(GraphicsData data) : base(data)
            {
            }

            #endregion Public Constructors

            #region Public Methods

            public override void Draw()
            {
                Display.GetInstanceAs<ConsoleDisplay>().Draw(Data.Item);
            }

            public override void Update(Character forCharacter)
            {
                data.item.Position = forCharacter.Position;
            }

            #endregion Public Methods
        }

        public class GraphicsWrapperImpl : GraphicsWrapper
        {
            #region Public Constructors

            public GraphicsWrapperImpl(GraphicsData data) : base(data)
            {
            }

            #endregion Public Constructors

            #region Public Methods

            public override void Draw()
            {
                Display.GetInstanceAs<ConsoleDisplay>().Draw(Data.Item);
            }

            #endregion Public Methods
        }

        #endregion Public Classes
    }
}
