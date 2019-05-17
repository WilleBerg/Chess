using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schack
{
    class Knight : Piece
    {
        public Knight(Texture2D newTexture, Rectangle newRectangle, Vector2 newVector, Vector2 newtempVector, bool newIsWhite, bool[] allowedMoves, bool[] am, bool isDead, int checkCounter) : base(newTexture, newRectangle, newVector, newtempVector, newIsWhite, allowedMoves, am, isDead, checkCounter)
        {
        }

        public override void ActualChecker(int pos, bool lfs)
        {
            if (DownLeftFirst(pos, lfs))
            {
                am[pos + 6] = true;
            }
            if (DownLeftSecond(pos, lfs))
            {
                am[pos + 15] = true;
            }
            if (DownRightFirst(pos, lfs))
            {
                am[pos + 10] = true;
            }
            if (DownRightSecond(pos, lfs))
            {
                am[pos + 17] = true;
            }
            if (UpLeftFirst(pos, lfs))
            {
                am[pos - 10] = true;
            }
            if (UpLeftSecond(pos, lfs))
            {
                am[pos - 17] = true;
            }
            if (UpRightFirst(pos, lfs))
            {
                am[pos - 6] = true;
            }
            if (UpRightSecond(pos, lfs))
            {
                am[pos - 15] = true;
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
        private bool DownLeftFirst(int pos, bool lfs)
        {
            if (pos + 6 <= 63 && !Game1.isTaken[pos + 6] && !((((pos % 8) == 0) || (pos % 8) == 1)))
            {
                return true;
            }
            else if (pos + 6 <= 63 && Game1.isTaken[pos + 6] && (!((((pos % 8) == 0) || (pos % 8) == 1))) && Game1.isWho[pos + 6].isWhite != isWhite && !lfs)
            {
                return true;
            } else if (pos + 6 <= 63 && Game1.isTaken[pos + 6] && (!((((pos % 8) == 0) || (pos % 8) == 1))) && Game1.isWho[pos + 6].isWhite == isWhite && lfs) {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool DownLeftSecond(int pos, bool lfs)
        {
            if ((pos + 15 <= 63 && !Game1.isTaken[pos + 15]) && !((pos % 8) == 0))
            {
                return true;
            }
            else if ((pos + 15 <= 63 && Game1.isTaken[pos + 15]) && !((pos % 8) == 0) && Game1.isWho[pos + 15].isWhite != isWhite && !lfs)
            {
                return true;
            } else if (pos + 15 <= 63 && Game1.isTaken[pos + 15] && !((pos % 8) == 0) && Game1.isWho[pos + 15].isWhite == isWhite && lfs) {
                return true;
            } else
            {
                return false;
            }
        }
        private bool DownRightFirst(int pos, bool lfs)
        {
            if (pos + 10 <= 63 && !Game1.isTaken[pos + 10] && !((((pos % 8) == 7) || (pos % 8) == 6)))
            {
                return true;
            }
            else if (pos + 10 <= 63 && Game1.isTaken[pos + 10] && (!((((pos % 8) == 7) || (pos % 8) == 6))) && Game1.isWho[pos + 10].isWhite != isWhite && !lfs)
            {
                return true;
            } else if (pos + 10 <= 63 && Game1.isTaken[pos + 10] && (!((((pos % 8) == 7) || (pos % 8) == 6))) && Game1.isWho[pos + 10].isWhite == isWhite && lfs) {
                return true;
            } else
            {
                return false;
            }
        }
        private bool DownRightSecond(int pos, bool lfs)
        {
            if (pos + 17 <= 63 && !Game1.isTaken[pos + 17] && !((pos % 8) == 7))
            {
                return true;
            }
            else if (pos + 17 <= 63 && Game1.isTaken[pos + 17] && !((pos % 8) == 7) && Game1.isWho[pos + 17].isWhite != isWhite && !lfs)
            {
                return true;
            } else if (pos + 17 <= 63 && Game1.isTaken[pos + 17] && !((pos % 8) == 7) && Game1.isWho[pos + 17].isWhite == isWhite && lfs) {
                return true;
            } else
            {
                return false;
            }
        }

        private bool UpRightFirst(int pos, bool lfs)
        {
            if (pos - 6 >= 0 && !Game1.isTaken[pos - 6] && !((((pos % 8) == 6) || (pos % 8) == 7)))
            {
                return true;
            }
            else if (pos - 6 >= 0 && Game1.isTaken[pos - 6] && (!((((pos % 8) == 6) || (pos % 8) == 7)) && Game1.isWho[pos - 6].isWhite != isWhite) && !lfs)
            {
                return true;
            } else if (pos - 6 >= 0 && Game1.isTaken[pos - 6] && (!((((pos % 8) == 6) || (pos % 8) == 7)) && Game1.isWho[pos - 6].isWhite == isWhite) && lfs) {
                return true;
            } else
            {
                return false;
            }
        }
        private bool UpRightSecond(int pos, bool lfs)
        {
            if (pos - 15 >= 0 && !Game1.isTaken[pos - 15] && !((pos % 8) == 7))
            {
                return true;
            }
            else if (pos - 15 >= 0 && Game1.isTaken[pos - 15] && !((pos % 8) == 7) && Game1.isWho[pos - 15].isWhite != isWhite && !lfs)
            {
                return true;
            } else if (pos - 15 >= 0 && Game1.isTaken[pos - 15] && !((pos % 8) == 7) && Game1.isWho[pos - 15].isWhite == isWhite && lfs) {
                return true;
            } else
            {
                return false;
            }
        }
        private bool UpLeftFirst(int pos, bool lfs)
        {
            if (pos - 10 >= 0 && !Game1.isTaken[pos - 10] && !((((pos % 8) == 0) || (pos % 8) == 1)))
            {
                return true;
            }
            else if (pos - 10 >= 0 && Game1.isTaken[pos - 10] && (!((((pos % 8) == 0) || (pos % 8) == 1))) && Game1.isWho[pos - 10].isWhite != isWhite && !lfs)
            {
                return true;
            } else if (pos - 10 >= 0 && Game1.isTaken[pos - 10] && (!((((pos % 8) == 0) || (pos % 8) == 1))) && Game1.isWho[pos - 10].isWhite == isWhite && lfs) {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool UpLeftSecond(int pos, bool lfs)
        {
            if (pos - 17 >= 0 && !Game1.isTaken[pos - 17] && !((pos % 8) == 0))
            {
                return true;
            }
            else if (pos - 17 >= 0 && Game1.isTaken[pos - 17] && !((pos % 8) == 0) && Game1.isWho[pos - 17].isWhite != isWhite && !lfs)
            {
                return true;
            } else if (pos - 17 >= 0 && Game1.isTaken[pos - 17] && !((pos % 8) == 0) && Game1.isWho[pos - 17].isWhite == isWhite && lfs) {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void ActualSchack(int pos)
        {
            if (UpLeftSecond(pos, false) && Game1.isWho[pos - 17] != null && Game1.isWho[pos - 17] is King && Game1.isWho[pos - 17].isWhite != isWhite)
            {
                schack[pos] = true;
                if (checkCounter == 0) {
                    Game1.shackArray[pos - 17] = true; 
                }
            }
            if (UpLeftFirst(pos, false) && Game1.isWho[pos - 10] != null && Game1.isWho[pos - 10] is King && Game1.isWho[pos - 10].isWhite != isWhite)
            {
                schack[pos] = true;
                if (checkCounter == 0) {
                    Game1.shackArray[pos - 10] = true; 
                }
            }
            if (UpRightFirst(pos, false) && Game1.isWho[pos - 6] != null && Game1.isWho[pos - 6] is King && Game1.isWho[pos - 6].isWhite != isWhite)
            {
                schack[pos] = true;
                if (checkCounter == 0) {
                    Game1.shackArray[pos - 6] = true; 
                }
            }
            if (DownRightFirst(pos, false) && Game1.isWho[pos + 10] != null && Game1.isWho[pos + 10] is King && Game1.isWho[pos + 10].isWhite != isWhite)
            {
                schack[pos] = true;
                if (checkCounter == 0) {
                    Game1.shackArray[pos + 10] = true; 
                }
            }
            if (UpRightSecond(pos, false) && Game1.isWho[pos - 15] != null && Game1.isWho[pos - 15] is King && Game1.isWho[pos - 15].isWhite != isWhite)
            {
                schack[pos] = true;
                if (checkCounter == 0) {
                    Game1.shackArray[pos - 15] = true; 
                }
            }
            if (DownRightSecond(pos, false) && Game1.isWho[pos + 17] != null && Game1.isWho[pos + 17] is King && Game1.isWho[pos + 17].isWhite != isWhite)
            {
                schack[pos] = true;
                if (checkCounter == 0) {
                    Game1.shackArray[pos + 17] = true; 
                }
            }
            if (DownLeftFirst(pos, false) && Game1.isWho[pos + 6] != null && Game1.isWho[pos + 6] is King && Game1.isWho[pos + 6].isWhite != isWhite)
            {
                schack[pos] = true;
                if (checkCounter == 0) {
                    Game1.shackArray[pos + 6] = true; 
                }
            }
            if (DownLeftSecond(pos, false) && Game1.isWho[pos + 15] != null && Game1.isWho[pos + 15] is King && Game1.isWho[pos + 15].isWhite != isWhite)
            {
                schack[pos] = true;
                if (checkCounter == 0) {
                    Game1.shackArray[pos + 15] = true; 
                }
            }
        }
    }
}

