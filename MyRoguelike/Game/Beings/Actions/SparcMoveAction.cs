using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace SPARC.Game.Beings.Actions {
    public class SparcMoveAction : MoveAction {
        public SparcMoveAction(Being performer, Step step) : base(performer, step) {
        }

        public override bool Perform() {
            bool result = base.Perform();
            if (result) { showFeatureNames(); }
            return result;
        }

        private void showFeatureNames() {
            char feature = GameController.Instance.Level.Map[performer.Position];
            if (feature == Terrain.Get("downstair").Character) {
                notify("pass-elevator");
            }
        }

    }
}
