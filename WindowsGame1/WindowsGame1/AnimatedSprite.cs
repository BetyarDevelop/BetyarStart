using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TextureAtlas
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        public int direction;
        int AnimationDelay = 0;
        private Vector2 NyuszinnoPos = new Vector2(400, 200);

        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            //totalFrames = Rows * Columns;
            totalFrames = 9;
            direction = 0;
        }

        public void Update()
        {
            if (AnimationDelay == 4)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
                AnimationDelay = 0;
            }
            AnimationDelay++;
        }

        public void Idle()
        {
            currentFrame = 0;
        }

        public Vector2 Move(KeyboardState oldKeyboardState, KeyboardState newKeyboardState)
        {
            if (newKeyboardState.IsKeyDown(Keys.Up) || newKeyboardState.IsKeyDown(Keys.Down)
                || newKeyboardState.IsKeyDown(Keys.Left) || newKeyboardState.IsKeyDown(Keys.Right))
            {
                if (newKeyboardState.IsKeyDown(Keys.Left))
                {
                    NyuszinnoPos.X -= 2;
                    direction = 4;
                }
                if (newKeyboardState.IsKeyDown(Keys.Right))
                {
                    NyuszinnoPos.X += 2;
                    direction = 2;
                }
                if (newKeyboardState.IsKeyDown(Keys.Up))
                {
                    NyuszinnoPos.Y -= 2;
                    direction = 1;
                }
                if (newKeyboardState.IsKeyDown(Keys.Down))
                {
                    NyuszinnoPos.Y += 2;
                    direction = 3;
                }
                Update();

            }
            else
            {
                Idle();
            }

            return NyuszinnoPos;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            //int row = (int)((float)currentFrame / (float)Columns);
            //int column = currentFrame % Columns;
            int row;
            switch (direction)
            {
                case 1:
                    row = 0;
                    break;
                case 2:
                    row = 2;
                    break;
                case 3:
                    row = 0;
                    break;
                case 4:
                    row = 1;
                    break;
                default:
                    row = 0;
                    break;
            }
            int column = currentFrame;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

        }
    }
}