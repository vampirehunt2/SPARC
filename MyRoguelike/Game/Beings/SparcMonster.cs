using SPARC.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Display;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace SPARC.Game.Beings {
    public class SparcMonster: Monster {

        private const float DROP_RATE = 0.5f;

        public SparcMonster(): base() { }

        public override bool CanWalkOn(char c) {
            if (c == '&' || c == '\'') return true;
            return base.CanWalkOn(c);
        }

        public override void Kill() {
            base.Kill();
            if (this is IEquipmentBeing) {  // drop loot
                foreach (EquipmentSlot slot in (this as IEquipmentBeing).Equipment.Slots) {
                    if (slot != null && slot.Item != null & Rng.Random.NextFloat() > DROP_RATE) {
                        slot.Item.Position = Position;
                        if (slot.Item is IActivable) ((IActivable)slot.Item).Active = false;
                        GameController.Instance.Level.Items.Add(slot.Item);
                    }
                }
            }
        }

    }
}
