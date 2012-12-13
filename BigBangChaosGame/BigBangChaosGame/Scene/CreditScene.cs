using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace BigBangChaosGame
{
    class CreditScene : AbstractGameScene
    {
        private MenuButton back;
        private MouseEvent mouseEvent;

        private SceneManager sceneMgr;
        private SpriteFont font;
        private SpriteBatch spriteBatch;
        private ContentManager Content;
        private List<String> lines;
        private int scrolling = 1;

        private Song mainTheme;

        private Boolean isFinsih = false;

        public CreditScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            this.sceneMgr = sceneMgr;
            //On charge les lignes du fichier
            lines = System.IO.File.ReadAllLines(@"credits.txt").OfType<String>().ToList();
        }

        public override void Initialize()
        {

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");
            //Bouton retour pour quitter
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
            //Chargement de la police
            font = Content.Load<SpriteFont>("DFsmall");
            mainTheme = Content.Load<Song>("Sounds/main_theme_v1.0");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(mainTheme);
            base.LoadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool othersceneHasFocus, bool coveredByOtherscene)
        {
            //on quitte si le scroll est fini
            if (isFinsih)
            {
                MediaPlayer.Stop();
                this.Remove();
            }
            else
            {
                base.Update(gameTime, othersceneHasFocus, coveredByOtherscene);

                if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(back.getContainer()))
                {
                    this.Remove();
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            back.DrawButton(spriteBatch);
            int i = 0;
            isFinsih = true;
            //On affiche toute les lignes
            foreach (String line in lines)
            {
                if (SceneManager.GraphicsDevice.Viewport.Height + font.MeasureString(line).Y - scrolling + i * 50 >= 0 && SceneManager.GraphicsDevice.Viewport.Height - scrolling + i * 50 < 720)
                {
                    spriteBatch.DrawString(font, line, new Vector2((SceneManager.GraphicsDevice.Viewport.Width - font.MeasureString(line).X) / 2, SceneManager.GraphicsDevice.Viewport.Height - scrolling + i * 50), Color.White);
                    isFinsih = false;
                }
                i++;                
            }           

            scrolling ++;
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
