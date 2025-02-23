using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Random;
using VH.Engine.World.Beings;

namespace SPARC.Game.Items.Disks {
    public class DiskOfCharging: Disk {

        public DiskOfCharging(): base() { }

        public override bool Execute(Being performer) {
            base.Execute(performer);
            if (performer == null) return false;
            if (!(performer is IEquipmentBeing)) return false;
            SparcEquipment equipment = (SparcEquipment)(performer as IEquipmentBeing).Equipment;
            if (equipment == null) return false;
            EnergySource energySource = (EnergySource)equipment.GetActiveItem(EnergySource.CHARACTER);
            if (energySource == null) return false;
            energySource.Energy += Rng.Random.Next(energySource.Energy);
            return true;
        }
    }
}
