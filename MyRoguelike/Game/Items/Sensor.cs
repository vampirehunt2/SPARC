using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Items;

namespace SPARC.Game.Items {
    public class Sensor : Item, IActivable {

        private bool active;
        public Sensor() { }
        public bool Active {
            get => active; 
            set => active = value; 
        }
    }
}
