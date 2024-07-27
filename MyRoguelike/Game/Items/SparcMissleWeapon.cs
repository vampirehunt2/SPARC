using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Items.Weapons;

namespace SPARC.Game.Items {
    public class SparcMissleWeapon : MissleWeapon, IActivable {

        private bool active;

        public SparcMissleWeapon() { }
        public bool Active {
            get => active;
            set => active = value;
        }

        public override string ToString() {
            string s = base.ToString();
            if (Active) s += " {a}";
            return s;
        }
    }
}
