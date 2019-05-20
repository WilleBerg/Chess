using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schack
{
    public class WhitePawn : Pawn
    {

        public WhitePawn(Texture2D newTexture, Rectangle newRectangle, Vector2 newVector, Vector2 newtempVector, bool newIsWhite, bool[] allowedMoves, bool[] am, bool isDead, int checkCounter, bool[] checkArray) : base(newTexture, newRectangle, newVector, newtempVector, newIsWhite, allowedMoves, am, isDead, checkCounter, checkArray)
        {
            isFirstMove = true;
        }

        public WhitePawn(Piece a) : base(a) {
        }

        public override void ActualChecker(int pos, bool lfs)
        {
            am = new bool[64];
            wPawnMove(pos);
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
        
        private int wPawnMove(int pos)
        {

            int tmp = 0;
            if (pos >= 48 && pos <= 55)
            {
                for (int i = 1; i < 3; i++)
                {
                    if (!Game1.isTaken[pos - 8 * i])
                    {
                        am[pos - i * 8] = true;
                    } else {
                        break;
                    }
                }
            }
            else
            {
                if (pos - 8 >= 0 && !Game1.isTaken[pos - 8])
                {
                    am[pos - 8] = true;
                }
            }
            if (pos - 7 >= 0 && pos % 8 != 7 && Game1.isTaken[pos - 7] == true && Game1.isWho[pos - 7].isWhite != isWhite)
            {
                am[pos - 7] = true;
            }
            if (pos - 9 >= 0 && pos % 8 != 0 && Game1.isTaken[pos - 9] == true && Game1.isWho[pos - 9].isWhite != isWhite)
            {
                am[pos - 9] = true;
            }
            return tmp;
        }
        public override void ActualSchack(int pos)
        {
            if (pos - 7 >= 0 && pos % 8 != 7 && Game1.isTaken[pos - 7] == true && Game1.isWho[pos - 7].isWhite != isWhite)
            {
                if (Game1.isWho[pos - 7] != null && Game1.isWho[pos - 7] is King)
                {
                    schack[pos] = true;
                    
                }
            }
            if (pos - 9 >= 0 && pos % 8 != 0 && Game1.isTaken[pos - 9] == true && Game1.isWho[pos - 9].isWhite != isWhite)
            {
                if (Game1.isWho[pos - 9] != null && Game1.isWho[pos - 9] is King)
                {
                    schack[pos] = true;
                    
                }
            }
        }

        public override void CheckPath(int pos) {
            if (pos - 7 >= 0 && pos % 8 != 7 && Game1.isTaken[pos - 7] == true && Game1.isWho[pos - 7].isWhite != isWhite) {
                if (Game1.isWho[pos - 7] != null && Game1.isWho[pos - 7] is King) {
                    checkArray[pos - 7] = true;
                }
            }
            if (pos - 9 >= 0 && pos % 8 != 0 && Game1.isTaken[pos - 9] == true && Game1.isWho[pos - 9].isWhite != isWhite) {
                if (Game1.isWho[pos - 9] != null && Game1.isWho[pos - 9] is King) {
                    checkArray[pos - 9] = true;
                }
            }
        }

        public override string toString() {
            return "White Pawn";
        }

        public override void SimulateAllowedMoves(int pos, Piece[] isWho, bool[] isTaken, List<Piece> tmpList) {
            if (pos >= 48 && pos <= 55) {
                for (int i = 1; i < 3; i++) {
                    if (!isTaken[pos - 8 * i]) {
                        am[pos - i * 8] = true;
                    } else {
                        break;
                    }
                }
            } else {
                if (pos - 8 >= 0 && !isTaken[pos - 8]) {
                    am[pos - 8] = true;
                }
            }
            if (pos - 7 >= 0 && pos % 8 != 7 && isTaken[pos - 7] == true && isWho[pos - 7].isWhite != isWhite) {
                am[pos - 7] = true;
            }
            if (pos - 9 >= 0 && pos % 8 != 0 && isTaken[pos - 9] == true && isWho[pos - 9].isWhite != isWhite) {
                am[pos - 9] = true;
            }
        }
    }

}

