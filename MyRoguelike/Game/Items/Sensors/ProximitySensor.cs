using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.LineOfSight;
using VH.Engine.Random;

namespace SPARC.Game.Items.Sensors {

    public class ProximitySensor: Sensor {

        #region constants

        private int MIN_VISION_RANGE = 1;
        private int MAX_VISION_RANGE = 2;

        #endregion

        #region constructors

        public ProximitySensor() : base() { 
            visionRange = Rng.Random.Next(MAX_VISION_RANGE - MIN_VISION_RANGE) + MIN_VISION_RANGE;
        }

        #endregion

        #region public methods

        public override AbstractFieldOfVision FoV {
            get { 
                if (fov == null) {
                    fov = new HardcodedShadowcastingFieldOfVision();
                }
                return fov;
            }
        }

        #endregion

    }
}
