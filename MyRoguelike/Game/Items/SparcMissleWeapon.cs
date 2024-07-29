using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VH.Engine.World.Items.Weapons;

namespace SPARC.Game.Items {
    public class SparcMissleWeapon : MissleWeapon, IActivable {

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
