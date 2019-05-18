using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Schack {
    public class UI {
        
        public Graph graphics = new Graph();

        public UI() {
            graphics = new Graph();
        }

        public void MenuButtons() {
            if (Musknappar() && RecChecker(graphics.playButton)) {
                Game1.scen = 0;
            }
            if (Musknappar() && RecChecker(graphics.settingRec)) {
                Game1.menuScene = 1;
            }
            if (Musknappar() && RecChecker(graphics.exitRec)) {
                Game1.exitGame = true;
            }
        }
        public void DisableButton() {
            if (Musknappar() && RecChecker(graphics.disableRec)) {
                Game1.debuggingMode = false;
            }
            if (Musknappar() && RecChecker(graphics.backRec)) {
                Game1.menuScene = 0;
            }
        }
        public void SettingsButtons() {
            if (Musknappar() && RecChecker(graphics.debugRec)) {
                Game1.debuggingMode = true;
            }
            if (Musknappar() && RecChecker(graphics.backRec)) {
                Game1.menuScene = 0;
            }

        }
        public bool Musknappar() {
            if (Game1.mus.LeftButton == ButtonState.Pressed && Game1.gammalMus.LeftButton == ButtonState.Released) {
                return true;
            } else {
                return false;
            }

        }
        public bool RecChecker(Rectangle a) {
            if (a.Contains(Game1.mus.Position)) {
                return true;
            } else {
                return false;
            }
        }
        public bool LeftMousePressed() {
            if (Game1.mus.LeftButton == ButtonState.Pressed) {
                return true;
            } else {
                return false;
            }
        }
        public void AddBoard(List<Rectangle> boardList) {
            for (int j = 0; j < 8; j++) {
                for (int i = 0; i < 8; i++) {
                    boardList.Add(new Rectangle(87 * i, 87 * j, 87, 87));
                }
            }
        }
        public void ForMouseUpdate() {
            if (Musknappar() && RecChecker(graphics.backRec2)) {
                Game1.scen = 1;
            }
            if (Musknappar() && RecChecker(graphics.exitRec2) && Game1.shackMatt) {
                Game1.exitGame = true;
}
            if (Musknappar() && RecChecker(graphics.newGameRec) && Game1.shackMatt) {
                Game1.exitGame = true;
            }
        }
    }
}
