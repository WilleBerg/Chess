using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schack
{
    public class Queen : Piece
    {
        public Queen(Texture2D newTexture, Rectangle newRectangle, Vector2 newVector, Vector2 newtempVector, bool newIsWhite, bool[] allowedMoves, bool[] am, bool isDead, int checkCounter, bool[] checkArray) : base(newTexture, newRectangle, newVector, newtempVector, newIsWhite, allowedMoves, am, isDead, checkCounter, checkArray)
        {

        }

        public Queen(Piece a) : base(a) {
        }

        override
        public bool Checker(Rectangle a)
        {
            return am[(a.X / 87) + 8 * (a.Y / 87)];
        }
        override
        public void ActualChecker(int pos, bool lfs)
        {
            am = new bool[64];
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
            for (int i = 1; i < diagUpRight(pos, lfs); i++)
            {
                am[pos - 7 * i] = true;
            }
            for (int i = 1; i < diagDownLeft(pos, lfs); i++)
            {
                am[pos + 7 * i] = true;
            }
            for (int i = 1; i < diagUpLeft(pos, lfs); i++)
            {
                am[pos - 9 * i] = true;
            }
            for (int i = 1; i < diagDownRight(pos, lfs); i++)
            {
                am[pos + i * 9] = true;
            }
            schack = new bool[64];
            SchackChecker(am);
            if (checkCounter < 50) {
                checkCounter++;
            }
        }
        private int diagUpRight(int pos, bool lfs) {
            int tmp = 1;
            
            for (int i = 1; i < 8; i++) {
                if (pos - 7 * i > 0 && (pos - 7 * i) % 8 != 0 && Game1.isTaken[pos - 7 * i] == false) {
                    tmp++;
                } else if (pos - 7 * i > 0 && (pos - 7 * i) % 8 != 0 && Game1.isTaken[pos - 7 * i] == true) {
                    if (Game1.isWho[pos - 7 * i] != null) {
                        if (Game1.isWho[pos - 7 * i].isWhite == isWhite && lfs == false) {
                            break;
                        } else if (Game1.isWho[pos - 7 * i].isWhite == isWhite && lfs == true) {
                            tmp++;
                            break;
                        } else {
                            tmp++;
                            break;
                        }
                    }
                } else {
                    break;
                }
            }
            return tmp;
        }
        private int diagUpLeft(int pos, bool lfs) {
            int tmp = 1;
            if (pos % 8 != 0) {
                for (int i = 1; i < 8; i++) {
                    if (pos - 9 * i >= 0 && (pos - 9 * i) % 8 != 0 && Game1.isTaken[pos - 9 * i] == false) {
                        tmp++;
                    } else if (pos - 9 * i >= 0 && (pos - 9 * i) % 8 == 0 && Game1.isTaken[pos - 9 * i] == false) {
                        tmp++;
                        break;
                    } else if (pos - 9 * i > 0 && (pos - 9 * i) % 8 != 0 && Game1.isTaken[pos - 9 * i] == true) {
                        if (Game1.isWho[pos - 9 * i] != null) {
                            if (Game1.isWho[pos - 9 * i].isWhite == isWhite && lfs == false) {
                                break;
                            } else if (Game1.isWho[pos - 9 * i].isWhite == isWhite && lfs == true) {
                                tmp++;
                                break;
                            } else {
                                tmp++;
                                break;
                            }
                        }
                    } else if (pos - 9 * i > 0 && (pos - 9 * i) % 8 == 0 && Game1.isTaken[pos - 9 * i] == true) {
                        if (Game1.isWho[pos - 9 * i] != null) {
                            if (Game1.isWho[pos - 9 * i].isWhite == isWhite && lfs == false) {
                                break;
                            } else if (Game1.isWho[pos - 9 * i].isWhite == isWhite && lfs == true) {
                                tmp++;
                                break;
                            } else {
                                tmp++;
                                break;
                            }
                        }
                    } else {
                        break;
                    }
                }
            } else {
                return 0;
            }
            return tmp;
        }
        private int diagDownRight(int pos, bool lfs) {
            int tmp = 1;
            for (int i = 1; i < 8; i++) {
                if (pos + 9 * i <= 63 && (pos + 9 * i) % 8 != 0 && Game1.isTaken[pos + 9 * i] == false) {
                    tmp++;
                } else if (pos + 9 * i <= 63 && (pos + 9 * i) % 8 != 0 && Game1.isTaken[pos + 9 * i] == true) {
                    if (Game1.isWho[pos + 9 * i] != null) {
                        if (Game1.isWho[pos + 9 * i].isWhite == isWhite && lfs == false) {
                            break;
                        } else if (Game1.isWho[pos + 9 * i].isWhite == isWhite && lfs == true) {
                            tmp++;
                            break;
                        } else {
                            tmp++;
                            break;
                        }
                    }
                } else {
                    break;
                }
            }
            return tmp;
        }
        private int diagDownLeft(int pos, bool lfs) {
            int tmp = 1;
            if (pos % 8 == 0) {
                return 0;
            }
            for (int i = 1; i < 8; i++) {
                if (pos + 7 * i <= 63 && (pos + 7 * i) % 8 != 0 && Game1.isTaken[pos + 7 * i] == false) {
                    tmp++;
                } else if (pos + 7 * i <= 63 && (pos + 7 * i) % 8 == 0 && Game1.isTaken[pos + 7 * i] == false) {
                    tmp++;
                    break;
                } else if (pos + 7 * i <= 63 && (pos + 7 * i) % 8 == 0 && Game1.isTaken[pos + 7 * i] == true) {
                    if (Game1.isWho[pos + 7 * i] != null) {
                        if (Game1.isWho[pos + 7 * i].isWhite == isWhite && lfs == false) {
                            break;
                        } else if (Game1.isWho[pos + 7 * i].isWhite == isWhite && lfs == true) {
                            tmp++;
                            break;
                        } else {
                            tmp++;
                            break;
                        }
                    }
                } else if (pos + 7 * i <= 63 && (pos + 7 * i) % 8 != 0 && Game1.isTaken[pos + 7 * i] == true) {
                    if (Game1.isWho[pos + 7 * i] != null) {
                        if (Game1.isWho[pos + 7 * i].isWhite == isWhite && lfs == false) {
                            break;
                        } else if (Game1.isWho[pos + 7 * i].isWhite == isWhite && lfs == true) {
                            tmp++;
                            break;
                        } else {
                            tmp++;
                            break;
                        }
                    }
                }
            }
            return tmp;
        }
        private int heightUp(int pos, bool lfs) {
            int tmp = 0;
            for (int i = 1; i < 8; i++) {
                if (pos - 8 * i >= 0 && Game1.isTaken[pos - 8 * i] == false) {
                    tmp++;
                } else if (pos - 8 * i >= 0 && Game1.isTaken[pos - 8 * i] == true && Game1.isWho[pos - 8 * i].isWhite != isWhite && lfs == false) {
                    tmp++;
                    break;
                } else if (pos - 8 * i >= 0 && Game1.isTaken[pos - 8 * i] == true && Game1.isWho[pos - 8 * i].isWhite == isWhite && lfs == true) {
                    tmp++;
                    break;
                } else {
                    break;
                }
            }
            tmp++;
            return tmp;
        }
        private int heightDown(int pos, bool lfs) {
            int tmp = 0;
            for (int i = 1; i < 8; i++) {
                if (pos + 8 * i <= 63 && Game1.isTaken[pos + 8 * i] == false) {
                    tmp++;
                } else if (pos + 8 * i <= 63 && Game1.isTaken[pos + 8 * i] == true && Game1.isWho[pos + 8 * i].isWhite != isWhite && lfs == false) {
                    tmp++;
                    break;
                } else if (pos + 8 * i <= 63 && Game1.isTaken[pos + 8 * i] == true && Game1.isWho[pos + 8 * i].isWhite == isWhite && lfs == true) {
                    tmp++;
                    break;
                } else {
                    break;
                }
            }
            tmp++;
            return tmp++;
        }
        private int widthLeft(int pos, bool lfs) {
            int rnt = 0;
            int tmp = pos - 1;
            if (pos % 8 == 0) {

            } else {
                while (tmp % 8 != 0) {
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
        private int widthRight(int pos, bool lfs) {
            int rnt = 0;
            int tmp = pos + 1;
            while (tmp % 8 != 0) {
                if (Game1.isTaken[tmp] == false) {
                    rnt++;
                    tmp++;
                } else if (Game1.isTaken[tmp] == true && Game1.isWho[tmp].isWhite != isWhite && !lfs) {
                    rnt++;
                    break;
                } else if (Game1.isTaken[tmp] == true && Game1.isWho[tmp].isWhite == isWhite && lfs) {
                    rnt++;
                    break;
                } else {
                    break;
                }
            }
            return rnt;
        }
        public override void ActualSchack(int pos)
        {
            for (int i = 0; i < widthLeft(pos, false); i++) {
                if (Game1.isWho[pos - 1 - i] != null && Game1.isWho[pos - 1 - i] is King && Game1.isWho[pos - 1 - i].isWhite != isWhite) {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos - 1 - j] = true;
                        } 
                    }
                }
            }
            for (int i = 0; i < widthRight(pos, false); i++) {
                if (Game1.isWho[pos + 1 + i] != null && Game1.isWho[pos + 1 + i] is King && Game1.isWho[pos + 1 + i].isWhite != isWhite) {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos + 1 + j] = true;
                        } 
                    }
                }
            }
            for (int i = 1; i < heightUp(pos, false); i++) {
                if (Game1.isWho[pos - 8 * i] != null && Game1.isWho[pos - 8 * i] is King && Game1.isWho[pos - 8 * i].isWhite != isWhite) {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos - 8 * j] = true;
                        } 
                    }
                }
            }
            for (int i = 1; i < heightDown(pos, false); i++) {
                if (Game1.isWho[pos + 8 * i] != null && Game1.isWho[pos + 8 * i] is King && Game1.isWho[pos + 8 * i].isWhite != isWhite) {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos + 8 * j] = true;
                        } 
                    }
                }
            }
            for (int i = 0; i < diagUpRight(pos, false); i++) {
                if (Game1.isWho[pos - 7 * i] != null && Game1.isWho[pos - 7 * i] is King && Game1.isWho[pos - 7 * i].isWhite != isWhite) {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos - 7 * j] = true;
                        }
                    }
                }
            }
            for (int i = 0; i < diagDownLeft(pos, false); i++) {
                if (Game1.isWho[pos + 7 * i] != null && Game1.isWho[pos + 7 * i] is King && Game1.isWho[pos + 7 * i].isWhite != isWhite) {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos + 7 * j] = true;
                        }
                    }
                }
            }
            for (int i = 0; i < diagUpLeft(pos, false); i++) {
                if (Game1.isWho[pos - 9 * i] != null && Game1.isWho[pos - 9 * i] is King && Game1.isWho[pos - 9 * i].isWhite != isWhite) {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos - 9 * j] = true;
                        }
                    }
                }
            }
            for (int i = 0; i < diagDownRight(pos, false); i++) {
                if (Game1.isWho[pos + 9 * i] != null && Game1.isWho[pos + 9 * i] is King && Game1.isWho[pos + 9 * i].isWhite != isWhite) {
                    schack[pos] = true;
                    if (checkCounter == 0) {
                        for (int j = 0; j < i; j++) {
                            Game1.shackArray[pos + 9 * j] = true;
                        }
                    }

                }
            }
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
            for (int i = 0; i < diagUpRight(pos, false); i++) {
                if (Game1.isWho[pos - 7 * i] != null && Game1.isWho[pos - 7 * i] is King && Game1.isWho[pos - 7 * i].isWhite != isWhite) {
                    for (int j = 0; j < i; j++) {
                        checkArray[pos - 7 * j] = true;
                    }
                }
            }
            for (int i = 0; i < diagDownLeft(pos, false); i++) {
                if (Game1.isWho[pos + 7 * i] != null && Game1.isWho[pos + 7 * i] is King && Game1.isWho[pos + 7 * i].isWhite != isWhite) {
                    for (int j = 0; j < i; j++) {
                        checkArray[pos + 7 * j] = true;
                    }
                }
            }
            for (int i = 0; i < diagUpLeft(pos, false); i++) {
                if (Game1.isWho[pos - 9 * i] != null && Game1.isWho[pos - 9 * i] is King && Game1.isWho[pos - 9 * i].isWhite != isWhite) {
                    for (int j = 0; j < i; j++) {
                        checkArray[pos - 9 * j] = true;
                    }
                }
            }
            for (int i = 0; i < diagDownRight(pos, false); i++) {
                if (Game1.isWho[pos + 9 * i] != null && Game1.isWho[pos + 9 * i] is King && Game1.isWho[pos + 9 * i].isWhite != isWhite) {
                    for (int j = 0; j < i; j++) {
                        checkArray[pos + 9 * j] = true;
                    }

                }
            }
        }

        public override string toString() {
            return "Queen";
        }
        public override void SimulateAllowedMoves(int pos, Piece[] isWho, bool[] isTaken, List<Piece> tmpList) {
            am = new bool[64];
            for (int i = 0; i < SimwidthLeft(pos, isWho, isTaken, tmpList); i++) {
                am[pos - 1 - i] = true;
            }
            for (int i = 0; i < SimwidthRight(pos, isWho, isTaken, tmpList); i++) {
                am[pos + 1 + i] = true;
            }
            for (int i = 1; i < SimheightUp(pos, isWho, isTaken, tmpList); i++) {
                am[pos - 8 * i] = true;
            }
            for (int i = 1; i < SimheightDown(pos, isWho, isTaken, tmpList); i++) {
                am[pos + 8 * i] = true;
            }
            for (int i = 1; i < SimdiagUpRight(pos, isWho, isTaken, tmpList); i++) {
                am[pos - 7 * i] = true;
            }
            for (int i = 1; i < SimdiagDownLeft(pos, isWho, isTaken, tmpList); i++) {
                am[pos + 7 * i] = true;
            }
            for (int i = 1; i < SimdiagUpLeft(pos, isWho, isTaken, tmpList); i++) {
                am[pos - 9 * i] = true;
            }
            for (int i = 1; i < SimdiagDownRight(pos, isWho, isTaken, tmpList); i++) {
                am[pos + i * 9] = true;
            }
        }
        private int SimdiagUpRight(int pos, Piece[] isWho, bool[] isTaken, List<Piece> tmpList) {
            int tmp = 1;

            for (int i = 1; i < 8; i++) {
                if (pos - 7 * i > 0 && (pos - 7 * i) % 8 != 0 && Game1.isTaken[pos - 7 * i] == false) {
                    tmp++;
                } else if (pos - 7 * i > 0 && (pos - 7 * i) % 8 != 0 && Game1.isTaken[pos - 7 * i] == true) {
                    if (Game1.isWho[pos - 7 * i] != null) {
                        if (Game1.isWho[pos - 7 * i].isWhite == isWhite) {
                            break;
                        } else {
                            tmp++;
                            break;
                        }
                    }
                } else {
                    break;
                }
            }
            return tmp;
        }
        private int SimdiagUpLeft(int pos, Piece[] isWho, bool[] isTaken, List<Piece> tmpList) {
            int tmp = 1;
            if (pos % 8 != 0) {
                for (int i = 1; i < 8; i++) {
                    if (pos - 9 * i >= 0 && (pos - 9 * i) % 8 != 0 && isTaken[pos - 9 * i] == false) {
                        tmp++;
                    } else if (pos - 9 * i >= 0 && (pos - 9 * i) % 8 == 0 && isTaken[pos - 9 * i] == false) {
                        tmp++;
                        break;
                    } else if (pos - 9 * i > 0 && (pos - 9 * i) % 8 != 0 && isTaken[pos - 9 * i] == true) {
                        if (isWho[pos - 9 * i] != null) {
                            if (isWho[pos - 9 * i].isWhite == isWhite) {
                                break;
                            } else {
                                tmp++;
                                break;
                            }
                        }
                    } else if (pos - 9 * i > 0 && (pos - 9 * i) % 8 == 0 && isTaken[pos - 9 * i] == true) {
                        if (isWho[pos - 9 * i] != null) {
                            if (isWho[pos - 9 * i].isWhite == isWhite) {
                                break;
                            }  else {
                                tmp++;
                                break;
                            }
                        }
                    } else {
                        break;
                    }
                }
            } else {
                return 0;
            }
            return tmp;
        }
        private int SimdiagDownRight(int pos, Piece[] isWho, bool[] isTaken, List<Piece> tmpList) {
            int tmp = 1;
            for (int i = 1; i < 8; i++) {
                if (pos + 9 * i <= 63 && (pos + 9 * i) % 8 != 0 && isTaken[pos + 9 * i] == false) {
                    tmp++;
                } else if (pos + 9 * i <= 63 && (pos + 9 * i) % 8 != 0 && isTaken[pos + 9 * i] == true) {
                    if (isWho[pos + 9 * i] != null) {
                        if (isWho[pos + 9 * i].isWhite == isWhite) {
                            break;
                        } else {
                            tmp++;
                            break;
                        }
                    }
                } else {
                    break;
                }
            }
            return tmp;
        }
        private int SimdiagDownLeft(int pos, Piece[] isWho, bool[] isTaken, List<Piece> tmpList) {
            int tmp = 1;
            if (pos % 8 == 0) {
                return 0;
            }
            for (int i = 1; i < 8; i++) {
                if (pos + 7 * i <= 63 && (pos + 7 * i) % 8 != 0 && isTaken[pos + 7 * i] == false) {
                    tmp++;
                } else if (pos + 7 * i <= 63 && (pos + 7 * i) % 8 == 0 && isTaken[pos + 7 * i] == false) {
                    tmp++;
                    break;
                } else if (pos + 7 * i <= 63 && (pos + 7 * i) % 8 == 0 && isTaken[pos + 7 * i] == true) {
                    if (isWho[pos + 7 * i] != null) {
                        if (isWho[pos + 7 * i].isWhite == isWhite) {
                            break;
                        } else {
                            tmp++;
                            break;
                        }
                    }
                } else if (pos + 7 * i <= 63 && (pos + 7 * i) % 8 != 0 && isTaken[pos + 7 * i] == true) {
                    if (isWho[pos + 7 * i] != null) {
                        if (isWho[pos + 7 * i].isWhite == isWhite) {
                            break;
                        }  else {
                            tmp++;
                            break;
                        }
                    }
                }
            }
            return tmp;
        }
        private int SimheightUp(int pos, Piece[] isWho, bool[] isTaken, List<Piece> tmpList) {
            int tmp = 0;
            for (int i = 1; i < 8; i++) {
                if (pos - 8 * i >= 0 && isTaken[pos - 8 * i] == false) {
                    tmp++;
                } else if (pos - 8 * i >= 0 && isTaken[pos - 8 * i] == true && isWho[pos - 8 * i].isWhite != isWhite) {
                    tmp++;
                    break;
                }  else {
                    break;
                }
            }
            tmp++;
            return tmp;
        }
        private int SimheightDown(int pos, Piece[] isWho, bool[] isTaken, List<Piece> tmpList) {
            int tmp = 0;
            for (int i = 1; i < 8; i++) {
                if (pos + 8 * i <= 63 && isTaken[pos + 8 * i] == false) {
                    tmp++;
                } else if (pos + 8 * i <= 63 && isTaken[pos + 8 * i] == true && isWho[pos + 8 * i].isWhite != isWhite) {
                    tmp++;
                    break;
                } else {
                    break;
                }
            }
            tmp++;
            return tmp++;
        }
        private int SimwidthLeft(int pos, Piece[] isWho, bool[] isTaken, List<Piece> tmpList) {
            int rnt = 0;
            int tmp = pos - 1;
            if (pos % 8 == 0) {

            } else {
                while (tmp % 8 != 0) {
                    if (isTaken[tmp] == false) {
                        rnt++;
                        tmp--;
                    } else if (isTaken[tmp] == true && isWho[tmp].isWhite != isWhite) {
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
        private int SimwidthRight(int pos, Piece[] isWho, bool[] isTaken, List<Piece> tmpList) {
            int rnt = 0;
            int tmp = pos + 1;
            while (tmp % 8 != 0) {
                if (isTaken[tmp] == false) {
                    rnt++;
                    tmp++;
                } else if (isTaken[tmp] == true && isWho[tmp].isWhite != isWhite) {
                    rnt++;
                    break;
                } else {
                    break;
                }
            }
            return rnt;
        }
    }
}
