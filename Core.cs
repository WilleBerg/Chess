using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schack {
    public class Core {
        public UI ui = new UI();
        public Random rnd = new Random();
        public List<Piece> tmpList = new List<Piece>();
        public Piece[] isWhoTemp = new Piece[64];
        public bool[] isTakenTemp = new bool[64];
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
        private void Copyer(Piece[] a, bool[] b) {
            for (int i = 0; i < Game1.isWho.Length; i++) {
                if (Game1.isWho[i] is Queen) {
                    a[i] = new Queen(Game1.isWho[i]);
                } else if (Game1.isWho[i] is King) {
                    a[i] = new King(Game1.isWho[i]);
                } else if (Game1.isWho[i] is Rook) {
                    a[i] = new Rook(Game1.isWho[i]);
                } else if (Game1.isWho[i] is Bishop) {
                    a[i] = new Bishop(Game1.isWho[i]);
                } else if (Game1.isWho[i] is BlackPawn) {
                    a[i] = new BlackPawn(Game1.isWho[i]);
                } else if (Game1.isWho[i] is WhitePawn) {
                    a[i] = new WhitePawn(Game1.isWho[i]);
                } else if (Game1.isWho[i] is Knight) {
                    a[i] = new Knight(Game1.isWho[i]);
                }
                bool tempBool = Game1.isTaken[i];
                b[i] = tempBool;
            }
        }
        public List<int> SimulateRound(Piece ap) {
            List<int> returnMoves = new List<int>();
            Piece apCopy = ap;
            int currPos = getPos(apCopy);
            Queue<int> q = new Queue<int>();
            for (int i = 0; i < apCopy.am.Length; i++) {
                if (apCopy.am[i]) {
                    q.Enqueue(i);
                }
            }
            Game1.debugger.Add(q.Count.ToString());
            while (q.Count > 0) {
                int i = q.Dequeue();
                if (ap is Queen) {
                    apCopy = new Queen(ap);
                } else if (ap is King) {
                    apCopy = new King(ap);
                } else if (ap is Rook) {
                    apCopy = new Rook(ap);
                } else if (ap is Bishop) {
                    apCopy = new Bishop(ap);
                } else if (ap is BlackPawn) {
                    apCopy = new BlackPawn(ap);
                } else if (ap is WhitePawn) {
                    apCopy = new WhitePawn(ap);
                } else if (ap is Knight) {
                    apCopy = new Knight(ap);
                }
                King wkTemp = new King(Game1.wk);
                King bkTemp = new King(Game1.bk);

                //Queens
                Queen wQTemp = new Queen(Game1.wQ);
                Queen bQTemp = new Queen(Game1.bQ);

                //White rooks
                Rook wR1Temp = new Rook(Game1.wR1);
                Rook wR2Temp = new Rook(Game1.wR2);

                //Black rooks
                Rook bR1Temp = new Rook(Game1.bR1);
                Rook bR2Temp = new Rook(Game1.bR2);

                //White Pawns
                WhitePawn wp1Temp = new WhitePawn(Game1.wp1);
                WhitePawn wp2Temp = new WhitePawn(Game1.wp2);
                WhitePawn wp3Temp = new WhitePawn(Game1.wp3);
                WhitePawn wp4Temp = new WhitePawn(Game1.wp4);
                WhitePawn wp5Temp = new WhitePawn(Game1.wp5);
                WhitePawn wp6Temp = new WhitePawn(Game1.wp6);
                WhitePawn wp7Temp = new WhitePawn(Game1.wp7);
                WhitePawn wp8Temp = new WhitePawn(Game1.wp8);

                //Black Pawns
                BlackPawn bp1Temp = new BlackPawn(Game1.bp1);
                BlackPawn bp2Temp = new BlackPawn(Game1.bp2);
                BlackPawn bp3Temp = new BlackPawn(Game1.bp3);
                BlackPawn bp4Temp = new BlackPawn(Game1.bp4);
                BlackPawn bp5Temp = new BlackPawn(Game1.bp5);
                BlackPawn bp6Temp = new BlackPawn(Game1.bp6);
                BlackPawn bp7Temp = new BlackPawn(Game1.bp7);
                BlackPawn bp8Temp = new BlackPawn(Game1.bp8);

                //Bishops
                Bishop wBTemp = new Bishop(Game1.wB);
                Bishop wB2Temp = new Bishop(Game1.wB2);
                Bishop bBTemp = new Bishop(Game1.bB);
                Bishop bB2Temp = new Bishop(Game1.bB2);

                //Knights (Horses)
                Knight whTemp = new Knight(Game1.wh);
                Knight wh2Temp = new Knight(Game1.wh2);
                Knight bhTemp = new Knight(Game1.bh);
                Knight bh2Temp = new Knight(Game1.bh2);

                tmpList.Clear();
                //Queens add
                tmpList.Add(wQTemp);
                tmpList.Add(bQTemp);

                //White Pawns add
                tmpList.Add(wp1Temp);
                tmpList.Add(wp2Temp);
                tmpList.Add(wp3Temp);
                tmpList.Add(wp4Temp);
                tmpList.Add(wp5Temp);
                tmpList.Add(wp6Temp);
                tmpList.Add(wp7Temp);
                tmpList.Add(wp8Temp);

                //Black Pawns add
                tmpList.Add(bp1Temp);
                tmpList.Add(bp2Temp);
                tmpList.Add(bp3Temp);
                tmpList.Add(bp4Temp);
                tmpList.Add(bp5Temp);
                tmpList.Add(bp6Temp);
                tmpList.Add(bp7Temp);
                tmpList.Add(bp8Temp);

                //Bishops add
                tmpList.Add(wB2Temp);
                tmpList.Add(wBTemp);
                tmpList.Add(bBTemp);
                tmpList.Add(bB2Temp);

                //Rooks add
                tmpList.Add(wR1Temp);
                tmpList.Add(wR2Temp);
                tmpList.Add(bR1Temp);
                tmpList.Add(bR2Temp);

                //Knights add
                tmpList.Add(whTemp);
                tmpList.Add(wh2Temp);
                tmpList.Add(bhTemp);
                tmpList.Add(bh2Temp);

                //Kings add
                tmpList.Add(wkTemp);
                tmpList.Add(bkTemp);

                if (!isTakenTemp[i]) {
                    isTakenTemp[i] = true;
                    isTakenTemp[currPos] = false;
                    apCopy.rectangle.X = Game1.boardList[i].X;
                    apCopy.rectangle.Y = Game1.boardList[i].Y;
                    apCopy.tempPos.X = apCopy.rectangle.X;
                    apCopy.tempPos.Y = apCopy.rectangle.Y;
                    isWhoTemp[i] = apCopy;
                    isWhoTemp[currPos] = null;
                } else if (isTakenTemp[i] && isWhoTemp[i].isWhite != apCopy.isWhite) {
                    Seize(isWhoTemp[i]);
                    isWhoTemp[i].isDead = true;
                    for (int j = 0; j < tmpList.Count; j++) {
                        if (getPos(tmpList[j]) == i) {
                            Seize(tmpList[j]);
                        }
                    }
                    Game1.debugger.Add("Seizing " + isWhoTemp[i].toString());
                    isTakenTemp[i] = true;
                    isTakenTemp[currPos] = false;
                    apCopy.rectangle.X = Game1.boardList[i].X;
                    apCopy.rectangle.Y = Game1.boardList[i].Y;
                    apCopy.tempPos.X = apCopy.rectangle.X;
                    apCopy.tempPos.Y = apCopy.rectangle.Y;
                    isWhoTemp[i] = apCopy;
                    isWhoTemp[currPos] = null;
                }
                for (int j = 0; j < tmpList.Count; j++) {
                    tmpList[j].SimulateAllowedMoves(getPos(tmpList[j]), isWhoTemp, isTakenTemp, tmpList);
                }
                Piece king = apCopy;
                if (apCopy.isWhite) {
                    king = tmpList[30];
                } else if (!apCopy.isWhite) {
                    king = tmpList[31];
                }
                Game1.debugger.Add(king.toString() + " IsWhite = " + king.isWhite + " Pos " + getPos(king));
                Game1.debugger.Add(getPos(apCopy).ToString());
                Game1.debugger.Add(isTakenTemp[currPos].ToString());
                int tmp = 0;
                for (int j = 0; j < tmpList.Count; j++) {
                    if (tmpList[j].isWhite != king.isWhite && !tmpList[j].isDead) {
                        for (int k = 0; k < tmpList[j].am.Length; k++) {
                            if (tmpList[j].am[k] && k == getPos(king)) {
                                tmp++;
                                Game1.debugger.Add(tmpList[j].toString());
                                break;  
                            }
                        }
                    }
                }
                Game1.debugger.Add("Tmp " + tmp.ToString());
                if (tmp == 0) {
                    returnMoves.Add(i);
                }
                Copyer(isWhoTemp, isTakenTemp);
            }
            return returnMoves;
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
