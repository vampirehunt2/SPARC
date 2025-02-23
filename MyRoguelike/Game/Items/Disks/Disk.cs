using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace SPARC.Game.Items.Disks {
    public class Disk: Item {

        #region constants

        private const string HIDDEN_NAME = "hidden-name";

        #endregion

        #region fields

        private string hiddenName;

        #endregion

        #region properties

        public string HiddenName {
            get { return hiddenName; }
        }

        #endregion

        #region constructors

        public Disk() { }

        #endregion

        #region public methods

        public virtual bool Execute(Being performer) {
            return false;
        }

        public override void Create(XmlElement prototype) {
            base.Create(prototype);
            hiddenName = prototype.Attributes[HIDDEN_NAME].Value;
        }

        #endregion

    }
}
