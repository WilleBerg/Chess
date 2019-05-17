using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schack {
    public class Graph {
        public Rectangle playButton;
        public Rectangle settingRec;
        public Rectangle debugRec;
        public Rectangle backRec;
        public Rectangle exitRec;
        public Rectangle exitRec2;
        public Rectangle backRec2;
        public Rectangle disableRec;
        public Rectangle newGameRec;
        public Graph() {
            playButton = new Rectangle(500, 87 * 2, 255, 66);
            settingRec = new Rectangle(500, 87 * 3, 255, 66);
            debugRec = new Rectangle(500, 87 * 2, 255, 66);
            backRec = new Rectangle(500, 87 * 3, 255, 66);
            exitRec = new Rectangle(500, 87 * 4, 255, 66);
            exitRec2 = new Rectangle(560 - 42, 440 - 39, 250, 61);
            backRec2 = new Rectangle(913, 87 * 7 + 50, (int)255 / 2, (int)66 / 2);
            disableRec = new Rectangle(500, 87 * 2, 255, 66);
            newGameRec = new Rectangle(258, 440 - 39, 250, 61);
        }
    }
}
