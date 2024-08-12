using SPARC.Display;
using Sparc.Game;
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
using SPARC.Game.Items;
using Sparc.Game.Beings;

namespace SPARC.Game.Beings.Actions {
    public class ActivateAction: AbstractAction {


        public ActivateAction(Being performer) : base(performer) {
        }

        public override bool Perform() {
            if (!(performer is IEquipmentBeing)) return false;
            Equipment equipment = (performer as IEquipmentBeing).Equipment;
            EquipmentSlot slot = (EquipmentSlot)performer.Ai.SelectTarget(equipment.Slots.ToArray(), this);
            if (slot == null) return false;
            Item item = slot.Item;
            if (item == null) return false;
            if (!(item is IActivable)) return false;
            SparcGameController ctrl = (SparcGameController)GameController.Instance;
            (item as IActivable).Active = true;
            for (int i = 0; i < equipment.Slots.Count; ++i) {
                if (equipment.Slots[i] != slot &&
                    equipment.Slots[i].Item != null &&
                    equipment.Slots[i].Item is IActivable &&
                    equipment.Slots[i].Item.Character == item.Character) {
                    (equipment.Slots[i].Item as IActivable).Active = false;
                }
            }
            ctrl.MessageManager.ShowMessage("activate", performer, item);
            return true;
        }
    }
}
