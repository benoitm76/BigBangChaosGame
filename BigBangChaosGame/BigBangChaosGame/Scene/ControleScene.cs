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
    class ControleScene : AbstractGameScene
    {
        MenuButton left;
        MenuButton right;
        MenuButton up;
        MenuButton down;
        MenuButton controles;
        SpriteBatch spriteBatch;

        private ContentManager Content;
        private Texture2D background;
        private SceneManager sceneMgr;

        public ControleScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            this.sceneMgr = sceneMgr;
        }

        public override void Initialize()
        {

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");

            left = new MenuButton(new Vector2(100, 200), Content.Load<Texture2D>("Left"), new Rectangle(100, 100, 100, 100));
            right = new MenuButton(new Vector2(100, 300), Content.Load<Texture2D>("Right"), new Rectangle(100, 100, 100, 100));
            up = new MenuButton(new Vector2(100, 400), Content.Load<Texture2D>("Up"), new Rectangle(100, 100, 100, 100));
            down = new MenuButton(new Vector2(100, 500), Content.Load<Texture2D>("Down"), new Rectangle(100, 100, 100, 100));
            controles = new MenuButton(new Vector2(250, 10), Content.Load<Texture2D>("ControlesB"), new Rectangle(100, 100, 100, 100));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");

            // fond pour le menu
            background = Content.Load<Texture2D>("Fond");


            // TODO: use this.Content to load your game content here
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            left.DrawButton(spriteBatch);
            right.DrawButton(spriteBatch);
            up.DrawButton(spriteBatch);
            down.DrawButton(spriteBatch);
            controles.DrawButton(spriteBatch);

            sceneMgr.Game.IsMouseVisible = true;
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
