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
using VH.Engine.Random;

namespace SPARC.Game.Beings.Ai {
    public class HostileAi : BaseAi {

        #region constants

        private const float SHOOT_RATE = 0.5f;
        private const float RANDOM_MOVE_RATE = 0.1f;

        #endregion


        #region constructors

        public HostileAi() : base() { }

        public HostileAi(Being being) : base(being) { }

        #endregion

        #region public methods

        public override AbstractAction SelectAction() {
            if (canPickup()) return new SparcPickupAction(Being);
            // try to find oponent
            Being oponent = getOponent();
            if (oponent != null) { 
                if (isAdjacentTo(oponent)) { // if adjacent to the oponent, choose mellee or missle attack at random
                    if (Rng.Random.NextFloat() < SHOOT_RATE) return new SparcShootAction(Being);
                    else return new AttackAction(Being, oponent);
                } else if (canShoot()) {
                    return new SparcShootAction(Being);
                } else {
                    if (canPickup()) return new SparcPickupAction(Being);
                    if (Rng.Random.NextFloat() < 1 - RANDOM_MOVE_RATE) { // sometimes, move randomly, even in pursuit
                        return new MoveAction(Being, getStepTowards(getPossibleSteps(Being, oponent.Position)));
                    }
                }
            }
            // 
            
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
            if (action is PickUpAction) {
                return GameController.Instance.Level.GetItemsAt(Being.Position).First();
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

        protected bool canPickup() {
            if (!(Being is IEquipmentBeing)) return false;
            SparcEquipment equipment = (SparcEquipment)(Being as IEquipmentBeing).Equipment;
            if (equipment == null) return false;
            if (equipment.IsFull) return false;
            if (GameController.Instance.Level.GetItemsAt(Being.Position).Count() == 0) return false;
            return true;
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
