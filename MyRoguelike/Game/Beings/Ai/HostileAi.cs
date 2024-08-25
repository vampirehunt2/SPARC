using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings.AI;
using VH.Engine.World.Beings;
using System.Drawing;
using SPARC.Game.Items;
using SPARC.Game.Beings.Actions;

namespace SPARC.Game.Beings.Ai {
    public class HostileAi : BaseAi {

        #region constructors

        public HostileAi() : base() { }

        public HostileAi(Being being) : base(being) { }

        #endregion

        #region public methods

        public override AbstractAction SelectAction() {
            // try to find oponent
            Being oponent = getOponent();
            if (oponent != null) {
                if (isAdjacentTo(oponent)) return new AttackAction(Being, oponent);
                else if (canShoot()) return new SparcShootAction(Being);
                else return new MoveAction(Being, getStepTowards(getPossibleSteps(Being, oponent.Position)));
            }
            // try to move in a random direction
            Step step = Step.CreateRandomStep();
            Position position = Being.Position.AddStep(step);
            if (GameController.Instance.IsFreeSpace(position, Being) ||
                GameController.Instance.Level.Map[position] == Terrain.Get("closed-door").Character) {
                return new MoveAction(Being, step);
            }
            // finally, just hang around
            else return new WaitAction(Being);
        }

        public override object SelectTarget(object[] objects, AbstractAction action) {
            if (action is SparcShootAction) {
                Being oponent = getOponent();
                if (oponent != null) return getStep();
            }
            return base.SelectTarget(objects, action);
        }

        #endregion

        #region protected methods

        protected bool canShoot() {
            if (getOponent() == null) return false;
            if (!(Being is IEquipmentBeing)) return false;
            SparcEquipment eq = (SparcEquipment)(Being as IEquipmentBeing).Equipment;
            if (eq == null) return false;
            SparcMissleWeapon weapon = (SparcMissleWeapon)eq.GetActiveItem(SparcMissleWeapon.CHARACTER);
            if (weapon == null) return false;
            return getStep() != Step.NONE;
        }

        protected Step getStep() {
            Being oponent = getOponent();
            if (oponent == null) return null;
            int deltaX = oponent.Position.X - Being.Position.X;
            int deltaY = oponent.Position.Y - Being.Position.Y;
            if (deltaX == 0) {
                if (deltaY > 0) return Step.SOUTH;
                else return Step.NORTH;
            }
            if (deltaY == 0) {
                if (deltaX > 0) return Step.EAST;
                else return Step.WEST;
            }
            if (deltaX == deltaY) {
                if (deltaX > 0) return Step.SOUTH_EAST; 
                else return Step.NORTH_WEST;
            }
            if (deltaX == -deltaY) {
                if (deltaX > 0) return Step.NORTH_EAST; 
                else return Step.SOUTH_WEST;
            }
            return Step.NONE;
        }
            
        #endregion
    }
}
