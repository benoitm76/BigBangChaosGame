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
        public List<String> type_ennemies;

        public Ennemies(Vector2 size_window)
            : base(size_window)
        {
            coef_dep = 1;
            type_ennemies = new List<string>();
            type_ennemies.Add("ennemie1_v2.0");
            type_ennemies.Add("ennemie2_v1.0");
            type_ennemies.Add("ennemie3_v1.0");
        }
        public void Update(GameTime gameTime, int displacementX)
        {
            Vector2 newPos = new Vector2(position.X - displacementX * coef_dep, position.Y);
            position = newPos;
        }
    }
}
