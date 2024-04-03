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
            IEquipmentBeing being = performer as IEquipmentBeing;
            Equipment equipment = being.Equipment;
            for (int i = 0; i < equipment.Slots.Count; ++i) {
                if (equipment.Slots[i].Item == null) {
                    equipment.Slots[i].Item = item;
                    for (int k = 0; k < GameController.Instance.Level.Items.Count; ++k) {
                        if (ReferenceEquals(GameController.Instance.Level.Items[k], item)) {
                            GameController.Instance.Level.Items.RemoveAt(k);
                            break;
                        }
                    }
                    (GameController.Instance as SparcGameController).ExpansionSlotsWindow.Refresh();
                    return true; 
                }
            }
            notify("no-slots");
            return false;
        }
    }
}
