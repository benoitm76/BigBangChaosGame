using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace BigBangChaosGame
{
    class GameOverScene : AbstractGameScene
    {
        #region Fields

        private ContentManager _content;
        private Video video;
        private VideoPlayer player;
        private Texture2D videoTexture;

        #endregion

        #region Initialization

        public GameOverScene(SceneManager sceneMgr, Video video)
            : base(sceneMgr)
        {
            TransitionOnTime = TimeSpan.FromSeconds(0);
            TransitionOffTime = TimeSpan.FromSeconds(0);
            this.video = video;
        }

        protected override void LoadContent()
        {
            if (_content == null)
                _content = new ContentManager(SceneManager.Game.Services, "Content");

            video = _content.Load<Video>("game_over_v3.0");
            player = new VideoPlayer();
            player.IsLooped = false;     
            player.Play(video);     
            
        }

        protected override void UnloadContent()
        {
            _content.Unload();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime, bool othersceneHasFocus, bool coveredByOtherscene)
        {  
            if (player.State == MediaState.Stopped)
            {
                player.Stop();
                player.Dispose();
                Remove();
            }

            base.Update(gameTime, othersceneHasFocus, coveredByOtherscene);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = SceneManager.SpriteBatch;

            GraphicsDevice.Clear(Color.Black);

            // Only call GetTexture if a video is playing or paused
            if (player.State != MediaState.Stopped)
                videoTexture = player.GetTexture();

            // Drawing to the rectangle will stretch the 
            // video to fill the screen
            Rectangle screen = new Rectangle(GraphicsDevice.Viewport.X,
                GraphicsDevice.Viewport.Y,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);

            // Draw the video, if we have a texture to draw.
            if (videoTexture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(videoTexture, screen, Color.White);
                spriteBatch.End();
            }
            
        }

        #endregion
    }
}
