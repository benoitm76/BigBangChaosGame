using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BigBangChaosGame
{
    class Particle : Sprite
    {
        public float coefDep { get; set; }
        public int nb_frame_invulnerability { get; set; }
        private int health;

        public Particle(Vector2 size_window) : base (size_window)
        {
            health = 5;
            coefDep = 10f;            
        }
        
        public override void LoadContent(ContentManager content, string assetName) 
        {
            base.LoadContent(content, assetName);
            //On défini la position de la particule
            position = new Vector2(50, (size_window.Y - texture.Height) / 2);
        }

        public override void HandleInput(KeyboardState keyboardState, MouseState mouseState)
        {
            Vector2 displacement = new Vector2();
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            GamePadCapabilities gamepadCaps = GamePad.GetCapabilities(PlayerIndex.One);
            Vector2 xbox360 = gamepadState.ThumbSticks.Left;

            displacement.X = xbox360.X;
            displacement.Y = -xbox360.Y;

            //Permet de déplacer la particule
            Vector2 newPos = new Vector2(position.X, position.Y);            
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                displacement.Y = -1;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                displacement.Y = 1;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                displacement.X = 1;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                displacement.X = -1;
            }
            newPos.X = newPos.X + displacement.X * coefDep;
            newPos.Y = newPos.Y + displacement.Y * coefDep;
            position = newPos;
        }

        public override void Update(GameTime gameTime)
        {
            //On vérifie que la particule ne sorte pas de l'écran
            Vector2 newPos = new Vector2(position.X, position.Y);
            if (newPos.X + texture.Width > size_window.X)
            {
                newPos.X = size_window.X - texture.Width;
            }
            if (newPos.X < 0)
            {
                newPos.X = 0;
            }
            if (newPos.Y + texture.Height > size_window.Y)
            {
                newPos.Y = size_window.Y - texture.Height;
            }
            if (newPos.Y < 0)
            {
                newPos.Y = 0;
            }            
            position = newPos;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Color color = Color.White;
            if (nb_frame_invulnerability != 0)
            {
                if ((int)(nb_frame_invulnerability / 20) % 2 == 1)
                {
                    color = Color.Transparent;
                }
                nb_frame_invulnerability--;
            }
            spriteBatch.Draw(texture, position, color);
        }

        public void touched()
        {
            health--;
            nb_frame_invulnerability = 60;
        }
    }
}
