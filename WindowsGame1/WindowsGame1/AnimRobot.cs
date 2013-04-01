using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TextureAtlas
{
    public class AnimRobot
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        public int direction;
        int AnimationDelay = 0;
        private Vector2 RobotPos = new Vector2(200, 200);


        public AnimRobot(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = 10;
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
            int RobotSpd = 3;
            if (newKeyboardState.IsKeyDown(Keys.Up) || newKeyboardState.IsKeyDown(Keys.Down)
                || newKeyboardState.IsKeyDown(Keys.Left) || newKeyboardState.IsKeyDown(Keys.Right))
            {
                if (newKeyboardState.IsKeyDown(Keys.Left) && newKeyboardState.IsKeyDown(Keys.Up))
                {
                    RobotPos.X -= RobotSpd-1;
                    RobotPos.Y -= RobotSpd-1;
                    direction = 6;
                }
                else if (newKeyboardState.IsKeyDown(Keys.Up) && newKeyboardState.IsKeyDown(Keys.Right))
                {
                    RobotPos.X += RobotSpd-1;
                    RobotPos.Y -= RobotSpd;
                    direction = 4;
                }
                else if (newKeyboardState.IsKeyDown(Keys.Right) && newKeyboardState.IsKeyDown(Keys.Down))
                {
                    RobotPos.X += RobotSpd-1;
                    RobotPos.Y += RobotSpd-1;
                    direction = 2;
                }
                else if (newKeyboardState.IsKeyDown(Keys.Down) && newKeyboardState.IsKeyDown(Keys.Left))
                {
                    RobotPos.X -= RobotSpd-1;
                    RobotPos.Y += RobotSpd-1;
                    direction = 8;
                }
                else if (newKeyboardState.IsKeyDown(Keys.Down))
                {
                    RobotPos.Y += RobotSpd;
                    direction = 1;
                }
                else if (newKeyboardState.IsKeyDown(Keys.Right))
                {
                    RobotPos.X += RobotSpd;
                    direction = 3;
                }
                else if (newKeyboardState.IsKeyDown(Keys.Up))
                {
                    RobotPos.Y -= RobotSpd;
                    direction = 5;
                }
                else if (newKeyboardState.IsKeyDown(Keys.Left))
                {
                    RobotPos.X -= RobotSpd;
                    direction = 7;
                }
                Update();

            }
            else
            {
                Idle();
            }

            return RobotPos;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = direction - 1;
            int column = currentFrame;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

        }
    }
}