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
        public List<Object[]> type_ennemies;
        public int move_type;
        public int direction_move = 1;

        public Ennemies(Vector2 size_window)
            : base(size_window)
        {
            coef_dep = 1;
            type_ennemies = new List<Object[]>();
            type_ennemies.Add(new Object[]{"ennemie1_v2.0", 0});
            type_ennemies.Add(new Object[]{"ennemie2_v1.0", 0});
            type_ennemies.Add(new Object[]{"ennemie3_v1.0", 1});
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, object[] type_ennemie)
        {
            move_type = (int)type_ennemie[1];
            base.LoadContent(content, (string)type_ennemie[0]);
        }

        public void Update(GameTime gameTime, int displacementX)
        {
            float new_vertical_pos = 0;
            new_vertical_pos = position.Y;
            if (move_type == 1)
            {
                new_vertical_pos += displacementX * direction_move;
                if (new_vertical_pos + texture.Height > size_window.Y - 70)
                {
                    new_vertical_pos = size_window.Y - texture.Height - 70 - (size_window.Y - texture.Height - new_vertical_pos - 70);
                    direction_move = direction_move * -1;
                }
                if (new_vertical_pos < 70)
                {
                    new_vertical_pos = 70 + (new_vertical_pos - 70);
                    direction_move = direction_move * -1;
                }            
            }
            Vector2 newPos = new Vector2(position.X - displacementX * coef_dep, new_vertical_pos);
            position = newPos;
        }
    }
}
