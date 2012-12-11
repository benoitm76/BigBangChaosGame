using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BigBangChaosGame
{
    class Ennemies : Sprite
    {
        public Ennemies(Vector2 size_window, Vector2 position)
            : base(size_window)
        {
            this.position = position;             
        }
        public void Update(GameTime gameTime, int displacementX)
        {
            Vector2 newPos = new Vector2(position.X - displacementX, position.Y);
            position = newPos;
        }
    }
}
