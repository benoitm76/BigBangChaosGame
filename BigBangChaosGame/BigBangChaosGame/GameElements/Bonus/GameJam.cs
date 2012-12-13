using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BigBangChaosGame
{
    class GameJam : Bonus
    {
        public GameJam(Vector2 size_window, BBCGame bbcgame)
            : base(size_window, bbcgame)
        {
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content, "logo_gamjam_bonus_v1.0");
        }

        public override void applyBonus()
        {
            bbcgame.distance += 2000; 
        }
    }
}
