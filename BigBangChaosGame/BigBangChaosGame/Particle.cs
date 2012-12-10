using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BigBangChaosGame
{
    class Particle : Sprite
    {
        public float coefDep { get; set; }

        public Particle(Vector2 size_window) : base (size_window)
        {
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
            //Permet de déplacer la particule
            Vector2 newPos = new Vector2(position.X, position.Y);
            Vector2 displacement = new Vector2();
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
    }
}
