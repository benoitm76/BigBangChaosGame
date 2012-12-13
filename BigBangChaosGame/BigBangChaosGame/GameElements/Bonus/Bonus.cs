using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BigBangChaosGame
{
    public abstract class Bonus : Sprite
    {
        protected BBCGame bbcgame;

        public Bonus(Vector2 size_window, BBCGame bbcgame) : base(size_window)
        {
            this.bbcgame = bbcgame;
        }

        public abstract void applyBonus();

        public abstract void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content);

        public virtual void Update(GameTime gameTime, int displacementX)
        {
            Vector2 newPos = new Vector2(position.X - displacementX, position.Y);
            position = newPos;
        }
    }
}
