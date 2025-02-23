using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.LineOfSight;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace SPARC.Game.Items.Sensors {
    public abstract class Sensor : Item, IActivable {

        #region fields

        private bool active;
        protected AbstractFieldOfVision fov = null;
        protected int visionRange = 0;

        #endregion

        #region constructors

        public Sensor() { }

        #endregion

        #region properties

        public bool Active {
            get { return active; }
            set {
                active = value;
                // TODO: make this non-PC-centric
                if (active) {
                    Level level = GameController.Instance.Level;
                    if (level != null) {
                        GameController.Instance.FieldOfVision = FoV;
                        Map map = GameController.Instance.Map;
                        Position pos = GameController.Instance.Pc.Position;
                        FoV.ComputeFieldOfVision(map, pos, visionRange);
                    }
                }
            }
        }

        public abstract AbstractFieldOfVision FoV { get; }

        #endregion

        #region public methods

        public override string ToString() {
            string s = base.ToString();
            if (Active) s += " {a}";
            return s;
        }

        #endregion

    }
}
