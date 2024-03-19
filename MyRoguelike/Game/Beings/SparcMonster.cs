using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Display;
using VH.Engine.World.Beings;

namespace SPARC.Game.Beings {
    public class SparcMonster: Monster {
        public SparcMonster(): base() { }

        public override bool CanWalkOn(char c) {
            if (c == '&' || c == '\'') return true;
            return base.CanWalkOn(c);
        }
    }
}
