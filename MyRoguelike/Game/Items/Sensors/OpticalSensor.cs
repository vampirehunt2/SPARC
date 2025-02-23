using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.LineOfSight;
using VH.Engine.Random;

namespace SPARC.Game.Items.Sensors {
    public class OpticalSensor: Sensor {

        #region constants

        private int MIN_VISION_RANGE = 5;
        private int MAX_VISION_RANGE = 10;

        #endregion

        #region constructors

        public OpticalSensor(): base() { 
            visionRange = Rng.Random.Next(MAX_VISION_RANGE -  MIN_VISION_RANGE) + MIN_VISION_RANGE;
        }

        #endregion

        #region public methods

        public override AbstractFieldOfVision FoV {
            get {
                if (fov == null) {
                    fov = new RaycastingFieldOfVision();
                }
                return fov;
            }
        }

        #endregion

    }
}
