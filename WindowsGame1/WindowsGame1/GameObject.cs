using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class GameObject
    {
        private int width;
        private int height;

        private Vector2 pos;

        public Texture2D Texture { get; set; }
    }
}
