using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schack
{
    class BlackPawn : Pawn
    {

        public BlackPawn(Texture2D newTexture, Rectangle newRectangle, Vector2 newVector, Vector2 newtempVector, bool newIsWhite, bool[] allowedMoves, bool[] am, bool isDead, int checkCounter, bool[] checkArray) : base(newTexture, newRectangle, newVector, newtempVector, newIsWhite, allowedMoves, am, isDead, checkCounter, checkArray)
        {
            isFirstMove = true;
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
            if (pos >= 8 && pos <= 15)
            {
                for (int i = 1; i < 3; i++)
                {
                    if (!Game1.isTaken[pos + 8 * i]) {
                        am[pos + i * 8] = true;
                    } else {
                        break;
                    }
                }
            }
            else
            {
                if (pos + 8 <= 63 && !Game1.isTaken[pos + 8])
                {
                    am[pos + 8] = true;
                }
            }
            if (pos + 7 <= 63 && Game1.isTaken[pos + 7] == true && Game1.isWho[pos + 7].isWhite != isWhite && pos % 8 != 0)
            {
                am[pos + 7] = true;
            }
            if (pos + 9 <= 63 && Game1.isTaken[pos + 9] == true && Game1.isWho[pos + 9].isWhite != isWhite && pos % 8 != 7)
            {
                am[pos + 9] = true;
            }
            return tmp;
        }
        public override void ActualSchack(int pos)
        {
            if (pos + 7 <= 63 && Game1.isTaken[pos + 7] == true && Game1.isWho[pos + 7].isWhite != isWhite && pos % 8 != 0)
            {
                if (Game1.isWho[pos + 7] != null && Game1.isWho[pos + 7] is King)
                {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        Game1.shackArray[pos + 7] = true; 
                    }
                }
            }
            if (pos + 9 <= 63 && Game1.isTaken[pos + 9] == true && Game1.isWho[pos + 9].isWhite != isWhite && pos % 8 != 7)
            {
                if (Game1.isWho[pos + 9] != null && Game1.isWho[pos + 9] is King)
                {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        Game1.shackArray[pos + 9] = true; 
                    }
                }
            }
        }

        public override void CheckPath(int pos) {
            if (pos + 7 <= 63 && Game1.isTaken[pos + 7] == true && Game1.isWho[pos + 7].isWhite != isWhite && pos % 8 != 0) {
                if (Game1.isWho[pos + 7] != null && Game1.isWho[pos + 7] is King) {
                    checkArray[pos + 7] = true;
                }
            }
            if (pos + 9 <= 63 && Game1.isTaken[pos + 9] == true && Game1.isWho[pos + 9].isWhite != isWhite && pos % 8 != 7) {
                if (Game1.isWho[pos + 9] != null && Game1.isWho[pos + 9] is King) {
                    checkArray[pos + 9] = true;
                }
            }
        }
        public override string toString() {
            return "Black Pawn";
        }
    }
}
