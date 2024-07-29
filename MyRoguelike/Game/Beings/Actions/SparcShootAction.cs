using SPARC.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VH.Engine.Game;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;
using VH.Engine.World.Items.Weapons;

namespace SPARC.Game.Beings.Actions {
    public class SparcShootAction: ShootAction {

        #region constructors

        public SparcShootAction(Being performer, int range): base(performer, range) { }

        #endregion

        #region properties

        public override MissleWeapon MissleWeapon {
            get {
                if (!(performer is IEquipmentBeing)) return null;
                Equipment eq = (performer as IEquipmentBeing).Equipment;
                foreach (EquipmentSlot slot in  eq.Slots) {
                    if (slot.Item != null &&
                        slot.Item is SparcMissleWeapon &&
                        (slot.Item as SparcMissleWeapon).Active)
                        return slot.Item as SparcMissleWeapon;
                }
                return null;
            }    
        }

        #endregion

        #region protected methods

        protected override void missleStep() {
            GameController controller = GameController.Instance;
            char character = controller.ViewPort.GetDisplayCharacter(controller.Map[pos]);
            ConsoleColor color = controller.ViewPort.GetDisplayColor(controller.Map[pos]);
            ItemFacade facade = new ItemFacade();
            Ammo missleAnimation = new Ammo();
            missleAnimation.Character = getCharacter();
            missleAnimation.Color = MissleWeapon.Color;
            missleAnimation.Position = pos;
            controller.ViewPort.Display(missleAnimation, controller.FieldOfVision, performer.Position);
            controller.ViewPort.Refresh(pos);
            Thread.Sleep(100);
            /* uncomment for advancing missle.
             * leave commented for ray weapons
             * missleAnimation.Character = character;
            missleAnimation.Color = color;
            controller.ViewPort.Display(missleAnimation, controller.FieldOfVision, performer.Position);
            controller.ViewPort.Refresh(pos);*/
        }

        #endregion

        #region private methods

        private char getCharacter() {
            Being attackee = GameController.Instance.GetBeingAt(pos);
            if (!isShootable(pos)) {
                return '*';
            }
            if (attackee != null) {
                return '*';
            }
            if (step.X == 0) return '|';
            if (step.Y == 0) return '-';
            if (step.X * step.Y > 0) return '\\';
            return '/';
        }

        #endregion

    }
}
