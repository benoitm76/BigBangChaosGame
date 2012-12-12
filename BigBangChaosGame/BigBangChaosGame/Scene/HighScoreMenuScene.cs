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
                {
                    Content = new ContentManager(SceneManager.Game.Services, "Content");
                }

            
            
        }


        public override void Initialize()
        {               
            // TODO: Add your initialization logic here
            TabScore tab = new TabScore();
           // tab.Ini();
            base.Initialize();           
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("fond");
            _font = Content.Load<SpriteFont>("DF");
            
        }

        public override void Update(GameTime gameTime)
        {



            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White );
           
            score = (int)game.distance;
            string text = string.Format("Score: {0} Km", score);
            Vector2 tailletext = _font.MeasureString(text);
            spriteBatch.DrawString(_font, text, new Vector2((game.size_window.X / 2) - (tailletext.X / 2), 250), Color.White);



            spriteBatch.End();

            base.Draw(gameTime);
        }



        protected override void OnCancel()
        {


        }
    }
}
