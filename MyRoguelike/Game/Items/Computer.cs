using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VH.Engine.World.Items;

namespace SPARC.Game.Items {

    public class Computer: Item, IActivable {

        #region constants

        private const string ID_PERCENTAGE = "id-percentage";
        private const string EXECUTE_PERCENTAGE = "execute-percentage";
        public const char CHARACTER = '}';

        #endregion

        #region fields

        private bool active;
        private float idPercentage;
        private float executePercentage;

        #endregion

        #region constructors

        public Computer() { }

        #endregion

        #region properties

        public bool Active {
            get => active;
            set => active = value;
        }

        public float IdPercentage {
            get => idPercentage;
            set => idPercentage = value;
        }

        public float ExecutePercentage {
            get => executePercentage;
            set => executePercentage = value;
        }

        #endregion

        #region public methods

        public override string ToString() {
            string s = base.ToString();
            if (Active) s += " {a}";
            return s;
        }

        public override void Create(XmlElement prototype) {
            base.Create(prototype);
            idPercentage = float.Parse(prototype.Attributes[ID_PERCENTAGE].Value) / 100f;
            executePercentage = float.Parse(prototype.Attributes[EXECUTE_PERCENTAGE].Value) / 100f;
        }

        #endregion
    }
}
