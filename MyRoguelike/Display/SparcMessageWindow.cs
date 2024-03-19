using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Display;

namespace MyRoguelike.Display {
    public class SparcMessageWindow : ColorVerticalMessageWindow {

        private int counter = 0;
        private ConsoleColor color = ConsoleColor.Gray;

        public SparcMessageWindow(int x, int y, int width, int height, IConsole console): 
            base(x, y, width, height, console) {
        }

        public override void SetColor(ConsoleColor color) {
            this.color = color;
            base.SetColor(color);
        }

        public override void ShowMessage(string message) {
            base.ShowMessage("$" + counter.ToString("X4") + ": " + message);
            counter++;
            if (counter >= Math.Pow(2, 16)) counter = 0;
        }

    }
}
