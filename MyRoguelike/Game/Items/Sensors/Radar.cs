using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.LineOfSight;

namespace SPARC.Game.Items.Sensors {
    public class Radar: Sensor {

        public Radar(): base() { }

        public override AbstractFieldOfVision FoV {
            get {
                if (fov == null) {
                    fov = new RaycastingFieldOfVision(); // TODO
                }
                return fov;
            }
        }

    }
}
