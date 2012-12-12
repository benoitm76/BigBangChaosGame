using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BigBangChaosGame
{
    public class Ennemies : Sprite
    {
        public float coef_dep{get; set;}

        public Ennemies(Vector2 size_window)
            : base(size_window)
        {
            coef_dep = 1;           
        }
        public void Update(GameTime gameTime, int displacementX)
        {
            Vector2 newPos = new Vector2(position.X - displacementX * coef_dep, position.Y);
            position = newPos;
        }
    }
}
