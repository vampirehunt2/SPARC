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
            if (!(performer is IEquipmentBeing)) return false;
            Equipment equipment = (performer as IEquipmentBeing).Equipment;
            EquipmentSlot slot = (EquipmentSlot)performer.Ai.SelectTarget(equipment.Slots.ToArray(), this);
            if (slot == null) return false;
            Item item = slot.Item;
            if (item == null) return false;
            SparcGameController ctrl = (SparcGameController)GameController.Instance;
            // ctrl.MessageManager.ShowMessage("drop", performer, item, force);
            item.Position = performer.Position;
            slot.Item = null;
            ctrl.Level.Items.Add(item);
            ctrl.ExpansionSlotsWindow.Refresh();
            return true;
        }
    }
}
