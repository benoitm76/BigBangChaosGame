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
        private SpriteBatch spriteBatch;
        private ContentManager Content;
        private List<String> lines;
        private int scrolling;

        public CreditScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            this.sceneMgr = sceneMgr;

            lines = System.IO.File.ReadAllLines(@"credits.txt").OfType<String>().ToList();
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
            int i = 0;
            foreach (String line in lines)
            {
                if (SceneManager.GraphicsDevice.Viewport.Height + font.MeasureString(line).Y - scrolling + i * 50 >= 0 && SceneManager.GraphicsDevice.Viewport.Height - scrolling + i * 50 < 720)
                {
                    spriteBatch.DrawString(font, line, new Vector2((SceneManager.GraphicsDevice.Viewport.Width - font.MeasureString(line).X) / 2, SceneManager.GraphicsDevice.Viewport.Height - scrolling + i * 50), Color.White);
                }
                i++;
            }
            scrolling ++;
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
