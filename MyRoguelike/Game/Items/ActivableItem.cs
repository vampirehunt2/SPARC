using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Items;

namespace SPARC.Game.Items {

    public interface IActivable {

        #region properties

        bool Active {
            get;
            set;
        }

        #endregion

    }
}
