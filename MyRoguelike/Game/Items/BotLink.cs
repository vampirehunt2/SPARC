using SPARC.Game.Beings;
using SPARC.Game.Beings.Ai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace SPARC.Game.Items {
    public class BotLink : Item, IActivable {

        #region fields

        private bool active;
        private SparcMonster bot = null;
        private Being owner = null;

        #endregion

        #region properties

        public SparcMonster Bot {
            get { return bot; }
            set { bot = value;
                ((BotAi)bot.Ai).Link = this;
            }
        }

        public Being Owner {
            get { return owner; }
            set { owner = value; }
        }

        public bool Active {
            get { return active; }
            set { active = value; }
        }

        public Being Activator {
            get { return owner; }
            set { }
        }

        #endregion

    }
}
