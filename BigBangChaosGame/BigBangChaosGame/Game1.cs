using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BigBangChaosGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D background;
        private Texture2D particle;

        private KeyboardState _keyboardState;

        private int scrollX = 1;

        private Game g;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            g = new Game();            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Pipe2");
            particle = Content.Load<Texture2D>("boule_png");
            g.particle = new Particle(new Vector2(particle.Width, particle.Height), new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            g.particle.coefDep = 5f;
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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            _keyboardState = Keyboard.GetState();
            Vector2 displacement = new Vector2();
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                displacement.Y = -1;
            }
            else if (_keyboardState.IsKeyDown(Keys.Down))
            {
                displacement.Y = 1;
            }

            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                displacement.X = 1;
            }
            else if (_keyboardState.IsKeyDown(Keys.Left))
            {
                displacement.X = -1;
            }
            g.particle.Displacement(displacement);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);            
            scrollX = (int)(scrollX + 5 * g.vitesse);
            
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            spriteBatch.Draw(background, Vector2.Zero, new Rectangle(scrollX, 0, background.Width, background.Height), Color.White);
            spriteBatch.Draw(particle, g.particle.position, Color.White);
            spriteBatch.End();
            if (scrollX >= background.Width)
            {
                scrollX = 0;
                //g.vitesse = g.vitesse * 1.3f;
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
