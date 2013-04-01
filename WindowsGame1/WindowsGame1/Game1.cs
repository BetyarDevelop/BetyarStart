using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TextureAtlas;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int Character;

        private KeyboardState oldKeyboardState;
        private KeyboardState newKeyboardState;
        private MouseState oldMouseState;
        private MouseState newMouseState;

        private Texture2D background;

        private Texture2D arrow;
        private float angle = 0;

        private SpriteFont font;
        private int score = 0;

        private AnimatedSprite NyuszinnoSprite;
        private Vector2 NyuszinnoPos = new Vector2(400, 200);
        private AnimBomberman BombermanSprite;
        private Vector2 BombermanPos = new Vector2(-500, 200);
        private AnimRobot RobotSprite;
        private Vector2 RobotPos = new Vector2(200, 200);
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here

            background = Content.Load<Texture2D>("stars");

            arrow = Content.Load<Texture2D>("arrow");

            font = Content.Load<SpriteFont>("Score");

            Texture2D nyuszinnoTexture = Content.Load<Texture2D>("$nyuszinno_9framev0.2Crop");
            NyuszinnoSprite = new AnimatedSprite(nyuszinnoTexture, 3, 9);
            Texture2D bombmanTexture = Content.Load<Texture2D>("playersCrop");
            BombermanSprite = new AnimBomberman(bombmanTexture, 4, 24);
            Texture2D RobotTexture = Content.Load<Texture2D>("walk_iso_trans");
            RobotSprite = new AnimRobot(RobotTexture, 8, 10);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

             //TODO: Add your update logic here

            // --------------------- BILLENTYŰZET INPUT KEZELÉS ----------------------------
            newKeyboardState = Keyboard.GetState();
            // handle the input
            if(newKeyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (oldKeyboardState.IsKeyUp(Keys.Space) && newKeyboardState.IsKeyDown(Keys.Space))
            {
                Character++;
                if (Character == 2)
                {
                    Character = 0;
                }
            }

            if (oldKeyboardState.IsKeyUp(Keys.Left) && newKeyboardState.IsKeyDown(Keys.Left))
            {
                // do something here
                // this will only be called when the key if first pressed
            }

            switch (Character)
            {
                case 0:
                    NyuszinnoPos = NyuszinnoSprite.Move(oldKeyboardState, newKeyboardState);
                    break;
                case 1:
                    RobotPos = RobotSprite.Move(oldKeyboardState, newKeyboardState);
                    break;
                default:
                    Character = 0;
                    break;
            }



         

            BombermanPos.X += 3;
            if (BombermanPos.X > 1000) BombermanPos.X = -500; 
            BombermanSprite.Update();
            
            oldKeyboardState = newKeyboardState;  // set the new state as the old state for next time

            // -------------------------- EGÉR INPUT KEZELÉS -------------------------------------
            MouseState mouseState = Mouse.GetState();

            newMouseState = Mouse.GetState();

            if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                // do something here
            }
            oldMouseState = newMouseState; // this reassigns the old state so that it is ready for next time

            // OTHER STUFF
            score++;

            angle += 0.01f;
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);

            Vector2 location = new Vector2(400, 240);
            Rectangle sourceRectangle = new Rectangle(0, 0, arrow.Width, arrow.Height);
            Vector2 origin = new Vector2(arrow.Width / 2, arrow.Height);
            spriteBatch.Draw(arrow, location, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);

            spriteBatch.DrawString(font, "Score: " + score, new Vector2(100, 100), Color.White);

            NyuszinnoSprite.Draw(spriteBatch, NyuszinnoPos);
            BombermanSprite.Draw(spriteBatch, BombermanPos);
            RobotSprite.Draw(spriteBatch, RobotPos);

            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}