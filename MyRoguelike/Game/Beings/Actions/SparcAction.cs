using SPARC.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;

namespace Sparc.Game.Beings.Actions {
    public class SparcAction : AbstractAction {
        public SparcAction(Being performer) : base(performer) {}

        public override bool Perform() {
            if (performer is IEquipmentBeing) {
                SparcEquipment eq = (SparcEquipment)(performer as IEquipmentBeing).Equipment;
                Shield shield = (Shield)eq.GetActiveItem(Shield.CHARACTER);
                if (shield != null) {
                    eq.ConsumeEnergy();
                    if (shield.Energy < Shield.FULL) {
                        if (eq.ConsumeEnergy()) shield.Energy++;
                    }
                }
                
            }
            return true;
        }
    }
}
