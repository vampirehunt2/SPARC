using SPARC.Game.Beings;
using SPARC.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.Translations;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace Sparc.Game.Beings {
    public class SparcPc : Pc, IEquipmentBeing {

        #region constants

        public const int MAX_SLOTS = 32;

        #endregion

        #region fields

        protected Equipment equipment = new Equipment();

        #endregion

        #region constructors

        public SparcPc(): base() {
            this.equipment = new SparcEquipment();
            identity = Accusativ = Translator.Instance["you"];
            for (int i = 0; i < MAX_SLOTS; i++) {
                EquipmentSlot slot = new SparcSlot();
                equipment.Slots.Add(slot);
            }
            ItemFacade facade = new ItemFacade();
            equipment.Slots[0].Item = facade.CreateItemById("proximity-sensor");
            (equipment.Slots[0].Item as IActivable).Active = true;
            equipment.Slots[1].Item = facade.CreateItemById("phaser");
            (equipment.Slots[1].Item as IActivable).Active = true;
            equipment.Slots[2].Item = facade.CreateItemById("lead-battery");
            (equipment.Slots[2].Item as IActivable).Active = true;
            equipment.Slots[3].Item = facade.CreateItemById("shield-generator");
            equipment.Slots[4].Item = facade.CreateItemById("optical-sensor");

            // bot setup
            MonsterFacade monsterFacade = new MonsterFacade();
            SparcMonster bot = (SparcMonster)monsterFacade.CreateMonster("//monster[@id='recon-bot']");
            BotLink link = (BotLink)facade.CreateItemById("bot-link");
            link.Bot = bot;
            equipment.Slots[5].Item = link;
            link.Owner = this;
            link.Active = true;
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
