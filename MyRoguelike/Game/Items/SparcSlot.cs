using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Items;

namespace SPARC.Game.Items {
    public class SparcSlot : EquipmentSlot {

        public SparcSlot() {
            id = "slot";
        }

        public override bool IsItemCompatible(Item item) {
            return true;
        }

        public override string ToString() {
            if (item == null) {
                return "[" + Name + "]";
            }
            return item.ToString();

        }
    }
}
