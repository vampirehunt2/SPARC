using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Display;
using VH.Engine.Game;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Sparc.Game.Beings;
using VH.Engine.World.Items;
using Menu = VH.Engine.Display.Menu;
using VH.Engine.World.Beings;

namespace SPARC.Display {

    public class SparcSlotMenu : Menu {

        GameController ctrl = GameController.Instance;
        EquipmentSlot slot = null;

        public EquipmentSlot Slot {
            get { return slot; }
        }

        public override MenuResult ShowMenu() {
            base.objects = (ctrl.Pc as IEquipmentBeing).Equipment.Slots.ToArray(); 
            ctrl.Console.ForegroundColor = ConsoleColor.Gray;
            ctrl.MessageManager.ShowDirectMessage("select-slot");
            ctrl.Console.Refresh();
            string s = ctrl.Console.ReadLine();
            int slotNum;
            try {
                slotNum = int.Parse(s, NumberStyles.HexNumber);
            } catch {
                showInvalidSlot();
                return MenuResult.Cancel;
            }
            if (slotNum < 0 || slotNum >= SparcPc.MAX_SLOTS) {
                showInvalidSlot();
                return MenuResult.Cancel;
            }
            slot = ((SparcPc)ctrl.Pc).Equipment[slotNum];
            if (slot == null) {
                showInvalidSlot();
                return MenuResult.Cancel;
            }
            Item item = slot.Item;
            if (item == null) {
                showInvalidSlot();
                return MenuResult.Cancel;
            }

            return MenuResult.OK;
        }

        private void showInvalidSlot() {
            ctrl.MessageManager.ShowDirectMessage("invalid-slot");
        }

    }
}
