using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace BigBangChaosGame
{
    class MenuButton
    {
        Vector2 position;
        Texture2D texture;
        Rectangle container;


        public MenuButton(Vector2 position, Texture2D texture, Rectangle container)
        {
            this.position = position;
            this.texture = texture;
            this.container = container;
        }

        public Rectangle getContainer()
        {
            container = new Rectangle((int)position.X,
                (int)position.Y,
                ((int)texture.Width),
                ((int)texture.Height));
            return container;
        }

        public void DrawButton(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void DrawButton(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, position, color);
        }



    }
}
