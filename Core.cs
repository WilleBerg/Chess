using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schack {
    public class Core {
        public UI ui = new UI();
        public Random rnd = new Random();
        public Core() {

        }
        public int getPos(Piece a) {
            return getBoard((int)a.tempPos.X, (int)a.tempPos.Y);
        }
        public int getBoard(int x, int y) {
            return (x / 87) + 8 * (y / 87);
        }
        public bool cantSeize(Piece a, Piece b) {
            for (int i = 0; i < a.am.Length; i++) {
                if (a.am[i] && i == getBoard((int)b.tempPos.X, (int)b.tempPos.Y)) {
                    return false;
                }
            }
            return true;
        }
        public int WhereSeize(Piece a, Piece b) {
            for (int i = 0; i < a.am.Length; i++) {
                if (a.am[i] && i == getBoard((int)b.tempPos.X, (int)b.tempPos.Y)) {
                    return i;
                }
            }
            //Should never return this
            return 0;
        }
        public void Reset() {

        }
        void Seize(Piece ap) {
            ap.isDead = true;
            ap.schack = new bool[64];
            ap.am = new bool[64];
            ap.checkArray = new bool[64];
            if (ap.isWhite == true) {
                float a = (float)rnd.NextDouble() * 1.2F + 0.3F;
                float b = (float)rnd.NextDouble() * 2.5F + 8;
                ap.vector.X = 87 * b;
                ap.vector.Y = 87 * a;
                ap.rectangle.X = (int)ap.vector.X;
                ap.rectangle.Y = (int)ap.vector.Y;

            } else {
                float a = (float)rnd.NextDouble() * 2 + 5;
                float b = (float)rnd.NextDouble() * 2.5F + 8;
                ap.vector.X = 87 * b;
                ap.vector.Y = 87 * a;
                ap.rectangle.X = (int)ap.vector.X;
                ap.rectangle.Y = (int)ap.vector.Y;
            }
        }
        public void PieceUpdate(Piece ap, int oNmr) {  // Activepiece originalNumber

            if (ap.rectangle.Contains(Game1.mus.Position) && ui.LeftMousePressed()) {
                ap.rectangle.X = Game1.mus.X - ap.rectangle.Width / 2;
                ap.rectangle.Y = Game1.mus.Y - ap.rectangle.Height / 2;
                Game1.mouseIsHolding = true;
            } else {
                Game1.mouseIsHolding = false;
            }
            for (int i = 0; i < 8 * 8; i++) {
                if (Game1.boardList[i].Contains(Game1.mus.Position)) {
                    Game1.text = $"{Game1.boardList[i]}{ap.vector}";
                }
                if (Game1.boardList[i].Contains(ap.vector) && !Game1.mouseIsHolding) {
                    if (ap.allowedMoves[i] == true && !Game1.isTaken[i]) {
                        Game1.placingPiece.Play();
                        Game1.isTaken[i] = true;
                        Game1.isTaken[oNmr] = false;
                        ap.rectangle.X = Game1.boardList[i].X;
                        ap.rectangle.Y = Game1.boardList[i].Y;
                        ap.tempPos.X = ap.rectangle.X;
                        ap.tempPos.Y = ap.rectangle.Y;
                        for (int j = 0; j < 64; j++) {
                            ap.allowedMoves[j] = false;
                        }
                        for (int j = 0; j < Game1.activePiece.Count; j++) {
                            if (Game1.activePiece[j].isWhite == ap.isWhite) {
                                Game1.activePiece[j].checkCounter = 0;
                            }
                        }
                        Game1.isWho[i] = ap;
                        Game1.isWho[oNmr] = null;
                        Game1.newRound = true;
                        Game1.runda++;
                        Game1.shackArray = new bool[64];
                    } else if (ap.allowedMoves[i] == true && Game1.isTaken[i] && Game1.isWho[i].isWhite != ap.isWhite) {
                        Game1.placingPiece.Play();
                        Seize(Game1.isWho[i]);

                        Game1.isTaken[i] = true;
                        Game1.isTaken[oNmr] = false;
                        ap.rectangle.X = Game1.boardList[i].X;
                        ap.rectangle.Y = Game1.boardList[i].Y;
                        ap.tempPos.X = ap.rectangle.X;
                        ap.tempPos.Y = ap.rectangle.Y;
                        for (int j = 0; j < 64; j++) {
                            ap.allowedMoves[j] = false;
                        }
                        for (int j = 0; j < Game1.activePiece.Count; j++) {
                            if (Game1.activePiece[j].isWhite == ap.isWhite) {
                                Game1.activePiece[j].checkCounter = 0;
                            }
                        }
                        Game1.isWho[i] = ap;
                        Game1.isWho[oNmr] = null;
                        Game1.newRound = true;
                        Game1.runda++;
                        Game1.shackArray = new bool[64];
                    } else if (Game1.boardList[i].Contains(ap.vector) && !Game1.mouseIsHolding && i == oNmr) {
                        Game1.isTaken[i] = true;
                        ap.rectangle.X = Game1.boardList[i].X;
                        ap.rectangle.Y = Game1.boardList[i].Y;
                        ap.tempPos.X = ap.rectangle.X;
                        ap.tempPos.Y = ap.rectangle.Y;
                        for (int j = 0; j < 64; j++) {
                            ap.allowedMoves[j] = false;
                        }
                    } else {
                        ap.rectangle.X = Game1.boardList[oNmr].X;
                        ap.rectangle.Y = Game1.boardList[oNmr].Y;
                        ap.tempPos.X = ap.rectangle.X;
                        ap.tempPos.Y = ap.rectangle.Y;
                        for (int j = 0; j < 64; j++) {
                            ap.allowedMoves[j] = false;
                        }

                    }

                }
            }
            ap.vector.X = ap.rectangle.X + ap.rectangle.Width / 2;
            ap.vector.Y = ap.rectangle.Y + ap.rectangle.Height / 2;
        }
    }
}
