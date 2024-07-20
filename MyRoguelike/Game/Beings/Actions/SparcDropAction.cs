using Sparc.Game;
using SPARC.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Display;
using VH.Engine.Game;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;

namespace SPARC.Game.Beings.Actions {
    public class SparcDropAction : AbstractAction {
        public SparcDropAction(Being performer) : base(performer) {
        }

        public override bool Perform() {
            SparcSlotMenu menu = new SparcSlotMenu();
            if (menu.ShowMenu() == MenuResult.OK) {
                SparcGameController ctrl = (SparcGameController)GameController.Instance;
                Item item = menu.Slot.Item;
                // ctrl.MessageManager.ShowMessage("drop", performer, item, force);
                item.Position = ctrl.Pc.Position;
                menu.Slot.Item = null;
                ctrl.Level.Items.Add(item);
                ctrl.ExpansionSlotsWindow.Refresh();
                return true;
            }
            return false;
        }
    }
}
