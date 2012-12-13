using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BigBangChaosGame
{
    /// <summary>
    /// Un fond d'écran
    /// </summary>
    public class BackgroundScene : AbstractGameScene
    {
        #region Fields

        private ContentManager _content;
        private Texture2D _backgroundTexture;
        private Texture2D logo_gamejam;
        private Vector2 size_window;

        private SceneManager sceneMgr;

        #endregion

        #region Initialization

        public BackgroundScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            this.sceneMgr = sceneMgr;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        protected override void LoadContent()
        {
            if (_content == null)
                _content = new ContentManager(SceneManager.Game.Services, "Content");

            _backgroundTexture = _content.Load<Texture2D>("fond");
            logo_gamejam = _content.Load<Texture2D>("logo_gamjam_v1.0");
        }

        protected override void UnloadContent()
        {
            _content.Unload();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime, bool othersceneHasFocus, bool coveredByOtherscene)
        {
            // Cette scène est destinée à être recouverte
            // coveredByOtherscene est donc forcée à false
            base.Update(gameTime, othersceneHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = SceneManager.SpriteBatch;
            Viewport viewport = SceneManager.GraphicsDevice.Viewport;
            var fullscene = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(_backgroundTexture, fullscene,
                             new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            spriteBatch.Draw(logo_gamejam, new Vector2(20, SceneManager.GraphicsDevice.Viewport.Height - logo_gamejam.Height - 20), Color.Gray);
            spriteBatch.End();
        }

        #endregion
    }
}
