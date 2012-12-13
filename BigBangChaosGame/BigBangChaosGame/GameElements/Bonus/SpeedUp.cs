using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BigBangChaosGame
{
    class SpeedUp : Bonus
    {
        public SpeedUp(Vector2 size_window)
            : base(size_window)
        {
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content, "compteurup_v1.0");
        }

        public override void applyBonus(BBCGame bbcgame)
        {
            if (bbcgame.maxSpeed > bbcgame.vitesse)
            {
                bbcgame.vitesse += 0.3f;
            }
        }
    }
}
