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
        private Vector2 size_window;
        TabScore tab = new TabScore();

        public ScoreScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            //new GameplayScene(sceneMgr).Add();
            size_window = new Vector2(SceneManager.GraphicsDevice.Viewport.Width, SceneManager.GraphicsDevice.Viewport.Height);
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
            font = Content.Load<SpriteFont>("DF");
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


            string afficherHS = tab.makeHighScoreString();
            string text = string.Format("{0}", afficherHS);
            Vector2 tailletext2 = font.MeasureString(text);
            spriteBatch.DrawString(font, text, new Vector2((size_window.X / 2) - (tailletext2.X / 2), (size_window.Y / 2) - (tailletext2.Y / 2)), Color.White);
    



            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
