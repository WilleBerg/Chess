using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schack {
    class Bishop : Piece {
        public Bishop(Texture2D newTexture, Rectangle newRectangle, Vector2 newVector, Vector2 newtempVector, bool newIsWhite, bool[] allowedMoves, bool[] am, bool isDead, int checkCounter, bool[] checkArray) : base(newTexture, newRectangle, newVector, newtempVector, newIsWhite, allowedMoves, am, isDead, checkCounter, checkArray) {
        }

        public override void ActualChecker(int pos, bool lfs) {
            for (int i = 1; i < diagUpRight(pos, lfs); i++) {
                am[pos - 7 * i] = true;
            }
            for (int i = 1; i < diagDownLeft(pos, lfs); i++) {
                am[pos + 7 * i] = true;
            }
            for (int i = 1; i < diagUpLeft(pos, lfs); i++) {
                am[pos - 9 * i] = true;
            }
            for (int i = 1; i < diagDownRight(pos, lfs); i++) {
                am[pos + i * 9] = true;
            }
            schack = new bool[64];
            SchackChecker(am);
            if (checkCounter < 50) {
                checkCounter++;
            }
        }

        public override bool Checker(Rectangle a) {
            return am[(a.X / 87) + 8 * (a.Y / 87)];
        }
        private int diagUpRight(int pos, bool lfs) {
            int tmp = 1;
            for (int i = 1; i < 8; i++) {
                if (pos - 7 * i > 0 && (pos - 7 * i) % 8 != 0 && Game1.isTaken[pos - 7 * i] == false) {
                    tmp++;
                } else if (pos - 7 * i > 0 && (pos - 7 * i) % 8 != 0 && Game1.isTaken[pos - 7 * i] == true) {
                    if (Game1.isWho[pos - 7 * i] != null) {
                        if (Game1.isWho[pos - 7 *i].isWhite == isWhite && lfs == false) {
                            break;
                        } else if (Game1.isWho[pos - 7 * i].isWhite == isWhite && lfs == true) {
                            tmp++;
                            break;
                        } else {
                            tmp++;
                            break;
                        }
                    }
                } 
                else {
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
            if (pos % 8 == 0)
            {
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
        public override void ActualSchack(int pos) {
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
        public override string toString() {
            return "Bishop";
        }
        public override void CheckPath(int pos) {
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
    }
}
