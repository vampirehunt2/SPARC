using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace SPARC.Game.Items {
    public class Shield : Item, IActivable {

        #region constants

        public const char CHARACTER = ']';
        public const int FULL = 100;


        #endregion

        #region fields

        private int energy;
        private bool active;

        #endregion

        #region constructors

        public Shield(): base() { }

        #endregion

        #region properties

        public Being Activator {
            get { return null; }
            set { }
        }

        public bool Active {
            get => active;
            set => active = value;
        }

        public int Energy {
            get => energy;
            set => energy = value;
        }

        #endregion

        public override string ToString() {
            string s = base.ToString();
            if (Active) s += " {a}";
            return s;
        }

    }
}
