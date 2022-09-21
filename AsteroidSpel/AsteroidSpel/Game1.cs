using AsteroidSpel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using static System.Formats.Asn1.AsnWriter;


namespace AsteroidSpel
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Texture2D backgroundTex;
        public Texture2D asteroidTex;
        public Vector2 asteroidPos;
        public Vector2 asteroidSpeed;

        Random rnd;
        public int widthX;
        public int heightY;
        public int height;
        public int width;

        List<Asteroid> asteroidList;
        Asteroid asteroid;
       
        MouseState mouseState, oldMouseState;
        bool isShootBool;
     
        public SpriteFont fontText;
        public int intScore;
        public int hp = 5;
    
        public Texture2D spaceCraftTex;
        public Vector2 alienPos;
        public float sizeAlien;
        Alien[] alienArrayList = new Alien[5];     

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
        }

        protected override void Initialize() // TODO: Add your initialization logic here below
        {
            intScore = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice); // TODO: use this.Content to load your game content here below
            backgroundTex = Content.Load<Texture2D>("BackgroundSpace");
            asteroidTex = Content.Load<Texture2D>("asteroid2D");
            spaceCraftTex = Content.Load<Texture2D>("spaceCraft_trans");

            mouseState = Mouse.GetState();
            fontText = Content.Load<SpriteFont>("File");

            rnd = new Random();
            asteroid = new Asteroid(asteroidTex, asteroidPos, asteroidSpeed, width);
            asteroidList = new List<Asteroid>();

            width = Window.ClientBounds.Width;
            height = Window.ClientBounds.Height;                    
            widthX = width - asteroidTex.Width - 300;
            heightY = height - asteroidTex.Height - 100;

            for (int i = 0; i < 4; i++)
            {               
                int rngPosX = rnd.Next(0, widthX);
                int rngPosY = rnd.Next(0, heightY);
                asteroidPos = new Vector2(rngPosX, rngPosY);

                int rngSpeedX = rnd.Next(-2, 2);
                int rngSpeedY = rnd.Next(-2, 2);
                asteroidSpeed = new Vector2(rngSpeedX, rngSpeedY);

                asteroid = new Asteroid(asteroidTex, asteroidPos, asteroidSpeed, width);
                asteroidList.Add(asteroid);
            }
                       
            for (int i = 0; i < 5; i++)
            {            
                int rngAlienX = rnd.Next(0, widthX);
                int rngAlienY = rnd.Next(0, heightY);                
                alienPos = new Vector2(rngAlienX, rngAlienY);
                sizeAlien = rnd.Next(1, 3 / 1);
                Alien alienships = new(spaceCraftTex, alienPos, sizeAlien); 
                alienArrayList[i] = alienships;              
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit(); // TODO: Add your update logic here below

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            asteroid.spawnTime -= gameTime.ElapsedGameTime.Milliseconds;

            if (asteroid.spawnTime < 0 && hp > 0 && intScore < 25)
            {
                int rngPosX = rnd.Next(0, widthX);
                int rngPosY = rnd.Next(0, heightY);
                asteroidPos = new Vector2(rngPosX, rngPosY);

                int rngSpeedX = rnd.Next(-2, 2);
                int rngSpeedY = rnd.Next(-2, 2);
                asteroidSpeed = new Vector2(rngSpeedX, rngSpeedY);

                Asteroid asteroidRespawn = new Asteroid(asteroidTex, asteroidPos, asteroidSpeed, width);
                asteroidList.Add(asteroidRespawn);
                asteroid.resetSpawnTime();
            }

            if (hp > 0 && intScore < 25)
            {                                
                foreach (Asteroid asteroid in asteroidList)
                {
                    asteroid.Update();
                  
                    if(asteroid.posAstClass.Y < 0 - asteroidTex.Height || asteroid.posAstClass.X < 0 - asteroidTex.Height || 
                       asteroid.posAstClass.X > width || asteroid.posAstClass.Y > height && asteroid.aliveAstClass == true) 
                    {
                        asteroid.aliveAstClass = false;
                        hp--;
                    }
                    
                        else if (asteroid.hitboxAstClass.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed)
                        {
                            isShootBool = asteroid.isShootBool(mouseState.X, mouseState.Y);

                            if (isShootBool)
                            {
                                intScore += 1;
                            }
                        }

                        if (asteroid.aliveAstClass == false)
                        {
                            asteroidList.Remove(asteroid);
                   
                            break;
                        }                          
                }                   
            }
            
        base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); // TODO: Add your drawing code here below

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTex, Vector2.Zero, Color.White);
            spriteBatch.DrawString(fontText, "Asteroid Game", new Vector2(13, 10), Color.GhostWhite);
            spriteBatch.DrawString(fontText, "Score:" + intScore, new Vector2(140, 40), Color.GhostWhite);
            spriteBatch.DrawString(fontText, "Health:" + hp, new Vector2(15, 40), Color.GhostWhite);
           
            foreach (Alien alienships in alienArrayList)
            {
                alienships.Draw(spriteBatch);
            }                 

            foreach (Asteroid asteroid in asteroidList)
            {
                asteroid.Draw(spriteBatch);
            }
                
            if (hp == 0)
            {            
                spriteBatch.DrawString(fontText, "You have lost!", new Vector2(width / 2 - 100, height / 2), Color.Red);
            }

                else if (intScore == 25)
                {                
                    spriteBatch.DrawString(fontText, "You have Won!", new Vector2(width / 2 - 100, height / 2), Color.GhostWhite);           
                }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}