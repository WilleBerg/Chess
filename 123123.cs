using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace stridsvagnar
{

    public class Game1 : Game
    {
        SoundEffect skjut;
        SoundEffect träff;        
        int rotationTal = 4;
        float stridsvagnHastighet = 3.5f;
        float stridsvagnHastighet2 = 3.5f;
        List<Rectangle> vänsterVäggLista = new List<Rectangle>();
        List<Rectangle> högerVäggLista = new List<Rectangle>();
        List<Rectangle> takLista = new List<Rectangle>();
        List<Rectangle> golvLista = new List<Rectangle>();        
        bool kanSkadaStridsvagn, kanSkadaStridsvagn2;
        double timer = 0;
        MouseState mus = Mouse.GetState();
        MouseState gammalMus = Mouse.GetState();
        int stridsvagnHP = 4;
        int stridsvagn2HP = 4;
        int skottHP = 1;
        int skott2HP = 1;
        int standardSkottHP = 4;
        int standardSkott2HP = 4;
        int scen = 0;
        Vector2 skottRiktning, skottRiktning2;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        double rotation = 0;
        double rotation2 = 0;
        Texture2D stridsvagnBild, stridsvagnBild2, skottBild, spelaKnappBild, bakgrundsbild, vinnareBild, vinnare2Bild,alternativKnappBild,alternativBild, kontrollerBild,tillbakaBild, stridsvagnarTXT;
        KeyboardState tangentBord = Keyboard.GetState();
        Rectangle stridsvagn = new Rectangle(400, 400, 45, 45);
        Rectangle stridsvagn2 = new Rectangle(1100, 350, 45, 45);
        Rectangle skottHitbox = new Rectangle(-5, 0, 10, 10);
        Rectangle skottHitbox2 = new Rectangle(-5, 0, 10, 10);
        Rectangle stridsvagnHitbox = new Rectangle(0, 0, 45, 45);
        Rectangle stridsvagn2Hitbox = new Rectangle(0, 0, 45, 45);
        Rectangle musHitbox = new Rectangle(0, 0, 9, 9);
        Rectangle spelaKnappHitbox = new Rectangle(500, 300, 300, 150);
        Rectangle stridsvagnHpHitbox = new Rectangle(-10, 0, 10, 5);
        Rectangle alternativKnappHitbox = new Rectangle(580, 450, 150, 150);
        Rectangle tillbakaHitbox = new Rectangle(50, 50, 100, 100);

        Vector2 stridsvagnPos, stridsvagnPos2;
        Vector2 stridsvagnVector = new Vector2(359, 400);
        Vector2 stridsvagnVector2 = new Vector2(800, 400);
        Vector2 skottVecktor = new Vector2(0, -20);
        Vector2 skottVecktor2 = new Vector2(0, -20);
        SpriteFont arial;
        double stridsvagnRiktningX, stridsvagnRiktningY, stridsvagn2RiktningX, stridsvagn2RiktningY;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1400;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            LäggTillSpelPlan();
           





            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            bakgrundsbild = Content.Load<Texture2D>("world-of-tanks-background-9");
            vinnareBild = Content.Load<Texture2D>("wot19201080art_large");
            vinnare2Bild = Content.Load<Texture2D>("wallpaper2you_292398");
            arial = Content.Load<SpriteFont>("arial");
            stridsvagnBild = Content.Load<Texture2D>("preview_344");
            stridsvagnBild2 = Content.Load<Texture2D>("cartoon_tank_by_nasnet-d3gpubb");
            skottBild = Content.Load<Texture2D>("näbb biologi");
            spelaKnappBild = Content.Load<Texture2D>("btnPlay (1)");
            träff = Content.Load<SoundEffect>("Torpedo+Explosion");
            skjut = Content.Load<SoundEffect>("Tank Firing-SoundBible.com-998264747");
            alternativKnappBild = Content.Load<Texture2D>("icon-832005_960_720");
            alternativBild = Content.Load<Texture2D>("f-nkchina-a-20151011-870x726");
            kontrollerBild = Content.Load<Texture2D>("KeyboardControls");
            tillbakaBild = Content.Load<Texture2D>("back");
            stridsvagnarTXT = Content.Load<Texture2D>("stridsvagnar");
        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.TargetElapsedTime = TimeSpan.FromSeconds(1f / 60f);
            Timer();
            tangentBord = Keyboard.GetState();
            
            FlyttaStridsvagn();
            StridsvagnRörelse();
            SkottAxelration();
            
            KollaSkott2HP();
            KollaKollition();
            StridsvagnHitbox();
            gammalMus = mus;
            Mus();
            ÄrStridsvagnDöd();
            KollaKollitionKnapp();
            ÄrSkottBorta();
            KollaSkott();
            
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            switch (scen)
            {
                case 0:
                    RitaMeny();
                    break;
                case 1:

                    RitaSpel();
                    break;
                case 2:
                    RitaSpelare1Vann();
                    break;
                case 3:
                    RitaSpelare2Vann();
                    break;
                case 4:
                    RitaAlternativ();
                    break;
            }





            base.Draw(gameTime);
        }
        
        void FlyttaStridsvagn()
        {
            if (tangentBord.IsKeyDown(Keys.W))
            {
                
                    if ((NuddarStridsvagnTak() && stridsvagnRiktningY < 0) || NuddarStridsvagnGolv() && stridsvagnRiktningY > 0)
                    {
                        if (NuddarStridsvagnTak() && (NuddarStridsvagnVänsterVägg() || NuddarStridsvagnHögerVägg()) || (NuddarStridsvagnGolv() && (NuddarStridsvagnHögerVägg() || NuddarStridsvagnVänsterVägg())))
                        {
                            stridsvagnVector.Y -= (float)stridsvagnRiktningY;
                        }
                    }
                    else
                    {
                        stridsvagnVector.Y -= (float)stridsvagnRiktningY;
                    }
                   
                
                if ((stridsvagnRiktningX > 0 || stridsvagn.X < 1370) && (KanRöraSigVänster() && KanRöraSigHöger()) && (stridsvagnRiktningX < 0 || stridsvagn.X > 20))
                {
                    stridsvagnVector.X -= (float)stridsvagnRiktningX;
                }


            }
            if (tangentBord.IsKeyDown(Keys.S))
            {
                
                    if (NuddarStridsvagnTak() && stridsvagnRiktningY > 0 || NuddarStridsvagnGolv() && stridsvagnRiktningY < 0)
                    {
                        if (NuddarStridsvagnTak() && (NuddarStridsvagnVänsterVägg()) || (NuddarStridsvagnGolv() && (NuddarStridsvagnHögerVägg() || NuddarStridsvagnVänsterVägg())))
                        {
                            stridsvagnVector.Y += (float)stridsvagnRiktningY;
                        }
                    }
                    else
                    {
                        stridsvagnVector.Y += (float)stridsvagnRiktningY;
                    }
                
                if ((stridsvagnRiktningX < 0 || stridsvagn.X < 1370) )
                {
                    if (((NuddarStridsvagnVänsterVägg() && stridsvagnRiktningX > 0)) || ((NuddarStridsvagnHögerVägg() && stridsvagnRiktningX < 0)))
                    {     
                        
                    }                    
                    else
                    {
                        stridsvagnVector.X += (float)stridsvagnRiktningX;
                    }
                }
            }
            if (tangentBord.IsKeyDown(Keys.Up))
            {
               
                if ((stridsvagn2RiktningY > 0 || stridsvagn2.Y < 670))
                {
                    if ((NuddarStridsvagn2Tak() && stridsvagn2RiktningY < 0) || NuddarStridsvagn2Golv() && stridsvagn2RiktningY > 0)
                    {
                      
                        if (NuddarStridsvagn2Tak() && (NuddarStridsvagn2VänsterVägg() || (NuddarStridsvagn2Golv()) && (NuddarStridsvagn2HögerVägg() || NuddarStridsvagn2VänsterVägg())))
                        {

                            stridsvagnVector2.Y -= (float)stridsvagn2RiktningY;
                        }
                    }
                    else
                    {
                        
                        stridsvagnVector2.Y -= (float)stridsvagn2RiktningY;
                    }

                }
                if ((stridsvagn2RiktningX > 0 || stridsvagn2.X < 1370) && (KanRöraSigVänster2() && KanRöraSigHöger2()))
                {
                    stridsvagnVector2.X -= (float)stridsvagn2RiktningX;
                }

            }
            if (tangentBord.IsKeyDown(Keys.Down))
            {

                if (NuddarStridsvagnTak() && stridsvagn2RiktningY > 0 || NuddarStridsvagn2Golv() && stridsvagn2RiktningY < 0)
                {
                    if (NuddarStridsvagn2Tak() && (NuddarStridsvagn2VänsterVägg()) || (NuddarStridsvagn2Golv() && (NuddarStridsvagn2HögerVägg() || NuddarStridsvagn2VänsterVägg())))
                    {
                        stridsvagnVector2.Y += (float)stridsvagn2RiktningY;
                    }
                }
                else
                {
                    stridsvagnVector2.Y += (float)stridsvagn2RiktningY;
                }

                if ((stridsvagn2RiktningX < 0 || stridsvagn2.X < 1370))
                {
                    if (((NuddarStridsvagn2VänsterVägg() && stridsvagn2RiktningX > 0)) || ((NuddarStridsvagn2HögerVägg() && stridsvagn2RiktningX < 0)))
                    {

                    }
                    else
                    {
                        stridsvagnVector2.X += (float)stridsvagn2RiktningX;
                    }
                }
            }
            if (tangentBord.IsKeyDown(Keys.A))
            {
                rotation -= rotationTal;
            }
            if (tangentBord.IsKeyDown(Keys.D))
            {
                rotation += rotationTal;

            }
            if (tangentBord.IsKeyDown(Keys.Left))
            {
                rotation2 -= rotationTal;

            }
            if (tangentBord.IsKeyDown(Keys.Right))
            {
                rotation2 += rotationTal;
            }
            if (tangentBord.IsKeyDown(Keys.RightControl) && SkottBorta2())
            {
                skjut.Play();
                skott2HP = standardSkott2HP;
                skottRiktning2.X = (float)stridsvagn2RiktningX;
                skottRiktning2.Y = (float)stridsvagn2RiktningY;
                skottVecktor2.X = stridsvagn2.X;
                skottVecktor2.Y = stridsvagn2.Y;

                Stridsvagn2Skjut();
            }
            if (tangentBord.IsKeyDown(Keys.Space) && SkottBorta())
            {
                skjut.Play();
                skottHP = standardSkottHP;
                skottRiktning.X = (float)stridsvagnRiktningX;
                skottRiktning.Y = (float)stridsvagnRiktningY;
                skottVecktor.X = stridsvagn.X;
                skottVecktor.Y = stridsvagn.Y;

                StridsvagnSkjut();
            }

        }
        void Mus()
        {
            gammalMus = mus;
            mus = Mouse.GetState();
            
        }
        void StridsvagnRörelse()
        {
            stridsvagn.X = (int)stridsvagnVector.X;
            stridsvagn.Y = (int)stridsvagnVector.Y;
            stridsvagn2.X = (int)stridsvagnVector2.X;
            stridsvagn2.Y = (int)stridsvagnVector2.Y;
            stridsvagnPos = new Vector2(stridsvagnBild.Width / 2, stridsvagnBild.Height / 2);
            stridsvagnPos2 = new Vector2(stridsvagnBild2.Width / 2, stridsvagnBild2.Height / 2);
            stridsvagnRiktningY = (Math.Sin(((rotation) + 90) * (Math.PI / 180))) * stridsvagnHastighet;
            stridsvagnRiktningX = (Math.Cos((((rotation) + 90)) * (Math.PI / 180))) * stridsvagnHastighet;
            stridsvagn2RiktningY = (Math.Sin(((rotation2) + 90) * (Math.PI / 180))) * stridsvagnHastighet2;
            stridsvagn2RiktningX = (Math.Cos((((rotation2) + 90)) * (Math.PI / 180))) * stridsvagnHastighet2;
        }       
        void StridsvagnSkjut()
        {
            skottHitbox.X = stridsvagnHitbox.X+15;
            skottHitbox.Y = stridsvagnHitbox.Y+15;
            skottHitbox.X = (int)skottVecktor.X;
            skottHitbox.Y = (int)skottVecktor.Y;
        }
        void StridsvagnHitbox()
        {
            stridsvagnHitbox.X = stridsvagn.X -22;
            stridsvagnHitbox.Y = stridsvagn.Y -22;
            stridsvagn2Hitbox.X = stridsvagn2.X - 22;
            stridsvagn2Hitbox.Y = stridsvagn2.Y - 22;
        }
        void Stridsvagn2Skjut()
        {
            skottHitbox2.X = stridsvagn2Hitbox.X+26;
            skottHitbox2.Y = stridsvagn2Hitbox.Y+26;
        }
        void SkottAxelration()
        {
            skottVecktor.X -= skottRiktning.X;
            skottVecktor.Y -= skottRiktning.Y;
            skottVecktor2.X -= skottRiktning2.X;
            skottVecktor2.Y -= skottRiktning2.Y;
            skottHitbox.X = (int)skottVecktor.X;
            skottHitbox.Y = (int)skottVecktor.Y;
            skottHitbox2.X = (int)skottVecktor2.X;
            skottHitbox2.Y = (int)skottVecktor2.Y;
        }       
        void KollaSkott()
        {

            for (int i = 0; i < vänsterVäggLista.Count; i++)
            {
                if (skottHitbox.Intersects(vänsterVäggLista[i]))
                {
                    if (skottHitbox.Intersects(golvLista[i]) && skottRiktning.X < 0)
                    {
                        skottHP++;
                        skottRiktning.Y *= -1;
                        skottHP -= 1;
                        kanSkadaStridsvagn = true;
                    }
                    else
                    {
                        skottHP -= 1;
                        skottRiktning.X *= -1;
                        kanSkadaStridsvagn = true;
                    }
                   
                }
            }
            for (int i = 0; i < högerVäggLista.Count; i++)
            {
                if (skottHitbox.Intersects(högerVäggLista[i]))
                {
                    if (skottHitbox.Intersects(golvLista[i]) && skottHitbox.Intersects(högerVäggLista[i]))
                    {
                        skottHP++;
                        skottRiktning.Y *= -1;
                    }
                    skottHP -= 1;
                    skottRiktning.X *= -1;
                    kanSkadaStridsvagn = true;
                }
            }
            for (int i = 0; i < takLista.Count; i++)
            {
                if (skottHitbox.Intersects(takLista[i]) && skottRiktning.Y < 0)
                {
                    skottHP -= 1;
                    skottRiktning.Y *= -1;
                    kanSkadaStridsvagn = true;
                }
            }
            for (int i = 0; i < golvLista.Count; i++)
            {
                if (skottHitbox.Intersects(golvLista[i]) && skottRiktning.Y > 0)
                {
                    skottHP -= 1;
                    skottRiktning.Y *= -1;
                    kanSkadaStridsvagn = true;

                }
                

            }
            

        }
        void KollaSkott2HP()
        {
            for (int i = 0; i < vänsterVäggLista.Count; i++)
            {
                if (skottHitbox2.Intersects(vänsterVäggLista[i]))
                {
                    skott2HP -= 1;
                    skottRiktning2.X *= -1;
                    kanSkadaStridsvagn2 = true;
                }
            }
            for (int i = 0; i < högerVäggLista.Count; i++)
            {
                if (skottHitbox2.Intersects(högerVäggLista[i]))
                {
                    skott2HP -= 1;
                    skottRiktning2.X *= -1;
                    kanSkadaStridsvagn2 = true;
                }
            }
            for (int i = 0; i < takLista.Count; i++)
            {
                if (skottHitbox2.Intersects(takLista[i]))
                {
                    skott2HP -= 1;
                    skottRiktning2.Y *= -1;
                    kanSkadaStridsvagn2 = true;
                }
            }
            for (int i = 0; i < golvLista.Count; i++)
            {
                if (skottHitbox2.Intersects(golvLista[i]))
                {
                    skott2HP -= 1;
                    skottRiktning2.Y *= -1;
                    kanSkadaStridsvagn2 = true;
                }
            }
        }
        void KollaKollition()
        {
            if (skottHitbox.Intersects(stridsvagnHitbox) && kanSkadaStridsvagn)
            {
                träff.Play();
                stridsvagnHP--;
                skottVecktor.X = -304;
                skottHP = 0;
            }
            if (skottHitbox.Intersects(stridsvagn2Hitbox))
            {
                träff.Play();
                stridsvagn2HP--;
                skottVecktor.X = -34;
                skottHP = 0;
            }
            if (skottHitbox2.Intersects(stridsvagn2Hitbox) && kanSkadaStridsvagn2)
            {
                träff.Play();
                stridsvagn2HP--;
                skottVecktor2.X = -34;
                skott2HP = 0;
            }
            if (skottHitbox2.Intersects(stridsvagnHitbox))
            {
                träff.Play();
                stridsvagnHP--;
                skottVecktor2.X = -34;
                skott2HP = 0;
            }
        }
        void Timer()
        {
            timer++;

        }
        void RitaSpel()
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();

            spriteBatch.Draw(skottBild, skottHitbox, Color.DarkBlue);
            spriteBatch.Draw(skottBild, skottHitbox2, Color.Green);
            
            spriteBatch.Draw(stridsvagnBild2, stridsvagn2, null, Color.White, (float)(rotation2 * (Math.PI / 180)), stridsvagnPos2, SpriteEffects.None, 0);
            
        
            spriteBatch.Draw(stridsvagnBild, stridsvagn, null, Color.White, (float)(rotation * (Math.PI / 180)), stridsvagnPos, SpriteEffects.None, 0);
            DrawSpelplan();


           // spriteBatch.DrawString(arial, $"stridsvang1HP:{stridsvagnHP} -{test} stridsvagn2HP::::::::{stridsvagn2HP} ::: ", new Vector2(100, 100), Color.White);
           // spriteBatch.DrawString(arial, $" {SkottBorta()} :  {stridsvagnRiktningX}", new Vector2(100, 150), Color.White);
            spriteBatch.DrawString(arial, $" ", new Vector2(100, 200), Color.White);
            spriteBatch.End();
        }
        void RitaMeny()
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            spriteBatch.Begin();
            spriteBatch.Draw(bakgrundsbild, new Rectangle(0, 0, 1400, 710), Color.White);
            spriteBatch.Draw(spelaKnappBild, spelaKnappHitbox, Color.White);
            spriteBatch.Draw(stridsvagnarTXT, new Rectangle(410,100,500,500), Color.Blue);
            spriteBatch.Draw(alternativKnappBild, alternativKnappHitbox, Color.White);
            spriteBatch.End();
        }
        void RitaSpelare1Vann()
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            spriteBatch.Begin();
            spriteBatch.Draw(vinnareBild, new Rectangle(0, 0, 1400, 710), Color.White);
            spriteBatch.Draw(spelaKnappBild, spelaKnappHitbox, Color.White);
            spriteBatch.DrawString(arial, $"SPELARE 1 VANN", new Vector2(600, 100), Color.White);
            spriteBatch.End();
        }
        void RitaSpelare2Vann()
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            spriteBatch.Begin();
            spriteBatch.Draw(vinnare2Bild, new Rectangle(0, 0, 1400, 710), Color.White);
            spriteBatch.Draw(spelaKnappBild, spelaKnappHitbox, Color.White);
            spriteBatch.DrawString(arial, $"SPELARE 2 VANN", new Vector2(600, 100), Color.White);
            spriteBatch.End();
        }
        void RitaAlternativ()

        {
            GraphicsDevice.Clear(Color.ForestGreen);

            spriteBatch.Begin();
            
            spriteBatch.Draw(alternativBild, new Rectangle(0, 0, 1400, 710), Color.White);
            spriteBatch.Draw(kontrollerBild, new Rectangle(200, 200, 900, 400), Color.White);
            spriteBatch.Draw(tillbakaBild, tillbakaHitbox, Color.White);
            spriteBatch.End();
        }
        void LäggTillSpelPlan()
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    vänsterVäggLista.Add(new Rectangle(100 + i * 150, 100 + 200 * j, 10, 100));
                }
            }

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    högerVäggLista.Add(new Rectangle(110 + i * 150, 100 + 200 * j, 10, 100));
                   
                }
            }

            
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    takLista.Add(new Rectangle(100 + i * 150, 90 + 200 * j, 20, 10));
                }
            }

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    golvLista.Add(new Rectangle(100 + i * 150, 200 + 200 * j, 20, 10));
                }
            }
            golvLista.Add(new Rectangle(0, 0, 1700, 2));
            takLista.Add(new Rectangle(0, 699, 1700, 3));
            högerVäggLista.Add(new Rectangle(2, 0, 2, 700));
            vänsterVäggLista.Add(new Rectangle(1397, 0, 2, 700));
        }
        void DrawSpelplan()
        {
            for (int i = 0; i < vänsterVäggLista.Count; i++)
            {
                spriteBatch.Draw(skottBild, vänsterVäggLista[i], Color.Blue);
            }
            for (int i = 0; i < högerVäggLista.Count; i++)
            {
                spriteBatch.Draw(skottBild, högerVäggLista[i], Color.White);
            }
            for (int i = 0; i < takLista.Count; i++)
            {
                spriteBatch.Draw(skottBild, takLista[i], Color.Red);
            }
            for (int i = 0; i < golvLista.Count; i++)
            {
                spriteBatch.Draw(skottBild, golvLista[i], Color.Green);
            }
            
        }
        void KollaKollitionKnapp()
        {
            if (tillbakaHitbox.Contains(mus.Position))
            {
                tillbakaHitbox.Width = 120;
                tillbakaHitbox.Height = 120;
                tillbakaHitbox.X = 40;
                tillbakaHitbox.Y = 40;
            }
            else
            {
                tillbakaHitbox.Width = 100;
                tillbakaHitbox.Height = 100;
                tillbakaHitbox.X = 50;
                tillbakaHitbox.Y = 50;
            }
            
            if (spelaKnappHitbox.Contains(mus.Position))
            {
                spelaKnappHitbox.Width = 320;
                spelaKnappHitbox.Height = 170;
                spelaKnappHitbox.X = 490;
                spelaKnappHitbox.Y = 290;
            }
            else
            {
                spelaKnappHitbox.Width = 300;
                spelaKnappHitbox.Height = 150;
                spelaKnappHitbox.X = 500;
                spelaKnappHitbox.Y = 300;

            }
            if (alternativKnappHitbox.Contains(mus.Position))
            {
                alternativKnappHitbox.Width = 160;
                alternativKnappHitbox.Height = 160;
                alternativKnappHitbox.X = 575;
                alternativKnappHitbox.Y = 445;
            }
            else
            {
                alternativKnappHitbox.Width = 150;
                alternativKnappHitbox.Height = 150;
                alternativKnappHitbox.X = 580;
                alternativKnappHitbox.Y = 450;

            }
            if (VänsterMusTryckt() && tillbakaHitbox.Contains(mus.Position))
            {
                ÄndraScen(0);

            }
            if (VänsterMusTryckt() && spelaKnappHitbox.Contains(mus.Position) && KanBörjaSpel())
            {
                ÄndraScen(1);

            }
            if (VänsterMusTryckt() && alternativKnappHitbox.Contains(mus.Position))
            {
                ÄndraScen(4);

            }
        }
        void ÄndraScen(int nyScen)
        {

            if (nyScen == 0)
            {
                scen = nyScen;
            }
            if (nyScen == 1)
            {
                scen = nyScen;
            }
            if (nyScen == 2)
            {
                scen = nyScen;
            }
            if (nyScen == 3)
            {
                scen = nyScen;
            }
            if (nyScen == 4)
            {
                scen = nyScen;
            }
        }
        void ÄrSkottBorta()
        {
            if (skottHitbox.X > 1400 || skottHitbox.X < 0)
            {
                skottHP = 0;
            }
            if (skottHitbox.Y > 700 || skottHitbox.Y < 0)
            {
                skottHP = 0;
            }
            if (skottHitbox2.X > 1400 || skottHitbox2.X < 0)
            {
                skott2HP = 0;
            }
            if (skottHitbox2.Y > 700 || skottHitbox2.Y < 0)
            {
                skott2HP = 0;
            }
            if (SkottBorta())
            {
                skottHitbox.X = -40;
            }
            if (SkottBorta2())
            {
                skottHitbox2.X = -40;
            }
        }
        void ÄrStridsvagnDöd()
        {
            if (stridsvagnHP <= 0)
            {
                ÄndraScen(3);
                stridsvagnHP = 4;
                stridsvagnVector.X = 359;
                stridsvagnVector.Y = 400;
                stridsvagnVector2.X = 800;
                stridsvagnVector2.Y = 400;
                rotation = 0;
                rotation2 = 0;
            }
            if (stridsvagn2HP <= 0)
            {
                ÄndraScen(2);
                stridsvagn2HP = 4;
                stridsvagnVector.X = 359;
                stridsvagnVector.Y = 400;
                stridsvagnVector2.X = 800;
                stridsvagnVector2.Y = 400;
                rotation = 0;
                rotation2 = 0;

            }
        }                
        bool SkottBorta()
        {
            if (skottHP <= 0)
            {
                kanSkadaStridsvagn = false;
                return true;
            }
            else return false;
        }
        bool SkottBorta2()
        {
            if (skott2HP <= 0)
            {
                kanSkadaStridsvagn2 = false;
                return true;
            }
            else return false;
        }
        bool KanRöraSigVänster()
        {

            if ((stridsvagnRiktningX < 0 && NuddarStridsvagnVänsterVägg()))
            {
                return false;
            }

            return true;
        }
        bool KanRöraSigHöger()
        {
            for (int i = 0; i < högerVäggLista.Count; i++)
            {
                if ((stridsvagnRiktningX > 0 && stridsvagnHitbox.Intersects(högerVäggLista[i])))
                {
                    return false;
                }
            }
            return true;
        }
        bool KanRöraSigHöger2()
        {
            for (int i = 0; i < högerVäggLista.Count; i++)
            {
                if ((stridsvagn2RiktningX > 0 && stridsvagn2Hitbox.Intersects(högerVäggLista[i])))
                {
                    return false;
                }
            }
            return true;
        }
        bool KanRöraSigVänster2()
        {

            if ((stridsvagn2RiktningX < 0 && NuddarStridsvagn2VänsterVägg()))
            {
                return false;
            }

            return true;
        }
        bool NuddarStridsvagnVänsterVägg()
        {
            for (int i = 0; i < vänsterVäggLista.Count; i++)
            {
                if (stridsvagnHitbox.Intersects(vänsterVäggLista[i]))
                {
                    return true;
                }
            }
            return false;
        }
        bool NuddarStridsvagn2VänsterVägg()
        {
            for (int i = 0; i < vänsterVäggLista.Count; i++)
            {
                if (stridsvagn2Hitbox.Intersects(vänsterVäggLista[i]))
                {
                    return true;
                }
            }
            return false;
        }
        bool NuddarStridsvagnHögerVägg()
        {
            for (int i = 0; i < högerVäggLista.Count; i++)
            {
                if (stridsvagnHitbox.Intersects(högerVäggLista[i]))
                {

                    return true;
                }
            }
            return false;
        }
        bool NuddarStridsvagn2HögerVägg()
        {
            for (int i = 0; i < högerVäggLista.Count; i++)
            {
                if (stridsvagn2Hitbox.Intersects(högerVäggLista[i]))
                {

                    return true;
                }
            }
            return false;
        }
        bool NuddarStridsvagnTak()
        {
            for (int i = 0; i < takLista.Count; i++)
            {
                if (stridsvagnHitbox.Intersects(takLista[i]))
                {
                    return true;
                }
            }
            return false;
        }
        bool NuddarStridsvagn2Tak()
        {
            for (int i = 0; i < takLista.Count; i++)
            {
                if (stridsvagn2Hitbox.Intersects(takLista[i]))
                {
                    return true;
                }
            }
            return false;
        }        
        bool NuddarStridsvagnGolv()
        {
            for (int i = 0; i < golvLista.Count; i++)
            {
                if (stridsvagnHitbox.Intersects(golvLista[i]))
                {
                    return true;
                }
            }
            return false;
        }
        bool NuddarStridsvagn2Golv()
        {
            for (int i = 0; i < golvLista.Count; i++)
            {
                if (stridsvagn2Hitbox.Intersects(golvLista[i]))
                {
                    return true;
                }
            }
            return false;
        }
        bool VänsterMusTryckt()
        {
            if (mus.LeftButton == ButtonState.Pressed && gammalMus.LeftButton == ButtonState.Released)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool KanBörjaSpel()
        {
            if (scen == 0 || scen == 3 || scen == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }
    }
}
