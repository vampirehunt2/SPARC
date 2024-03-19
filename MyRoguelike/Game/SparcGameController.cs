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


        protected Keybindings keybindings = Keybindings.Instance;
        private string command;

        protected override void runTurn() {
            base.runTurn();

            // display the whole game screen
            console.ForegroundColor = ConsoleColor.Gray;
            fieldOfVision.ComputeFieldOfVision(Map, pc.Position, fieldOfVision.MaxVisionRange);
            viewPort.RenderMap(fieldOfVision);
            foreach (Item item in Level.Items) viewPort.Display(item, fieldOfVision, pc.Position);
            foreach (Monster monster in Level.Monsters) viewPort.Display(monster, fieldOfVision, pc.Position);
            viewPort.Display(pc, fieldOfVision, pc.Position);
            viewPort.Refresh();

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
                gametimeTicks = (int)(action.TimeNeeded / pc.Speed);
                moveMonsters();
                Pc.Move();
                runBaseAction(Pc);
            } else {
                // out-of-game actions. 
                // these are actions performed by the player.
                // these actions do not take up gametime.
                if (Command == "backpack") { }          // show PC inventory
                else if (Command == "show-help") { }    // show game help
                else if (Command == "quit") {
                    string msg = Translator.Instance["quit?"];
                    if (new YesNoMenu(msg, messageWindow, 'Y', 'N').ShowMenu() == MenuResult.OK) {
                        QuitGame = true;
                    }
                    messageWindow.Clear();
                }
            }
        }

        public string Command {
            get { return command; }
        }

        protected override void runBaseAction(Being performer) {
            new SparcAction(performer).Perform();
        }

        protected override void setUpGame() {
            // initialise the display
            console = new VhConsole();  // this type is a fullscreen console
            console.ForegroundColor = ConsoleColor.Blue;
            fieldOfVision = new HardcodedShadowcastingFieldOfVision(); // this is a simple LoS implementation with a maximum range of 2 tiles
            messageWindow = new SparcMessageWindow(41, 1, 38, 48, console);
            messageManager = new SparcMessageManager(messageWindow);
            console.CursorVisible = false;
            console.Height = 50;
            console.Width = 80;
            console.Clear();
            console.GoTo(0, 0);

            // intialise the PC
            pc = new SparcPc();
            pc.Character = '@';
            pc.Ai = new SparcPcAi(pc);

            // you can add a splash screen here
            //

            // optionally, draw the outline of the game window frames 
            //

            // initialise the generators
            // itemGenerator = new ItemGenerator(new ItemFacade());
            monsterGenerator = new MonsterGenerator();
            itemGenerator = new SparcItemGenerator(new ItemFacade());

            // init the viewport
            viewPort = new ViewPort(1, 1, 40, 40, console, new Position(0, 0));
            viewPort.Shade = false;

            // set up the level structure
            Level = new LevelGenerator().CreateLevelStructure();
            viewPort.Map = Map;
        }

        protected override void tearDownGame() {
            // save game stuff goes here
        }

    }
}
