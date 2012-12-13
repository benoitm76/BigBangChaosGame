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
        SpriteBatch spriteBatch;

        private SpriteFont spritfont;

        private ContentManager Content;
        private Texture2D background;
        private SceneManager sceneMgr;

        public InstructionScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            this.sceneMgr = sceneMgr;
        }

        public override void Initialize()
        {

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");

            medic = new MenuButton(new Vector2(50, 350), Content.Load<Texture2D>("medic_kitv1.0"), new Rectangle(100, 100, 100, 100));
            fiole = new MenuButton(new Vector2(50, 400), Content.Load<Texture2D>("fiole_v2.1"), new Rectangle(100, 100, 100, 100));
            enemmy1 = new MenuButton(new Vector2(1175, 320), Content.Load<Texture2D>("ennemie1_v2.0"), new Rectangle(100, 100, 100, 100));
            enemmy2 = new MenuButton(new Vector2(1200, 420), Content.Load<Texture2D>("ennemie2_v1.0"), new Rectangle(100, 100, 100, 100));
            enemmy3 = new MenuButton(new Vector2(1207, 580), Content.Load<Texture2D>("ennemie3_v1.0"), new Rectangle(100, 100, 100, 100));
            instructions = new MenuButton(new Vector2(250, 10), Content.Load<Texture2D>("Intructions_big"), new Rectangle(100, 100, 100, 100));
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

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(spritfont, "The goal is to go as far as possible while avoiding enemies", new Vector2(50, 255), Color.White);
            spriteBatch.DrawString(spritfont, "You can catch various items such as health or invulnerability to go further.", new Vector2(50, 280), Color.White);
            spriteBatch.DrawString(spritfont, "Health", new Vector2(200, 360), Color.White);
            spriteBatch.DrawString(spritfont, "Invulnerability", new Vector2(200, 410), Color.White);
            spriteBatch.DrawString(spritfont, "Enemies : ", new Vector2(1075, 450), Color.White);

            fiole.DrawButton(spriteBatch);
            medic.DrawButton(spriteBatch);
            instructions.DrawButton(spriteBatch);
            enemmy1.DrawButton(spriteBatch);
            enemmy2.DrawButton(spriteBatch);
            enemmy3.DrawButton(spriteBatch);

            sceneMgr.Game.IsMouseVisible = true;
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}