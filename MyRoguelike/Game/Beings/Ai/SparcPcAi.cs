﻿using SPARC.Display;
using SPARC.Game.Beings.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VH.Engine.Display;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.Translations;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings.AI;
using VH.Engine.World.Items;

namespace Sparc.Game.Beings.Ai {
    public class SparcPcAi : BaseAi {

        #region constructors
        public SparcPcAi(Pc pc) : base(pc) { }

        #endregion

        #region public methods

        public override AbstractAction SelectAction() {
            Pc pc = (Pc)Being;
            string command = ((SparcGameController)GameController.Instance).Command;
            AbstractAction action = null;
            //
            if (command == "wait") action = new WaitAction(pc);
            //
            else if (command == "north") action = new SparcMoveAction(pc, Step.NORTH);
            else if (command == "south") action = new SparcMoveAction(pc, Step.SOUTH);
            else if (command == "east") action = new SparcMoveAction(pc, Step.EAST);
            else if (command == "west") action = new SparcMoveAction(pc, Step.WEST);
            else if (command == "north-east") action = new SparcMoveAction(pc, Step.NORTH_EAST);
            else if (command == "north-west") action = new SparcMoveAction(pc, Step.NORTH_WEST);
            else if (command == "south-east") action = new SparcMoveAction(pc, Step.SOUTH_EAST);
            else if (command == "south-west") action = new SparcMoveAction(pc, Step.SOUTH_WEST);
            else if (command == "take-stairs") action = new TakeStairsAction(pc);
            else if (command == "close-door") action = new CloseDoorAction(pc);
            else if (command == "shoot") action = new SparcShootAction(pc);
            else if (command == "pick-up") action = new SparcPickupAction(pc);
            else if (command == "drop") action = new SparcDropAction(pc);
            else if (command == "activate") action = new ActivateAction(pc);
            else if (command == "execute") action = new ExecuteAction(pc);

            // add other actions below

            //

            return action;
        }

        public override bool InteractWithEnvironment(Position position) {
            if (new OpenDoorAction(Being, position).Perform()) return true;
            return base.InteractWithEnvironment(position);
        }

        public override object SelectTarget(object[] objects, AbstractAction action) {
            if (action is ShootAction) return selectDirection(objects);
            if (action is SparcDropAction || action is ActivateAction) {
                SparcSlotMenu sslmenu = new SparcSlotMenu();
                if (sslmenu.ShowMenu() == MenuResult.OK) return sslmenu.SelectedItem;
                return null;
            }
            LetterMenu lmenu = new LetterMenu(Translator.Instance["select-item"], objects, GameController.Instance.MessageWindow, true);
            if (lmenu.ShowMenu() == MenuResult.OK) return lmenu.SelectedItem;
            return null;
        }

        #endregion

        #region private methods

        private object selectDirection(object[] objects) {
            GameController.Instance.MessageManager.ShowMessage("which-direction", Being);
            char key = GameController.Instance.Console.ReadKey();
            Step direction = ((SparcGameController)GameController.Instance).Keybindings.GetStepForKey(key);
            return direction;
        }

        #endregion



    }
}
