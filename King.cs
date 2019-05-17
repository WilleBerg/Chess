using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Schack
{
    class King : Piece
    {
        public bool isFirstMove;
        public King(Texture2D texture, Rectangle rectangle, Vector2 vector, Vector2 tempPos, bool isWhite, bool[] allowedMoves, bool[] am, bool isDead, int checkCounter) : base(texture, rectangle, vector, tempPos, isWhite, allowedMoves, am, isDead, checkCounter)
        {
            this.texture = texture;
            this.rectangle = rectangle;
            this.vector = vector;
            this.tempPos = tempPos;
            this.isWhite = isWhite;
            this.allowedMoves = allowedMoves;
            this.am = am;
            isFirstMove = true;
            this.isDead = isDead;
            schack = new bool[64];
        }

        override
        public bool Checker(Rectangle a)
        {
            return am[(a.X / 87) + 8 * (a.Y / 87)];
        }
        override
        public void ActualChecker(int pos, bool lfs)
        {
            kingMove(pos);
        }
        public bool SchackMatt() {
            Game1.debugger.Clear();
            ActualChecker(Game1.getBoard((int)tempPos.X, (int)tempPos.Y), false);
            bool temps = false;
            for (int i = 0; i < am.Length; i++) {
                if (am[i]) {
                    Game1.debugger.Add("KING => Line 42 \n");
                    temps = true;
                    break;
                }
            }
            int tmp = 0;
            Piece temp = null;
            for (int i = 0; i < Game1.activePiece.Count; i++) {
                if (Game1.activePiece[i].isWhite != isWhite && !Game1.activePiece[i].isDead && Game1.activePiece[i].schack[Game1.getBoard((int)Game1.activePiece[i].tempPos.X, (int)Game1.activePiece[i].tempPos.Y)]) {
                    tmp++;
                    temp = Game1.activePiece[i];
                    break;
                }
            }
            Game1.debugger.Add(tmp.ToString());
            if (temp is Queen) {
                Game1.debugger.Add("Checker is Queen ; IsWhite = " + temp.isWhite);
                Game1.debugger.Add("Checker position" + Game1.getBoard((int)temp.tempPos.X, (int)temp.tempPos.Y));
            } else if (temp is Rook) {
                Game1.debugger.Add("Checker is Rook ; IsWhite = " + temp.isWhite);
                Game1.debugger.Add("Checker position" + Game1.getBoard((int)temp.tempPos.X, (int)temp.tempPos.Y));
            } else if (temp is Bishop) {
                Game1.debugger.Add("Checker is Bishop ; IsWhite = " + temp.isWhite);
                Game1.debugger.Add("Checker position" + Game1.getBoard((int)temp.tempPos.X, (int)temp.tempPos.Y));
            } else if (temp is Knight) {
                Game1.debugger.Add("Checker is Knight ; IsWhite = " + temp.isWhite);
                Game1.debugger.Add("Checker position" + Game1.getBoard((int)temp.tempPos.X, (int)temp.tempPos.Y));
            } else if (temp is WhitePawn || temp is BlackPawn) {
                Game1.debugger.Add("Checker is Pawn ; IsWhite = " + temp.isWhite);
                Game1.debugger.Add("Checker position" + Game1.getBoard((int)temp.tempPos.X, (int)temp.tempPos.Y));
            }
            if (tmp == 1 && SingleCaptorPossible(temp) && !temps) {
                Game1.debugger.Add("KING => Line 55 \n");
                if (isWhite == true) {
                    Game1.vitVinst = false;
                    Game1.shackMatt = true;
                } else {
                    Game1.vitVinst = true;
                    Game1.shackMatt = true;
                }
                return true;
            } else if (tmp == 1 && isWhite) {
                Game1.whiteCheck = true;
                Game1.debugger.Add("KING => Line 82");
            } else if (tmp == 1 && !isWhite) {
                Game1.debugger.Add("KING => Line 84");
                Game1.blackCheck = true;
            } else if (tmp == 0 && Game1.whiteCheck) {
                Game1.debugger.Add("KING => Line 86");
                Game1.whiteCheck = false;
                Game1.shackArray = new bool[64];
            } else if (tmp == 0 && Game1.blackCheck) {
                Game1.blackCheck = false;
                Game1.shackArray = new bool[64];
                Game1.debugger.Add("KING => Line 90");
            }
            Game1.debugger.Add("KING => Line 58 \n");
            return false;
        }
        private bool SingleCaptorPossible(Piece a) {
            Game1.checkPieces.Clear();
            int temporary = 0;
            Game1.theChecker = a;
            for (int i = 0; i < Game1.activePiece.Count; i++) {
                for (int j = 0; j < 8 * 8; j++) {
                    if (!(Game1.activePiece[i] is King) && Game1.SimulatedMove(Game1.activePiece[i], j)) {
                        temporary++;
                        break;
                    }
                }
                if (Game1.activePiece[i].isWhite != a.isWhite) {
                    Game1.activePiece[i].ActualChecker(Game1.getBoard((int)Game1.activePiece[i].tempPos.X, (int)Game1.activePiece[i].tempPos.Y), false);
                    if (Game1.activePiece[i].am[Game1.getBoard((int)a.tempPos.X, (int)a.tempPos.Y)] == true) {
                        Game1.debugger.Add("KING => Line 65 \n");
                        Game1.checkPieces.Add(Game1.activePiece[i]);
                    }
                }
            }
            Game1.debugger.Add("Temp => " + temporary);
            Game1.debugger.Add("KING => Line 70 \n");
            if (a is Bishop) {
                Game1.debugger.Add("Bioshop outside of if");
            } else if (a is Queen) {
                Game1.debugger.Add("queen outside of if");
            }
            if (Game1.checkPieces.Count > 0 || temporary > 0) {
                return false;
            }
            return true;
        }
        public void kingMove(int pos)
        {
            if (pos - 9 >= 0 && (pos) % 8 != 0 && !IsCheckChecker(pos - 9))
            {
                if (Game1.isTaken[pos - 9])
                {
                    if (Game1.isWho[pos - 9] != null)
                    {
                        if (Game1.isWho[pos - 9].isWhite != isWhite)
                        {
                            am[pos - 9] = true;
                        }
                    }
                }
                else
                {
                    am[pos - 9] = true;
                }
            }
            if (pos - 8 >= 0 && !IsCheckChecker(pos - 8))
            {
                if (Game1.isTaken[pos - 8])
                {
                    if (Game1.isWho[pos - 8] != null)
                    {
                        if (Game1.isWho[pos - 8].isWhite != isWhite)
                        {
                            am[pos - 8] = true;
                        }
                    }
                }
                else
                {
                    am[pos - 8] = true;
                }
            }
            if (pos - 7 >= 0 && (pos) % 8 != 7 && !IsCheckChecker(pos - 7))
            {
                if (Game1.isTaken[pos - 7])
                {
                    if (Game1.isWho[pos - 7] != null)
                    {
                        if (Game1.isWho[pos - 7].isWhite != isWhite)
                        {
                            am[pos - 7] = true;
                        }
                    }
                }
                else
                {
                    am[pos - 7] = true;
                }
            }
            if (pos - 1 >= 0 && (pos) % 8 != 0 && !IsCheckChecker(pos - 1))
            {
                if (Game1.isTaken[pos - 1])
                {
                    if (Game1.isWho[pos - 1] != null)
                    {
                        if (Game1.isWho[pos - 1].isWhite != isWhite)
                        {
                            am[pos - 1] = true;
                        }
                    }
                }
                else
                {
                    am[pos - 1] = true;
                }
            }
            if (pos + 1 <= 63 && (pos) % 8 != 7 && !IsCheckChecker(pos + 1))
            {
                if (Game1.isTaken[pos + 1])
                {
                    if (Game1.isWho[pos + 1] != null)
                    {
                        if (Game1.isWho[pos + 1].isWhite != isWhite)
                        {
                            am[pos + 1] = true;
                        }
                    }
                }
                else
                {
                    am[pos + 1] = true;
                }
            }
            if (pos + 7 <= 63 && pos % 8 != 0 && !IsCheckChecker(pos + 7))
            {
                if (Game1.isTaken[pos + 7])
                {
                    if (Game1.isWho[pos + 7] != null)
                    {
                        if (Game1.isWho[pos + 7].isWhite != isWhite)
                        {
                            am[pos + 7] = true;
                        }
                    }
                }
                else
                {
                    am[pos + 7] = true;
                }
            }
            if (pos + 8 <= 63 && !IsCheckChecker(pos + 8))
            {
                if (Game1.isTaken[pos + 8])
                {
                    if (Game1.isWho[pos + 8] != null)
                    {
                        if (Game1.isWho[pos + 8].isWhite != isWhite)
                        {
                            am[pos + 8] = true;
                        }
                    }
                }
                else
                {
                    am[pos + 8] = true;
                }
            }
            if (pos + 9 <= 63 && (pos) % 8 != 7 && !IsCheckChecker(pos + 9))
            {
                if (Game1.isTaken[pos + 9])
                {
                    if (Game1.isWho[pos + 9] != null)
                    {
                        if (Game1.isWho[pos + 9].isWhite != isWhite)
                        {
                            am[pos + 9] = true;
                        }
                    }
                }
                else
                {
                    am[pos + 9] = true;
                }
            }

            //SchackMatt();
        }
        //kungen kan inte ge schack
        public override void ActualSchack(int pos)
        {
        }

        public bool IsCheckChecker(int pos)
        {
            Vector2 tempp;
            for (int i = 0; i < Game1.activePiece.Count; i++)
            {
                tempp.X = -100;
                tempp.Y = -100;
                if (Game1.activePiece[i].isWhite != isWhite && !(Game1.activePiece[i] is King) && !Game1.activePiece[i].isDead)
                {
                    Game1.activePiece[i].am = new bool[64];
                    Game1.activePiece[i].ActualChecker(Game1.getBoard((int)Game1.activePiece[i].tempPos.X, (int)Game1.activePiece[i].tempPos.Y), true);
                    int apPos = Game1.getBoard((int)Game1.activePiece[i].tempPos.X, (int)Game1.activePiece[i].tempPos.Y);
                    for (int j = 0; j < 8 * 8; j++)
                    {
                        if (Game1.activePiece[i] is Pawn)
                        {
                            if (Game1.activePiece[i].isWhite)
                            {
                                if ((Game1.boardList[j].X / 87) + 8 * (Game1.boardList[j].Y / 87) - 7 >= 0 && apPos % 8 != 7)
                                {
                                    if ((Game1.activePiece[i].rectangle.X / 87) + 8 * (Game1.activePiece[i].rectangle.Y / 87) - 7 == pos)
                                    {
                                        return true;
                                    }
                                }
                                if ((Game1.boardList[j].X / 87) + 8 * (Game1.boardList[j].Y / 87) - 9 >= 0)
                                {
                                    if ((Game1.activePiece[i].rectangle.X / 87) + 8 * (Game1.activePiece[i].rectangle.Y / 87) - 9 == pos && apPos % 8 != 0)
                                    {
                                        return true;
                                    }
                                }
                            }
                            else
                            {
                                if ((Game1.boardList[j].X / 87) + 8 * (Game1.boardList[j].Y / 87) + 7 <= 63)
                                {
                                    if ((Game1.activePiece[i].rectangle.X / 87) + 8 * (Game1.activePiece[i].rectangle.Y / 87) + 7 == pos && apPos % 8 != 0)
                                    {
                                        return true;
                                    }
                                }
                                if ((Game1.boardList[j].X / 87) + 8 * (Game1.boardList[j].Y / 87) + 9 <= 63)
                                {
                                    if ((Game1.activePiece[i].rectangle.X / 87) + 8 * (Game1.activePiece[i].rectangle.Y / 87) + 9 == pos && apPos % 8 != 7)
                                    {
                                        return true;
                                    }
                                }

                            }
                        }
                        else if (Game1.activePiece[i].Checker(Game1.boardList[j]))
                        {
                            tempp.X = Game1.boardList[j].X;
                            tempp.Y = Game1.boardList[j].Y;
                        }
                        if ((tempp.X / 87) + 8 * (tempp.Y / 87) == pos)
                        {
                            return true;
                        }
                    }
                } else if (Game1.activePiece[i].isWhite != isWhite && Game1.activePiece[i] is King) {
                    return KingMoveOpposite(pos, i);
                }
            }
            return false;
        }
        private bool KingMoveOpposite(int pos, int i) {
            int OppositePos = Game1.getBoard((int)Game1.activePiece[i].tempPos.X, (int)Game1.activePiece[i].tempPos.Y);
            if (OppositePos - 9 >= 0 && (OppositePos) % 8 != 0) {
                if (Game1.isTaken[OppositePos - 9]) {
                    if (Game1.isWho[OppositePos - 9] != null) {
                        if (Game1.isWho[OppositePos - 9].isWhite != Game1.activePiece[i].isWhite) {
                            if (OppositePos - 9 == pos) {
                                return true;
                            }
                        }
                    }
                } else {
                    if (OppositePos - 9 == pos) {
                        return true;
                    }
                }
            }
            if (OppositePos - 8 >= 0) {
                if (Game1.isTaken[OppositePos - 8]) {
                    if (Game1.isWho[OppositePos - 8] != null) {
                        if (Game1.isWho[OppositePos - 8].isWhite != Game1.activePiece[i].isWhite) {
                            if (OppositePos - 8 == pos) {
                                return true;
                            }
                        }
                    }
                } else {
                    if (OppositePos - 8 == pos) {
                        return true;
                    }
                }
            }
            if (OppositePos - 7 >= 0 && (OppositePos) % 8 != 7) {
                if (Game1.isTaken[OppositePos - 7]) {
                    if (Game1.isWho[OppositePos - 7] != null) {
                        if (Game1.isWho[OppositePos - 7].isWhite != Game1.activePiece[i].isWhite) {
                            if (OppositePos - 7 == pos) {
                                return true;
                            }
                        }
                    }
                } else {
                    if (OppositePos - 7 == pos) {
                        return true;
                    }
                }
            }
            if (OppositePos - 1 >= 0 && (OppositePos) % 8 != 0) {
                if (Game1.isTaken[OppositePos - 1]) {
                    if (Game1.isWho[OppositePos - 1] != null) {
                        if (Game1.isWho[OppositePos - 1].isWhite != Game1.activePiece[i].isWhite) {
                            if (OppositePos - 1 == pos) {
                                return true;
                            }
                        }
                    }
                } else {
                    if (OppositePos - 1 == pos) {
                        return true;
                    }
                }
            }
            if (OppositePos + 1 <= 63 && (OppositePos) % 8 != 7) {
                if (Game1.isTaken[OppositePos + 1]) {
                    if (Game1.isWho[OppositePos + 1] != null) {
                        if (Game1.isWho[OppositePos + 1].isWhite != Game1.activePiece[i].isWhite) {
                            if (OppositePos + 1 == pos) {
                                return true;
                            }
                        }
                    }
                } else {
                    if (OppositePos + 1 == pos) {
                        return true;
                    }
                }
            }
            if (OppositePos + 7 <= 63 && OppositePos % 8 != 0) {
                if (Game1.isTaken[OppositePos + 7]) {
                    if (Game1.isWho[OppositePos + 7] != null) {
                        if (Game1.isWho[OppositePos + 7].isWhite != Game1.activePiece[i].isWhite) {
                            if (OppositePos + 7 == pos) {
                                return true;
                            }
                        }
                    }
                } else {
                    if (OppositePos + 7 == pos) {
                        return true;
                    }
                }
            }
            if (OppositePos + 8 <= 63) {
                if (Game1.isTaken[OppositePos + 8]) {
                    if (Game1.isWho[OppositePos + 8] != null) {
                        if (Game1.isWho[OppositePos + 8].isWhite != Game1.activePiece[i].isWhite) {
                            if (OppositePos + 8 == pos) {
                                return true;
                            }
                        }
                    }
                } else {
                    if (OppositePos + 8 == pos) {
                        return true;
                    }
                }
            }
            if (OppositePos + 9 <= 63 && (OppositePos) % 8 != 7) {
                if (Game1.isTaken[OppositePos + 9]) {
                    if (Game1.isWho[OppositePos + 9] != null) {
                        if (Game1.isWho[OppositePos + 9].isWhite != Game1.activePiece[i].isWhite) {
                            if (OppositePos + 9 == pos) {
                                return true;
                            }
                        }
                    }
                } else {
                    if (OppositePos + 9 == pos) {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
