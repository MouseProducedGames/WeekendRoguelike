using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike.UI.ConsoleUI
{
    public class ConsoleListbox : Listbox
    {
        #region Public Methods

        public override void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Items.Length; ++i)
            {
                if (i != SelectedIndex)
                    Console.WriteLine("{0, 4}) {1}", (char)('a' + i), Items[i]);
                else
                    Console.WriteLine("-->{0}) {1}", (char)('a' + i), Items[i]);
            }
        }

        public override void Update()
        {
            var keyInfo = Input.GetInput;
            if (keyInfo.KeyChar >= 'a' &&
                keyInfo.KeyChar <= 'z')
            {
                SelectedIndex = keyInfo.KeyChar - 'a';
            }
            switch (keyInfo.Key)
            {
                case ConsoleKey.Enter: Confirm(); break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.NumPad2: ++SelectedIndex; break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.NumPad8: --SelectedIndex; break;
            }
            SelectedIndex = Math.Max(0, Math.Min(Items.Length - 1, SelectedIndex));

            Draw();
        }

        #endregion Public Methods
    }
}
