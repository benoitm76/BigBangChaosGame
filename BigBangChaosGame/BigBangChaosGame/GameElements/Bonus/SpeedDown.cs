using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BigBangChaosGame
{
    class SpeedDown : Bonus
    {  
        public SpeedDown(Vector2 size_window, BBCGame bbcgame)
            : base(size_window, bbcgame)
        {
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content, "compteurdown_v1.0");
        }

        public override void applyBonus()
        {
            if (bbcgame.timeSpeedDown == 0)
            {
                bbcgame.vitesse -= 0.5f;
                bbcgame.timeSpeedDown = 60 * 5 + 1;
            }
        }        
    }
}
