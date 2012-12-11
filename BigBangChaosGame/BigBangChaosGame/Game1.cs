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

        private KeyboardState keyboardState;
        private MouseState mouseState;

        private Vector2 size_window;

        private Texture2D background;

        private int scrollX = 1;

        private Game g;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            g = new Game();
            size_window = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
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
            g.particle = new Particle(new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            g.particle.LoadContent(Content, "particle_v2.1");
            g.ennemies.Add(new Ennemies(size_window, new Vector2(size_window.X - 500, size_window.Y / 2)));
            g.ennemies[0].coef_dep = 1.2f;
            foreach (Sprite ennemie in g.ennemies)
            {
                ennemie.LoadContent(Content, "1P");
            }
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
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            g.particle.HandleInput(keyboardState, mouseState);
            g.particle.Update(gameTime);
            int displacementX = (int)(5 * g.vitesse);
            List<Ennemies> destroy_ennemies = new List<Ennemies>();
            foreach(Ennemies ennemie in g.ennemies)
            {                
                ennemie.Update(gameTime, displacementX);
                if (g.particle.nb_frame_invulnerability == 0)
                {
                    if (Game1.BoundingCircle(GetCenter((int)g.particle.position.X, (int)g.particle.texture.Width), GetCenter((int)g.particle.position.Y, (int)g.particle.texture.Height), (int)(g.particle.texture.Width / 2), GetCenter((int)ennemie.position.X, (int)ennemie.texture.Width), GetCenter((int)ennemie.position.Y, (int)ennemie.texture.Height), (int)(ennemie.texture.Width / 2)))
                    {
                        //g.ennemies.Remove(ennemie);
                        g.particle.touched();
                    }
                    if (ennemie.position.X < 0)
                    {
                        destroy_ennemies.Add(ennemie);
                    }
                }
            }
            foreach (Ennemies ennemie in destroy_ennemies)
            {
                g.ennemies.Remove(ennemie);
            }
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
            g.particle.Draw(spriteBatch, gameTime);
            //spriteBatch.Draw(particle, g.particle.position, Color.White);
            foreach (Ennemies ennemie in g.ennemies)
            {
                ennemie.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
            if (scrollX >= background.Width)
            {
                scrollX = 0;
                //g.vitesse = g.vitesse * 1.3f;
            }
            // TODO: Add your drawing code here            
            base.Draw(gameTime);
        }

        public static bool BoundingCircle(int x1, int y1, int radius1, int x2, int y2, int radius2) 
        { 
            Vector2 V1 = new Vector2(x1, y1); // reference point 1 
            Vector2 V2 = new Vector2(x2, y2); // reference point 2 
            Vector2 Distance = V1 - V2; // get the distance between the two reference points 
            if (Distance.Length() < radius1 + radius2) // if the distance is less than the diameter 
                return true; 
         
            return false;
        }

        public static int GetCenter(int position, int size)
        {
            return position + (size / 2);
        }
    }
}
