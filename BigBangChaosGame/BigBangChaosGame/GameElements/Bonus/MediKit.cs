using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BigBangChaosGame
{
    class MediKit : Bonus
    {
        public MediKit(Vector2 size_window)
            : base(size_window)
        {
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content, "medic_kitv1.0");
        }

        public override void applyBonus(BBCGame bbcgame)
        {
            if(bbcgame.particle.health < 5)
                bbcgame.particle.health += 1;
        }
    }
}
