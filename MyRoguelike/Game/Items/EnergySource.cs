using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VH.Engine.Random;
using VH.Engine.World.Items;

namespace SPARC.Game.Items {
    public class EnergySource: Item, IActivable {

        #region constants

        public const char CHARACTER = '!';

        #endregion

        #region fields

        private int energy;
        private bool active;

        #endregion

        #region constructors

        public EnergySource(): base() { }

        #endregion

        #region properties

        public int Energy { 
            get { return energy; } 
            set {  energy = value; } 
        }

        public bool Active {
            get { return active; }
            set { active = value; }  
        }

        #endregion

        #region public methods
        public override void Create(XmlElement prototype) {
            base.Create(prototype);
            int maxEnergy = int.Parse(prototype.Attributes["max-energy"].Value);
            this.energy = Rng.Random.Next(maxEnergy / 2) + maxEnergy / 2;
        }

        public override string ToString() {
            string s = base.ToString() + " (" + Energy + ")";
            if (Active) s += " {a}";
            return s;
        }

        #endregion
    }
}
