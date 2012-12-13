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
        private SpriteFont _font2;
        private BBCGame game;
        private int score;
        private ContentManager Content;
        SpriteBatch spriteBatch;
        private Texture2D background;
        TabScore tab = new TabScore();
        Textbox textbox;
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
           
            tab.Ini();
            int scorre = (int)game.distance;
            tab.SaveHighScore(scorre, "princeali");
            base.Initialize();           
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("fond");
            _font = Content.Load<SpriteFont>("DF");
            _font2 = Content.Load<SpriteFont>("DFsmall");
            textbox = new Textbox(
            GraphicsDevice,
            400,
            Content.Load<SpriteFont>("DFsmall"))
            {
                ForegroundColor = Color.Blue,
                BackgroundColor = Color.White,
                Position = new Vector2((game.size_window.X/2)-400/2, 110),
                HasFocus = true
            };
        }

        public override void Update(GameTime gameTime)
        {

            textbox.Update(gameTime);
        

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White );
           
            score = (int)game.distance;
            string text = string.Format("Score: {0} Km", score);
            Vector2 tailletext = _font.MeasureString(text);
            spriteBatch.DrawString(_font, text, new Vector2((game.size_window.X / 2) - (tailletext.X / 2), 50), Color.White);
           string afficherHS = tab.makeHighScoreString();
           string text2 = string.Format("{0}", afficherHS);
           Vector2 tailletext2 = _font2.MeasureString(text2);
           spriteBatch.DrawString(_font2, text2, new Vector2((game.size_window.X / 2) - (tailletext2.X / 2), 270), Color.White);

            spriteBatch.End();


            textbox.PreDraw();
            textbox.Draw();


            base.Draw(gameTime);
        }



        protected override void OnCancel()
        {


        }
    }
}
