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
        private Texture2D title;
        TabScore tab = new TabScore();

        public ScoreScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            size_window = new Vector2(SceneManager.GraphicsDevice.Viewport.Width, SceneManager.GraphicsDevice.Viewport.Height);
            this.sceneMgr = sceneMgr;
        }

        public override void Initialize()
        {

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");

            back = new MenuButton(new Vector2(0, 625), Content.Load<Texture2D>("Back"), new Rectangle(100, 100, 100, 100));
            title = Content.Load<Texture2D>("High Scores_big");

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

            string afficherHS = tab.makeHighScoreString2();
            string text = string.Format("{0}", afficherHS);
            Vector2 tailletext2 = font.MeasureString(text);
            spriteBatch.Draw(title, new Vector2((size_window.X / 2) - (title.Width / 2), 10), Color.White);
            spriteBatch.DrawString(font, text, new Vector2((size_window.X / 2) - (tailletext2.X / 2), 300), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);

            //Permet le fondu au chargement
            if (TransitionPosition > 0 && SceneState == SceneState.TransitionOn)
            {
                SceneManager.FadeBackBufferToBlack(TransitionPosition);
            }
        }

    }
}
