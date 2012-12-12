using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace BigBangChaosGame
{
    //Classe générique permettant l'affichage d'objet
    public class Sprite
    {
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public Vector2 size_window { get; set; }

        public Color[] color { get; set; }

        //public Vector2 size_particle { get; set; }

        //Le constructeur prenant en paramètre la taille de la fenêtre
        public Sprite(Vector2 size_window)
        {
            this.size_window = size_window;
        }

        //Fonction initialize
        public virtual void Initialize()
        {
        }

        //Fonction permettant de charger les textures
        public virtual void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
            color = new Color[texture.Width * texture.Height];
            texture.GetData(color);
        }

        //Fonction Update de l'objet
        public virtual void Update(GameTime gameTime)
        {
        }

        //Fonction lors de saisie clavier
        public virtual void HandleInput(KeyboardState keyboardState, MouseState mouseState)
        {
        }

        //Fonction permettant de dessiner l'objet
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public Rectangle getRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
    }
}
