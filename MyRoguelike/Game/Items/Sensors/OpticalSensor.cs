using SPARC.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Display;
using VH.Engine.Game;
using VH.Engine.Levels;
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

        #region properties

        public override bool Active {
            get { return base.Active; }
            set { 
                base.Active = value;
                if (value) {
                    if (Activator != null && Activator.Person == Person.Second) {
                        ViewPort viewPort = GameController.Instance.ViewPort;
                        if (viewPort != null) {
                            viewPort.Shade = true;
                            viewPort.ShadeColor = ConsoleColor.DarkGray;
                        }
                    }
                }
            }
        }

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
