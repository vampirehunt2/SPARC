using Sparc.Game.Beings.Actions;
using SPARC.Game.Items;
using SPARC.Game.Items.Disks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VH.Engine.Display;
using VH.Engine.Game;
using VH.Engine.Random;
using VH.Engine.Translations;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace SPARC.Game.Beings.Actions {
    public class ExecuteAction : SparcAction {

        #region constants

        private const float DISK_DESTRUCTION_RATE = 0.5f;

        #endregion

        #region fields

        private Disk disk;

        #endregion

        #region constructors

        public ExecuteAction(Being performer) : base(performer) {
        }

        #endregion

        #region public methods

        public override bool Perform() {
            if (performer == null) return false; 
            if (!(performer is IEquipmentBeing)) return false;
            SparcEquipment equipment = (performer as IEquipmentBeing).Equipment as SparcEquipment;
            if (equipment == null) return false;
            EquipmentSlot slot = (EquipmentSlot)performer.Ai.SelectTarget(equipment.Slots.ToArray(), this);
            if (slot == null) return false;
            Item item = slot.Item;
            if (item == null) return false;
            if (!(item is Disk)) {
                GameController.Instance.MessageManager.ShowMessage("not-a-disk", performer, item);
                return false;
            }
            disk = (Disk)item;
            Computer computer = (Computer)(equipment).GetActiveItem(Computer.CHARACTER);
            if (computer == null) {
                notify("no-computer", disk);
                return false;
            }
            string diskName = disk.Name;
            if (Rng.Random.NextFloat() < computer.IdPercentage) diskName = disk.HiddenName;
            GameController.Instance.MessageManager.ShowDirectMessage(diskName);
            YesNoMenu menu = new YesNoMenu(Translator.Instance["execute"], GameController.Instance.MessageWindow, 'Y', 'N');
            if (menu.ShowMenu() == MenuResult.Cancel) return false;
            float execute = Rng.Random.NextFloat();
            if ( execute < computer.ExecutePercentage) {
                if (disk.Execute(performer)) {
                    slot.Item = null;
                } else {
                    notify("fail-execute", disk);
                    destroyDisk(slot);
                }
            } else {
                notify("fail-execute", disk);
                destroyDisk(slot);
            }
            return true;
        }

        #endregion

        #region private methods

        private void destroyDisk(EquipmentSlot slot) {
            if (Rng.Random.NextFloat() < DISK_DESTRUCTION_RATE) {
                slot.Item = null;
                performer.Ai.Notify("disk-destroyed");
            }
        }

        #endregion

    }
}
