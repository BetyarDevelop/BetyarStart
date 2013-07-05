using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    struct Polygon
    {
        public Vector2 a;
        public Vector2 b;

        public Polygon(Vector2 _a, Vector2 _b)
        {
            a = _a;
            b = _b;
        }
    }

    class Level
    {
        List<Polygon> collisionMap = new List<Polygon>();
        int width;
        int height;

        public Level()
        {
            width = 1024;
            height = 768;
            collisionMap.Add(new Polygon(new Vector2(10, 10),new Vector2( 1014, 10)));
            collisionMap.Add(new Polygon(new Vector2(1014, 10),new Vector2( 1014, 758)));
            collisionMap.Add(new Polygon(new Vector2(1014, 758),new Vector2( 10, 758)));
            collisionMap.Add(new Polygon(new Vector2(10, 758),new Vector2( 10, 10)));
        }

        public void Draw(SpriteBatch _sb)
        {
            foreach(Polygon i in collisionMap)
            {
                C3.XNA.Primitives2D.DrawLine(_sb, i.a, i.b, Color.Black);
            }
        }


    }
}
