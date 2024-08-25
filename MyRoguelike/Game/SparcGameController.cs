using MyRoguelike.Display;
using Sparc.Game.Beings;
using Sparc.Game.Beings.Actions;
using Sparc.Game.Beings.Ai;
using SPARC.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VH.Engine.Configuration;
using VH.Engine.Display;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.LineOfSight;
using VH.Engine.Translations;
using VH.Engine.VhConsole;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;
using VH.Game.World.Beings;
using VH.Game.World.Items;

namespace Sparc.Game {
    public class SparcGameController : GameController {

        #region fields

        protected Keybindings keybindings = Keybindings.Instance;
        private string command;
        private ExpansionSlotsWindow expansionSlotsWindow;

        #endregion

        #region properties

        public Keybindings Keybindings {
            get { return keybindings; }
        }

        public ExpansionSlotsWindow ExpansionSlotsWindow { 
            get { return expansionSlotsWindow; } 
        }

        public string Command {
            get { return command; }
        }

        #endregion

        #region protected methods

        protected override void runTurn() {
            base.runTurn();

            // display the whole game screen
            reDraw();

            // select next action
            GameController.Instance.Console.ClearBuffer();
            char c = GameController.Instance.Console.ReadKey();
            AbstractAction action = null; // resets the action on each turn
            command = keybindings[c];

            // in-game actions. 
            // these are actions performed by the pc.
            action = pc.Ai.SelectAction();
            // perform selected pc action, if there is one.
            if (action != null && action.Perform()) {
                if (action is MoveAction) reDraw();
                gametimeTicks = (int)(action.TimeNeeded / pc.Speed);
                moveMonsters();
                Pc.Move();
                runBaseAction(Pc);
                ExpansionSlotsWindow.Refresh();
            } else {
                // out-of-game actions. 
                // these are actions performed by the player.
                // these actions do not take up gametime.
                if (Command == "show-help") {
                    messageWindow.Clear();
                    console.ForegroundColor = ConsoleColor.Gray;
                    messageWindow.ShowMessage("\n" + Keybindings.ToString());
                } 
                else if (Command == "quit") {
                    string msg = Translator.Instance["quit?"];
                    if (new YesNoMenu(msg, messageWindow, 'Y', 'N').ShowMenu() == MenuResult.OK) {
                        QuitGame = true;
                    }
                    messageWindow.Clear();
                }
            }
        }

        protected override void runBaseAction(Being performer) {
            new SparcAction(performer).Perform();
        }

        protected override void setUpGame() {
            // intialise the PC
            pc = new SparcPc();
            pc.Character = '@';
            pc.Ai = new SparcPcAi(pc);

            // initialise the console
            console = new VhConsole();  // this type is a fullscreen console
            console.CursorVisible = false;
            console.Height = 50;
            console.Width = 120;
            console.Clear();
            console.GoTo(0, 0);

            // initialise the display
            console.ForegroundColor = ConsoleColor.Blue;
            fieldOfVision = new HardcodedShadowcastingFieldOfVision(); // this is a simple LoS implementation with a maximum range of 2 tiles
            messageWindow = new SparcMessageWindow(80, 1, 40, 40, console);
            messageManager = new SparcMessageManager(messageWindow);
            expansionSlotsWindow = new ExpansionSlotsWindow(0, 0, 40, 32, console, (pc as IEquipmentBeing).Equipment);

            // you can add a splash screen here
            //

            // optionally, draw the outline of the game window frames 
            //

            // initialise the generators
            // itemGenerator = new ItemGenerator(new ItemFacade());
            monsterGenerator = new MonsterGenerator();
            itemGenerator = new SparcItemGenerator(new ItemFacade());

            // init the viewport
            viewPort = new ViewPort(40, 0, 40, 40, console, new Position(0, 0));
            viewPort.Shade = false;

            // set up the level structure
            Level = new LevelGenerator().CreateLevelStructure();
            viewPort.Map = Map;
        }

        protected override void tearDownGame() {
            // save game stuff goes here
        }

        #endregion

        #region private methods

        private void reDraw() {
            fieldOfVision.ComputeFieldOfVision(Map, pc.Position, fieldOfVision.MaxVisionRange);
            viewPort.RenderMap(fieldOfVision);
            foreach (Item item in Level.Items) viewPort.Display(item, fieldOfVision, pc.Position);
            foreach (Monster monster in Level.Monsters) viewPort.Display(monster, fieldOfVision, pc.Position);
            viewPort.Display(pc, fieldOfVision, pc.Position);
            viewPort.Refresh();
        }

        #endregion
    }
}
