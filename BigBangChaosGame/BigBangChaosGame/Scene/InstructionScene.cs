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
    class InstructionScene : AbstractGameScene
    {
        MenuButton fiole;
        MenuButton medic;
        MenuButton enemmy1;
        MenuButton enemmy2;
        MenuButton enemmy3;
        MenuButton instructions;
        MenuButton flife;
        MenuButton dlife;
        MenuButton gamjam;
        MenuButton back;
        MenuButton timeA;
        MenuButton timeR;
        SpriteBatch spriteBatch;

        MouseEvent mouseEvent;

        private SpriteFont spritfont;

        private ContentManager Content;
        private Texture2D background;
        private SceneManager sceneMgr;

        public InstructionScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            this.sceneMgr = sceneMgr;
        }

        public override void Initialize()
        {

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");

            instructions = new MenuButton(new Vector2(250, 10), Content.Load<Texture2D>("Intructions_big"), new Rectangle(100, 100, 100, 100));
            medic = new MenuButton(new Vector2(50, 350), Content.Load<Texture2D>("medic_kitv1.0"), new Rectangle(100, 100, 100, 100));
            fiole = new MenuButton(new Vector2(50, 400), Content.Load<Texture2D>("fiole_v2.1"), new Rectangle(100, 100, 100, 100));
            gamjam = new MenuButton(new Vector2(500, 480), Content.Load<Texture2D>("logo_gamjam_bonus_v1.0"), new Rectangle(100, 100, 100, 100));
            timeA = new MenuButton(new Vector2(50, 465), Content.Load<Texture2D>("compteurup_v1.0"), new Rectangle(100, 100, 100, 100));
            timeR = new MenuButton(new Vector2(50, 525), Content.Load<Texture2D>("compteurdown_v1.0"), new Rectangle(100, 100, 100, 100));
            enemmy1 = new MenuButton(new Vector2(1175, 320), Content.Load<Texture2D>("ennemie1_v2.0"), new Rectangle(100, 100, 100, 100));
            enemmy2 = new MenuButton(new Vector2(1200, 420), Content.Load<Texture2D>("ennemie2_v1.0"), new Rectangle(100, 100, 100, 100));
            enemmy3 = new MenuButton(new Vector2(1207, 580), Content.Load<Texture2D>("ennemie3_v1.0"), new Rectangle(100, 100, 100, 100));
            flife = new MenuButton(new Vector2(500, 350), Content.Load<Texture2D>("fullLife"), new Rectangle(100, 100, 100, 100));
            dlife = new MenuButton(new Vector2(500, 400), Content.Load<Texture2D>("DamageLife"), new Rectangle(100, 100, 100, 100));
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
            spritfont = Content.Load<SpriteFont>("DFPetit");

            // TODO: use this.Content to load your game content here
        }

        public override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                sceneMgr.Game.Exit();
            if (IsActive)
            {
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
            spriteBatch.DrawString(spritfont, "The goal is to go as far as possible while avoiding enemies and keep the particle stable", new Vector2(50, 255), Color.White);
            spriteBatch.DrawString(spritfont, "You can catch various items such as health or invulnerability to go further.", new Vector2(50, 280), Color.White);
            spriteBatch.DrawString(spritfont, "Health", new Vector2(130, 360), Color.White);
            spriteBatch.DrawString(spritfont, "Invulnerability", new Vector2(130, 410), Color.White);
            spriteBatch.DrawString(spritfont, "Acceleration", new Vector2(130, 475), Color.White);
            spriteBatch.DrawString(spritfont, "Deceleration", new Vector2(130, 535), Color.White);
            spriteBatch.DrawString(spritfont, "Enemies :", new Vector2(1050, 440), Color.White);
            spriteBatch.DrawString(spritfont, "Complety stable", new Vector2(730, 350), Color.White);
            spriteBatch.DrawString(spritfont, "Percentage of instability", new Vector2(730, 400), Color.White);
            spriteBatch.DrawString(spritfont, "Increase your score of 2000 km", new Vector2(570, 500), Color.White);

            fiole.DrawButton(spriteBatch);
            medic.DrawButton(spriteBatch);
            instructions.DrawButton(spriteBatch);
            enemmy1.DrawButton(spriteBatch);
            enemmy2.DrawButton(spriteBatch);
            enemmy3.DrawButton(spriteBatch);
            flife.DrawButton(spriteBatch);
            dlife.DrawButton(spriteBatch);
            back.DrawButton(spriteBatch);
            timeA.DrawButton(spriteBatch);
            timeR.DrawButton(spriteBatch);
            gamjam.DrawButton(spriteBatch);

            sceneMgr.Game.IsMouseVisible = true;
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