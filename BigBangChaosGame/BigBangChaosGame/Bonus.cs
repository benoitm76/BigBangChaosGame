using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BigBangChaosGame
{
    public abstract class Bonus : Sprite
    {
        public Bonus(Vector2 size_window) : base(size_window)
        {
        }

        public abstract void applyBonus(BBCGame bbcgame);

        public void Update(GameTime gameTime, int displacementX)
        {
            Vector2 newPos = new Vector2(position.X - displacementX, position.Y);
            position = newPos;
        }
    }
}
