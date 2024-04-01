using SPARC.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Translations;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace Sparc.Game.Beings {
    public class SparcPc : Pc, IEquipmentBeing {

        #region constants

        private const int MAX_SLOTS = 32;

        #endregion

        #region fields

        protected Equipment equipment = new Equipment();

        #endregion

        #region constructors

        public SparcPc(): base() {
            this.equipment = new Equipment();
            identity = Accusativ = Translator.Instance["you"];
            for (int i = 0; i < MAX_SLOTS; i++) {
                EquipmentSlot slot = new SparcSlot();
                equipment.Slots.Add(slot);
            }
        }

        #endregion

        #region properties

        public override int Health {
            get { return 10; }      // replace with actual logic
            set { }                 // replace with actual logic
        }

        public override int MaxHealth {
            get { return 10; }      // replace with actual logic
        }

        public override int Attack {
            get { return 10; }      // replace with actual logic
        }

        public override int Defense {
            get { return 10; }      // replace with actual logic
        }

        public override int DistanceAttack {
            get { return 10; }                 // replace with actual logic
        }

        public override void Move() {
        }

        public Equipment Equipment {
            get { return equipment; }
            set { equipment = value; }
        }

        #endregion

        #region public methods

        public override bool CanWalkOn(char c) {
            if (c == '&' || c == '\'') return true;
            return base.CanWalkOn(c);
        }

        #endregion

    }
}
