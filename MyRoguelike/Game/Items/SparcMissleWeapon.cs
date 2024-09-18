using SPARC.Game.Beings.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items.Weapons;

namespace SPARC.Game.Items {
    public class SparcMissleWeapon : MissleWeapon, IActivable {

        #region constants

        public const char CHARACTER = '/';

        #endregion

        #region fields

        private bool active;
        private int range;

        #endregion

        #region constructors

        public SparcMissleWeapon() { }

        #endregion

        #region properties

        public bool Active {
            get => active;
            set => active = value;
        }

        public int Range {
            get => range;   
            set => range = value;
        }

        #endregion

        #region public methods

        public virtual bool Fire(Being shooter) {
            if (shooter == null) return false;
            if (!(shooter is IEquipmentBeing)) return true; // let regular monsters shoot indefinitely at no cost
            SparcEquipment equipment = (SparcEquipment)(shooter as IEquipmentBeing).Equipment;
            if (equipment == null) return false;
            int cost = Attack;
            EnergySource source = (EnergySource)equipment.GetActiveItem(EnergySource.CHARACTER);
            if (source == null) return false;
            if (source.Energy < cost) return false;
            source.Energy -= cost;
            return true;
        }

        public override void Create(XmlElement prototype) {
            base.Create(prototype);
            range = int.Parse(prototype.Attributes["range"].Value);
        }

        public override string ToString() {
            string s = base.ToString();
            if (Active) s += " {a}";
            return s;
        }


        #endregion
    }
}
