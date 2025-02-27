using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Display;
using VH.Engine.Levels;

namespace SPARC.Levels {
    public class SparcViewPort: ViewPort {

        #region constructors

        public SparcViewPort(): base() { }

        public SparcViewPort(int x, int y, int width, int height, IConsole console, Position position) 
            : base(x, y, width, height, console, position) { }

        public SparcViewPort(ViewPort sourceViewPort): base() {
            copyFrom(sourceViewPort);
        }

        #endregion

        #region protected methods

        protected void copyFrom(ViewPort viewPort) {
            x = viewPort.X;
            y = viewPort.Y;
            width = viewPort.Width;
            height = viewPort.Height;
            map = viewPort.Map;
            console = viewPort.Console;
            position = viewPort.Position;
        }

        #endregion


    }
}
