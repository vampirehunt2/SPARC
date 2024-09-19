using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Items;

namespace SPARC.Game.Items {
    public class SparcEquipment : Equipment {

        #region constructors

        public SparcEquipment() : base() { }

        #endregion

        #region properties

        public bool IsFull {
            get {
                foreach (EquipmentSlot slot in this) {
                    if (slot.Item == null) return false;
                }
                return true;
            }
        }

        #endregion

        #region public methods

        public bool ConsumeEnergy(int amount) {
            EnergySource activeEnergySource = (EnergySource)GetActiveItem(EnergySource.CHARACTER);
            if (activeEnergySource != null && activeEnergySource.Energy >= amount) {
                activeEnergySource.Energy -= amount;
                return true;
            }
            return false;
        }

        public bool ConsumeEnergy() {
            EnergySource activeEnergySource = (EnergySource)GetActiveItem(EnergySource.CHARACTER);
            if (activeEnergySource != null && activeEnergySource.Energy > 0) {
                activeEnergySource.Energy--;
                return true;
            }
            return false;
        }

        public Item GetActiveItem(char kind) {
           foreach (EquipmentSlot slot in Slots) {
                if (slot != null &&
                    slot.Item != null &&
                    slot.Item.Character == kind &&
                    slot.Item is IActivable &&
                    (slot.Item as IActivable).Active) { 
                    return slot.Item;
                }
            }
            return null;
        }

        public Item GetActiveItem(Type type) {
            foreach (EquipmentSlot slot in Slots) {
                if (slot != null &&
                    slot.Item != null &&
                    type.IsInstanceOfType(slot.Item)  &&
                    slot.Item is IActivable &&
                    (slot.Item as IActivable).Active) {
                    return slot.Item;
                }
            }
            return null;
        }

        public int GetTotalEnergy() {
            int en = 0;
            foreach (EquipmentSlot slot in Slots) {
                if (slot != null &&
                    slot.Item != null &&
                    slot.Item.Character == EnergySource.CHARACTER) {
                    en += (slot.Item as EnergySource).Energy;
                }
            }
            return en;
        }

        #endregion
    }
}
