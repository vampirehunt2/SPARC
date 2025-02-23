using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VH.Engine.Display;
using VH.Engine.Game;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace SPARC.Game.Items.Disks {
    public class DiskOfItemDestruction: Disk {

        public DiskOfItemDestruction() : base() { }

        public override bool Execute(Being performer) {
            base.Execute(performer);
            if (performer == null) return false;
            if (!(performer is IEquipmentBeing)) return false;
            SparcEquipment equipment = (SparcEquipment)(performer as IEquipmentBeing).Equipment;
            if (equipment == null) return false;
            int totalItems = 0;
            foreach (EquipmentSlot slot in equipment.Slots) {
                if (slot != null && slot.Item != null && !(slot.Item is Shield)) totalItems++;
            }
            if (totalItems == 0) return false;
            int selectedItem = Rng.Random.Next(totalItems);
            int itemIndex = 0;
            foreach (EquipmentSlot slot in equipment.Slots) {
                if (slot != null && slot.Item != null && !(slot.Item is Shield)) {
                    if (itemIndex == selectedItem) {
                        Item destroyedItem = slot.Item;
                        slot.Item = null;
                        performer.Ai.Notify("item-destroyed");
                        break;
                    }
                    itemIndex++;
                }
            }
            return true;
        }

    }
}
