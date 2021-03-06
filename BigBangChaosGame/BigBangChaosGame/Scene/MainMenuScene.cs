﻿using System;
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
    public class MainMenuScene : AbstractMenuScene
    {
        SpriteBatch spriteBatch;
        MenuButton button1;
        MenuButton button2;
        MenuButton button3;
        MenuButton button4;
        MenuButton button5;
        MenuButton button6;

        private SceneManager sceneMgr;
        private Texture2D logo_gamejam;

        MouseEvent mouseEvent;

        private ContentManager Content;
        private Texture2D background;

        public MainMenuScene(SceneManager sceneMgr)
            : base(sceneMgr, "")
        {
            //new GameplayScene(sceneMgr).Add();
            this.sceneMgr = sceneMgr;

            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            //TransitionOffTime = TimeSpan.FromSeconds(0.5);


        }

        protected override void OnCancel()
        {
        }

        public override void Initialize()
        {
            // TODO: Add your initialization logic here

            /*this.graphics.IsFullScreen = false;
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.ApplyChanges();*/

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");

            button1 = new MenuButton(new Vector2(10, 250), Content.Load<Texture2D>("Play"), new Rectangle(100, 100, 100, 100));
            button2 = new MenuButton(new Vector2(1000, 400), Content.Load<Texture2D>("Controles"), new Rectangle(100, 100, 100, 100));
            button3 = new MenuButton(new Vector2(500, 650), Content.Load<Texture2D>("Credits"), new Rectangle(100, 100, 100, 100));
            button4 = new MenuButton(new Vector2(1075, 650), Content.Load<Texture2D>("Exit"), new Rectangle(100, 100, 100, 100));
            button5 = new MenuButton(new Vector2(10, 400), Content.Load<Texture2D>("Instructions"), new Rectangle(100, 100, 100, 100));
            button6 = new MenuButton(new Vector2(980, 250), Content.Load<Texture2D>("High Scores"), new Rectangle(100, 100, 100, 100));

            mouseEvent = new MouseEvent();

            logo_gamejam = Content.Load<Texture2D>("logo_gamjam_v1.0");

            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");

            // fond pour le menu
            background = Content.Load<Texture2D>("Menu principal1");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        public override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                sceneMgr.Game.Exit();
            if (!(SceneState == SceneState.Hidden || SceneState == SceneState.TransitionOff))
            {
                sceneMgr.Game.IsMouseVisible = true;
                // TODO: Add your update logic here
                if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(button1.getContainer()))
                {
                    new ChoixScene(sceneMgr).Add();
                }

                if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(button2.getContainer()))
                {
                    new ControleScene(sceneMgr).Add();
                }

                if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(button3.getContainer()))
                {
                    new CreditScene(sceneMgr).Add();
                }

                if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(button4.getContainer()))
                {
                    sceneMgr.Game.Exit();
                }

                if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(button5.getContainer()))
                {
                    new InstructionScene(sceneMgr).Add();
                }

                if (mouseEvent.UpdateMouse() && mouseEvent.getMouseContainer().Intersects(button6.getContainer()))
                {
                    new ScoreScene(sceneMgr).Add();
                }                
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            if (TransitionPosition > 0 && SceneState == SceneState.TransitionOn)
            {
                SceneManager.FadeBackBufferToBlack(TransitionPosition);
            }

            if (!(SceneState == SceneState.Hidden || SceneState == SceneState.TransitionOff))
            {
                spriteBatch.Begin();
                spriteBatch.Draw(background, Vector2.Zero, Color.White);
                spriteBatch.Draw(logo_gamejam, new Vector2(20, SceneManager.GraphicsDevice.Viewport.Height - logo_gamejam.Height - 20), Color.Gray);
                button1.DrawButton(spriteBatch);
                button2.DrawButton(spriteBatch);
                button3.DrawButton(spriteBatch);
                button4.DrawButton(spriteBatch);
                button5.DrawButton(spriteBatch);
                button6.DrawButton(spriteBatch);
                spriteBatch.End();
            }
            
            base.Draw(gameTime);            
        }
    }
}
