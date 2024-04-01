using SPARC.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Display;
using VH.Engine.World.Items;

namespace SPARC.Display {
    public class ExpansionSlotsWindow: Window {

        private Equipment equipment;

        public ExpansionSlotsWindow(): base() { }

        public ExpansionSlotsWindow(int x, int y, int width, int height, IConsole console, Equipment equipment):
            base(x, y, width, height, console) {
            this.equipment = equipment;
            Refresh();
        }

        public void Refresh() {
            StringBuilder sb = new StringBuilder();
            for (int l = 0; l < width; ++l) sb.Append(' '); 
            for (int i = 0; i < equipment.Slots.Count; ++i) {
                Write(i, sb.ToString());
                ConsoleColor color = ConsoleColor.DarkGray;
                Item item = equipment.Slots[i].Item;
                if (item != null) color = item.Color;
                console.ForegroundColor = color;
                Write(i, equipment.Slots[i].ToString());
            }
        }

        private void Write(int y, string s) {
            int i = 0;
            while (i < s.Length && i < this.width) {
                Write(s[i++], i, y);
            }
        }
    }
}
