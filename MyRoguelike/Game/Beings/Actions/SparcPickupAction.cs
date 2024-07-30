using Sparc.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Game;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;

namespace SPARC.Game.Beings.Actions {
    public class SparcPickupAction : PickUpAction {

        

        public SparcPickupAction(Being performer) : base(performer) {
        }

        public override bool PickUp() {
            if (!(performer is IEquipmentBeing)) return false;
            SparcGameController ctrl = (SparcGameController)GameController.Instance;
            IEquipmentBeing being = performer as IEquipmentBeing;
            Equipment equipment = being.Equipment;
            for (int i = 0; i < equipment.Slots.Count; ++i) {
                if (equipment.Slots[i].Item == null) {
                    equipment.Slots[i].Item = item;
                    for (int k = 0; k < ctrl.Level.Items.Count; ++k) {
                        if (ReferenceEquals(GameController.Instance.Level.Items[k], item)) {
                            ctrl.Level.Items.RemoveAt(k);
                            break;
                        }
                    }
                    (GameController.Instance as SparcGameController).ExpansionSlotsWindow.Refresh();
                    ctrl.MessageManager.ShowMessage("pick-up", performer, item);
                    return true; 
                }
            }
            notify("no-slots");
            return false;
        }
    }
}
