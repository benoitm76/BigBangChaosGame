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
    class ScoreScene: AbstractGameScene
    {
        MenuButton back;

        MouseEvent mouseEvent;

        private SpriteFont font;
        private SpriteBatch spriteBatch;
        private ContentManager Content;
        private SceneManager sceneMgr;

        public ScoreScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            //new GameplayScene(sceneMgr).Add();
            this.sceneMgr = sceneMgr;
        }

        public override void Initialize()
        {

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");

            back = new MenuButton(new Vector2(0, 625), Content.Load<Texture2D>("Back"), new Rectangle(100, 100, 100, 100));

            mouseEvent = new MouseEvent();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            if (Content == null)
            {
                Content = new ContentManager(SceneManager.Game.Services, "Content");
            }
            font = Content.Load<SpriteFont>("DFsmall");
            base.LoadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool othersceneHasFocus, bool coveredByOtherscene)
        {
            base.Update(gameTime, othersceneHasFocus, coveredByOtherscene);

            if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(back.getContainer()))
            {
                this.Remove();
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            
            back.DrawButton(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
