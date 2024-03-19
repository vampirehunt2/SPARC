using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Translations;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace Sparc.Game.Beings {
    public class SparcPc : Pc, IEquipmentBeing {

        public SparcPc(): base() {
            identity = Accusativ = Translator.Instance["you"];
        }

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

        public override bool CanWalkOn(char c) {
            if (c == '&' || c == '\'') return true;
            return base.CanWalkOn(c);
        }

        public override string Name { get => base.Name; set => base.Name = value; }

        public Equipment Equipment => throw new NotImplementedException();
    }
}
