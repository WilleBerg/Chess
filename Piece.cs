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
    public abstract class Piece
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 vector;
        public Vector2 tempPos;
        public bool isWhite;
        public bool isDead;
        public bool[] schack;
        public bool[] allowedMoves { get; set; }
        public bool[] am;
        public int checkCounter;
        public Piece(Texture2D newTexture, Rectangle newRectangle, Vector2 newVector, Vector2 newtempVector, bool newIsWhite, bool[] allowedMoves, bool[] am, bool isDead, int checkCounter)
        {
            texture = newTexture;
            rectangle = newRectangle;
            vector = newVector;
            tempPos = newtempVector;
            isWhite = newIsWhite;
            this.allowedMoves = allowedMoves;
            this.am = am;
            this.isDead = isDead;
            this.checkCounter = checkCounter;
        }
        protected Piece(Texture2D texture, Rectangle rectangle, Vector2 vector, Vector2 tempPos, bool isWhite, bool isDead, bool[] shack, bool[] allowedMoves, bool[] am) {
            this.texture = texture;
            this.rectangle = rectangle;
            this.vector = vector;
            this.tempPos = tempPos;
            this.isWhite = isWhite;
            this.isDead = isDead;
            this.schack = shack;
            this.allowedMoves = allowedMoves;
            this.am = am;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public abstract bool Checker(Rectangle aa);
        public abstract void ActualChecker(int pos, bool lfs); // lfs = Looking For Schack

        public void SchackChecker(bool[] arr) {
            Queue<int> q = new Queue<int>();
            for (int i = 0; i < arr.Length; i++) {
                if (arr[i] || i == Game1.getBoard((int)tempPos.X, (int)tempPos.Y)) {
                    q.Enqueue(i);
                }
            }
            while (q.Count > 0) {
                int tmp = q.Dequeue();
                ActualSchack(tmp);
            }
        }

        public abstract void ActualSchack(int pos);



    }
}
