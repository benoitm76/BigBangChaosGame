using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BigBangChaosGame
{
    public class AbstractMenuScene : AbstractGameScene
    {
        private readonly string _menuTitle;

        protected AbstractMenuScene(SceneManager sceneMgr, string menuTitle)
            : base(sceneMgr)
        {
            _menuTitle = menuTitle;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        protected virtual void OnCancel()
        {
            Remove();
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            OnCancel();
        }

        /*private void UpdateMenuItemLocations()
        {
            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);
            var position = new Vector2(0f, 175f);

            foreach (MenuItem menuItem in _menuItems)
            {
                position.X = SceneManager.GraphicsDevice.Viewport.Width / 2 - menuItem.GetWidth(this) / 2;

                if (SceneState == SceneState.TransitionOn)
                    position.X -= transitionOffset * 256;
                else
                    position.X += transitionOffset * 512;

                menuItem.Position = position;
                position.Y += MenuItem.GetHeight(this);
            }
        }*/

        public override void Update(GameTime gameTime, bool othersceneHasFocus, bool coveredByOtherscene)
        {
            base.Update(gameTime, othersceneHasFocus, coveredByOtherscene);

            /*for (int i = 0; i < _menuItems.Count; i++)
            {
                bool isSelected = IsActive && (i == _selecteditem);
                _menuItems[i].Update(isSelected, gameTime);
            }*/
        }

        public override void Draw(GameTime gameTime)
        {
            //UpdateMenuItemLocations();

            GraphicsDevice graphics = SceneManager.GraphicsDevice;
            SpriteBatch spriteBatch = SceneManager.SpriteBatch;
            SpriteFont font = SceneManager.Font;

            spriteBatch.Begin();
            /*for (int i = 0; i < _menuItems.Count; i++)
            {
                MenuItem menuItem = _menuItems[i];
                bool isSelected = IsActive && (i == _selecteditem);
                menuItem.Draw(this, isSelected, gameTime);
            }*/
            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);
            var titlePosition = new Vector2(graphics.Viewport.Width / 2f, 80);
            Vector2 titleOrigin = font.MeasureString(_menuTitle) / 2;
            Color titleColor = new Color(192, 192, 192) * TransitionAlpha;
            titlePosition.Y -= transitionOffset * 100;
            spriteBatch.DrawString(font, _menuTitle, titlePosition, titleColor, 0,
                                   titleOrigin, 1, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
