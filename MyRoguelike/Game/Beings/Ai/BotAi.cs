using SPARC.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings.AI;

namespace SPARC.Game.Beings.Ai {
    public class BotAi : BaseAi {

        #region fields

        private bool initialised = false;
        private BotLink link = null;
        private NeutralBehavior neutralBehavior;
        private FollowBehaviour followBehaviour;

        #endregion

        #region constructors

        public BotAi(): base() {
        }

        public BotAi(Being being): base(being) {
            
        }

        #endregion

        #region properties

        public BotLink Link {
            get { return link; } 
            set { link = value; }
        }

        public Being Owner {
            get { return link.Owner; }
        }



        #endregion

        #region public methods
        public override AbstractAction SelectAction() {
            if (!initialised) initialise();
            if (link == null) return neutralBehavior.SelectAction();
            if (!link.Active) return neutralBehavior.SelectAction();
            if (link.Owner == null) return neutralBehavior.SelectAction();
            followBehaviour.FollowTarget = Owner;
            return followBehaviour.SelectAction();
        }

        #endregion

        #region private methods

        private void initialise() {
            neutralBehavior = new NeutralBehavior(Being);
            followBehaviour = new FollowBehaviour(Being);
            initialised = true;
        }

        #endregion
    }
}
