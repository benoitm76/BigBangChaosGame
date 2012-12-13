using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace BigBangChaosGame.Scene
{
    public class HighScoreMenuScene : AbstractMenuScene
    {
        MenuButton back;
        MouseEvent mouseEvent;

        private SpriteFont _font;
        private SpriteFont _font2;
        private BBCGame game;
        private int score;
        private ContentManager Content;
        SpriteBatch spriteBatch;
        private Texture2D background;
        TabScore tab = new TabScore();
        Textbox textbox;
        private String texthighscore;
        private Boolean showTextbox;

        private SoundEffect soundHightScore;
        
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

            base.Initialize();

            back = new MenuButton(new Vector2(0, 625), Content.Load<Texture2D>("Back"), new Rectangle(100, 100, 100, 100));

            mouseEvent = new MouseEvent();
          
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
                Position = new Vector2((game.size_window.X / 2) - 400 / 2, 110),
                HasFocus = true
            };

            BigBangChaosGame.TabScore.HighScoreData data = tab.LoadHighScores(tab.HighScoresFilename);
            score = (int)game.distance;
            int scoreIndex = -1;
            for (int i = data.Count - 1; i > -1; i--)
            {
                if (score >= data.Score[i])
                {
                    scoreIndex = i;
                }
            }
            if (scoreIndex > -1)
            {
                showTextbox = true;
            }
            texthighscore = tab.makeHighScoreString(data);
            soundHightScore = Content.Load<SoundEffect>("Sounds/highscore_v1.0");
        }

        public override void Update(GameTime gameTime)
        {

            textbox.Update(gameTime);
            if (Textbox.Pseudo != "Default" && Textbox.Pseudo != "")
            {
                int scorre = (int)game.distance;
                tab.SaveHighScore(scorre, Textbox.Pseudo);                
                texthighscore = tab.makeHighScoreString();
                Textbox.Pseudo = "Default";
                soundHightScore.Play();
            }

            if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(back.getContainer()))
            {
                this.Remove();
            }

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            if(showTextbox)
                textbox.PreDraw();

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);

            score = (int)game.distance;
            string text = string.Format("Score: {0} Km", score);
            Vector2 tailletext = _font.MeasureString(text);
            spriteBatch.DrawString(_font, text, new Vector2((game.size_window.X / 2) - (tailletext.X / 2), 50), Color.White);
            string afficherHS = texthighscore;
            string text2 = string.Format("{0}", afficherHS);
            Vector2 tailletext2 = _font2.MeasureString(text2);
            spriteBatch.DrawString(_font2, text2, new Vector2((game.size_window.X / 2) - (tailletext2.X / 2), 270), Color.White);

            back.DrawButton(spriteBatch);

            spriteBatch.End();

            if (showTextbox)
                textbox.Draw();

            base.Draw(gameTime);
        }



        protected override void OnCancel()
        {
        }
    }
}
