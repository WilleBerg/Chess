using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schack
{
    class Rook : Piece
    {
        public Rook(Texture2D newTexture, Rectangle newRectangle, Vector2 newVector, Vector2 newtempVector, bool newIsWhite, bool[] allowedMoves, bool[] am, bool isDead, int checkCounter, bool[] checkArray) : base(newTexture, newRectangle, newVector, newtempVector, newIsWhite, allowedMoves, am, isDead, checkCounter, checkArray)
        {
        }

        public override void ActualChecker(int pos, bool lfs)
        {
            for (int i = 0; i < widthLeft(pos, lfs); i++)
            {
                am[pos - 1 - i] = true;
            }
            for (int i = 0; i < widthRight(pos, lfs); i++)
            {
                am[pos + 1 + i] = true;
            }
            for (int i = 1; i < heightUp(pos, lfs); i++)
            {
                am[pos - 8 * i] = true;
            }
            for (int i = 1; i < heightDown(pos, lfs); i++)
            {
                am[pos + 8 * i] = true;
            }
            schack = new bool[64];
            SchackChecker(am);
            if (checkCounter < 50) {
                checkCounter++;
            }
        }

        public override bool Checker(Rectangle a)
        {
            return am[(a.X / 87) + 8 * (a.Y / 87)];
        }
        private int heightUp(int pos, bool lfs)
        {
            int tmp = 0;
            for (int i = 1; i < 8; i++)
            {
                if (pos - 8 * i >= 0 && Game1.isTaken[pos - 8 * i] == false)
                {
                    tmp++;
                }
                else if (pos - 8 * i >= 0 && Game1.isTaken[pos - 8 * i] == true && Game1.isWho[pos - 8 * i].isWhite != isWhite && lfs == false)
                {
                    tmp++;
                    break;
                } else if (pos - 8 * i >= 0 && Game1.isTaken[pos - 8 * i] == true && Game1.isWho[pos - 8 * i].isWhite == isWhite && lfs == true) {
                    tmp++;
                    break;
                } else
                {
                    break;
                }
            }
            tmp++;
            return tmp;
        }
        private int heightDown(int pos, bool lfs)
        {
            int tmp = 0;
            for (int i = 1; i < 8; i++)
            {
                if (pos + 8 * i <= 63 && Game1.isTaken[pos + 8 * i] == false)
                {
                    tmp++;
                }
                else if (pos + 8 * i <= 63 && Game1.isTaken[pos + 8 * i] == true && Game1.isWho[pos + 8 * i].isWhite != isWhite && lfs == false)
                {
                    tmp++;
                    break;
                } else if (pos + 8 * i <= 63 && Game1.isTaken[pos + 8 * i] == true && Game1.isWho[pos + 8 * i].isWhite == isWhite && lfs == true) {
                    tmp++;
                    break;
                }
                else
                {
                    break;
                }
            }
            tmp++;
            return tmp++;
        }
        private int widthLeft(int pos, bool lfs)
        {
            int rnt = 0;
            int tmp = pos - 1;
            if (pos % 8 == 0)
            {

            }
            else
            {
                while (tmp % 8 != 0)
                {
                    if (Game1.isTaken[tmp] == false) {
                        rnt++;
                        tmp--;
                    } else if (Game1.isTaken[tmp] == true && Game1.isWho[tmp].isWhite != isWhite && lfs == false) {
                        break;
                    } else if (Game1.isTaken[tmp] == true && Game1.isWho[tmp].isWhite == isWhite && lfs == true) {
                        break;
                    } else {
                        rnt--;
                        break;
                    }
                }
                rnt++;
            }
            return rnt;
        }
        private int widthRight(int pos, bool lfs)
        {
            int rnt = 0;
            int tmp = pos + 1;
            while (tmp % 8 != 0)
            {
                if (Game1.isTaken[tmp] == false)
                {
                    rnt++;
                    tmp++;
                }
                else if (Game1.isTaken[tmp] == true && Game1.isWho[tmp].isWhite != isWhite && !lfs)
                {
                    rnt++;
                    break;
                } else if (Game1.isTaken[tmp] == true && Game1.isWho[tmp].isWhite == isWhite && lfs) {
                    rnt++;
                    break;
                } else
                {
                    break;
                }
            }
            return rnt;
        }

        public override void ActualSchack(int pos)
        {
            for (int i = 0; i < widthLeft(pos, false); i++)
            {
                if (Game1.isWho[pos - 1 - i] != null && Game1.isWho[pos - 1 - i] is King && Game1.isWho[pos - 1 - i].isWhite != isWhite)
                {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos - 1 - j] = true;
                        } 
                    }
                }
            }
            for (int i = 0; i < widthRight(pos, false); i++)
            {
                if (Game1.isWho[pos + 1 + i] != null && Game1.isWho[pos + 1 + i] is King && Game1.isWho[pos + 1 + i].isWhite != isWhite)
                {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos + 1 + j] = true;
                        } 
                    }
                }
            }
            for (int i = 1; i < heightUp(pos, false); i++)
            {
                if (Game1.isWho[pos - 8 * i] != null && Game1.isWho[pos - 8 * i] is King && Game1.isWho[pos - 8 * i].isWhite != isWhite)
                {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos - 8 * j] = true;
                        } 
                    }
                }
            }
            for (int i = 1; i < heightDown(pos, false); i++)
            {
                if (Game1.isWho[pos + 8 * i] != null && Game1.isWho[pos + 8 * i] is King && Game1.isWho[pos + 8 * i].isWhite != isWhite)
                {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos + 8 * j] = true;
                        } 
                    }
                }
            }
        }
        public override string toString() {
            return "Rook";
        }
        public override void CheckPath(int pos) {
            for (int i = 0; i < widthLeft(pos, false); i++) {
                if (Game1.isWho[pos - 1 - i] != null && Game1.isWho[pos - 1 - i] is King && Game1.isWho[pos - 1 - i].isWhite != isWhite) {
                    for (int j = 0; j < i; j++) {
                        checkArray[pos - 1 - j] = true;
                    }
                }
            }
            for (int i = 0; i < widthRight(pos, false); i++) {
                if (Game1.isWho[pos + 1 + i] != null && Game1.isWho[pos + 1 + i] is King && Game1.isWho[pos + 1 + i].isWhite != isWhite) {
                    for (int j = 0; j < i; j++) {
                        checkArray[pos + 1 + j] = true;
                    }
                }
            }
            for (int i = 1; i < heightUp(pos, false); i++) {
                if (Game1.isWho[pos - 8 * i] != null && Game1.isWho[pos - 8 * i] is King && Game1.isWho[pos - 8 * i].isWhite != isWhite) {
                    for (int j = 0; j < i; j++) {
                        checkArray[pos - 8 * j] = true;
                    }
                }
            }
            for (int i = 1; i < heightDown(pos, false); i++) {
                if (Game1.isWho[pos + 8 * i] != null && Game1.isWho[pos + 8 * i] is King && Game1.isWho[pos + 8 * i].isWhite != isWhite) {
                    for (int j = 0; j < i; j++) {
                        checkArray[pos + 8 * j] = true;
                    }
                }
            }
        }
    }
}
