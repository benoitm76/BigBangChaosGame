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
    class ChoixScene : AbstractGameScene
    {
        MenuButton clavier;
        MenuButton xbox;
        MenuButton back;

        SpriteBatch spriteBatch;

        MouseEvent mouseEvent;

        private ContentManager Content;
        private Texture2D background;
        private SceneManager sceneMgr;

        public ChoixScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            this.sceneMgr = sceneMgr;
        }

        public override void Initialize()
        {

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");

            Texture2D keyboard = Content.Load<Texture2D>("Keyboard");
            Texture2D gamepad = Content.Load<Texture2D>("Gamepad");

            clavier = new MenuButton(new Vector2((SceneManager.GraphicsDevice.Viewport.Width - keyboard.Width) / 2, 250), keyboard, new Rectangle(100, 100, 100, 100));
            xbox = new MenuButton(new Vector2((SceneManager.GraphicsDevice.Viewport.Width - gamepad.Width) / 2, 370), gamepad, new Rectangle(100, 100, 100, 100));
            back = new MenuButton(new Vector2(0, 625), Content.Load<Texture2D>("Back"), new Rectangle(100, 100, 100, 100));

            mouseEvent = new MouseEvent();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");

            // fond pour le menu
            background = Content.Load<Texture2D>("Fond");


            // TODO: use this.Content to load your game content here
        }

        public override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                sceneMgr.Game.Exit();
            if (IsActive)
            {

                if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(clavier.getContainer()))
                {
                    sceneMgr.Game.IsMouseVisible = false;
                    BBCGame.controller = BBCGame.Keyboard;
                    new GameplayScene(sceneMgr).Add();
                    this.Remove();
                }

                if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(xbox.getContainer()))
                {
                    if (GamePad.GetState(PlayerIndex.One).IsConnected)
                    {
                        sceneMgr.Game.IsMouseVisible = false;
                        BBCGame.controller = BBCGame.XboxController;
                        new GameplayScene(sceneMgr).Add();
                        this.Remove();
                    }
                }

                // TODO: Add your update logic here
                if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(back.getContainer()))
                {
                    this.Remove();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            clavier.DrawButton(spriteBatch);
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                xbox.DrawButton(spriteBatch);
            }
            else
            {
                xbox.DrawButton(spriteBatch, Color.Gray);
            }
            back.DrawButton(spriteBatch);
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
