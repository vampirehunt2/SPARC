using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Random;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using SPARC.Game.Items;

namespace SPARC.Game.Beings.Actions {
    public class SparcMissleAttackAction : AbstractAttackAction {

        #region fields

        SparcMissleWeapon weapon;

        #endregion

        #region properties
        protected override int Attack => Math.Max(Rng.Random.Next(weapon.Attack), 1);

        protected override int Defense => Rng.Random.Next(attackee.Defense);

        #endregion

        #region constructors

        public SparcMissleAttackAction(Being performer)
            : base(performer) {
        }

        public SparcMissleAttackAction(Being performer, Being attackee, SparcMissleWeapon weapon)
            : base(performer) {
            this.weapon = weapon;
            base.attackee = attackee;
        }

        #endregion
    }
}
