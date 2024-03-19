using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Display;
using VH.Engine.Levels;

namespace SPARC.Levels {
    internal class SpaceshipRoom {

        #region fields

        int x, y, r;

        #endregion

        #region properties

        public int X {
            get { return this.x; }
        }

        public int Y {
            get { return this.y; }
        }

        public int R {
            get { return this.r; }
        }

        #endregion

        #region constructors

        public SpaceshipRoom(int x, int y, int r) { 
            this.x = x;
            this.y = y;
            this.r = r;
        }

        #endregion

        #region public methods

        public void GenerateRoom(Map map) {
            
            char ground = Terrain.Get("ground").Character;
            for (int i = x - r; i <= x + r; i++) {
                for (int j = y - r; j <= y + r; j++) {
                    int dx = Math.Abs(x - i);
                    int dy = Math.Abs(y - j);
                    int dx2 = dx * dx;
                    int dy2 = dy * dy;
                    int d = (int)Math.Sqrt(dx2 +  dy2);
                    if (d <= r) {
                        map[i, j] = ground;
                    }
                }
            }
        }

        #endregion

    }
}
