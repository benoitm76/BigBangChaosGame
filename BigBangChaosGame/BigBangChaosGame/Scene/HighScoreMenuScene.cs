using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BigBangChaosGame.Scene
{
    public class HighScoreMenuScene : AbstractMenuScene
    {

        private SpriteFont _font;
        private BBCGame game;
        private int score;
        private ContentManager Content;
        SpriteBatch spriteBatch;
        private Texture2D background;

        public HighScoreMenuScene(SceneManager sceneMgr, BBCGame game)
            : base(sceneMgr, "")
        {
            this.game = game;
            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");
        }


        public override void Initialize()
        {               
            // TODO: Add your initialization logic here
            base.Initialize();           
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("pipe_v2.0");
            _font = Content.Load<SpriteFont>("menufont");
            
        }

        public override void Update(GameTime gameTime)
        {



            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            score = (int)game.distance;
            string text = string.Format("score: {0} Km", score);
            spriteBatch.DrawString(_font, text, new Vector2(640 , 400), Color.Red);



            spriteBatch.End();

            base.Draw(gameTime);
        }



        protected override void OnCancel()
        {


        }
    }
}
