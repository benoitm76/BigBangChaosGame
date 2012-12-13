using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace BigBangChaosGame
{
    class CreditScene : AbstractGameScene
    {
        private SceneManager sceneMgr;
        private SpriteFont font;
        private String text;
        private SpriteBatch spriteBatch;
        private ContentManager Content;

        public CreditScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            this.sceneMgr = sceneMgr;

            text = "Big Bang Chaos \r\n\r\n" +
                "Programmers :";
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
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
