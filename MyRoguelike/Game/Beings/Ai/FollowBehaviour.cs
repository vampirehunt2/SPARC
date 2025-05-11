using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Levels;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings.AI;

namespace SPARC.Game.Beings.Ai {
    public class FollowBehaviour : BaseAi {

        #region constants

        private const int MAX_DISTANCE = 5;

        #endregion

        #region fields

        private Being followTarget = null;
        private NeutralBehavior neutralBehavior = null;

        #endregion

        #region constructors

        public FollowBehaviour(Being being): base(being) { 
            neutralBehavior = new NeutralBehavior(being);
        }

        #endregion

        #region properties

        public Being FollowTarget {
            get { return followTarget; }
            set { followTarget = value; }
        }

        #endregion


        #region public methods

        public override AbstractAction SelectAction() {
            if (followTarget == null) return neutralBehavior.SelectAction();
            if (isClose()) return neutralBehavior.SelectAction();
            return follow();
        }

        #endregion

        #region private methods

        private bool isClose() {
            int xDiff = Math.Abs(Being.Position.X - followTarget.Position.X);   
            int yDiff = Math.Abs(Being.Position.Y - followTarget.Position.Y);
            int maxDiff = Math.Max(xDiff, yDiff);
            return maxDiff <= MAX_DISTANCE;
        }

        private MoveAction follow() {
            int xDiff = followTarget.Position.X - Being.Position.X;
            int yDiff = followTarget.Position.Y - Being.Position.Y;
            Step step = new Step(Math.Sign(xDiff), Math.Sign(yDiff));
            return new MoveAction(Being, step);
        }

        #endregion
    }
}
