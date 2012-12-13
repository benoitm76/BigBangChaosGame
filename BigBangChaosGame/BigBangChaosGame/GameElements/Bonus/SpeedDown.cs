using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BigBangChaosGame
{
    class SpeedDown : Bonus
    {
        private int timeSpeedDown;        

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
            if (bbcgame.vitesse > 0.7f && timeSpeedDown == 0)
            {
                bbcgame.vitesse -= 0.3f;
                timeSpeedDown = 60 * 5 + 1;
            }
        }
        public override void Update(GameTime gameTime, int displacementX)
        {
            base.Update(gameTime, displacementX);

            if (timeSpeedDown == 1)
            {
                bbcgame.vitesse += 0.3f;
            }
        }
        
    }
}
