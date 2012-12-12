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
using System.Threading.Tasks;
using System.Threading;
using ParticleEmitter;

namespace BigBangChaosGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Vector2 size_window;

        private Texture2D background;

        private int scrollX = 1;

        private Game g;

        static Mutex mu;

        private float distancy_meters = 0;

        ParticleEmitter.ParticleSystem emitter = null;

        public Game1()
        {
            //Chargement des paramètres grapiques
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            size_window = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            mu = new Mutex();

            g = new Game(size_window, Content);            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ParticleSystemSettings settings = new ParticleSystemSettings();
            settings.ParticleTextureFileName = "ParticleStar";
            settings.IsBurst = false;
            settings.SetLifeTimes(1f, 1.5f);
            settings.SetScales(0.1f, 0.4f);
            settings.ParticlesPerSecond = 50.0f;
            settings.InitialParticleCount = (int)(settings.ParticlesPerSecond * settings.MaximumLifeTime);
            settings.SetDirectionAngles(0, 360);

            emitter = new ParticleEmitter.ParticleSystem(this, settings);
            Components.Add(emitter);

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

            //Chargement du fond
            background = Content.Load<Texture2D>("pipe_v2.0");

            //Chargement de la particule
            g.particle = new Particle(new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            g.particle.LoadContent(Content, "particle_v2.1");

            //Ajout d'un ennemie
            /*g.ennemies.Add(new Ennemies(size_window, new Vector2(size_window.X - 500, size_window.Y / 2)));
            g.ennemies[0].coef_dep = 0.9f;
            foreach (Sprite ennemie in g.ennemies)
            {
                ennemie.LoadContent(Content, "1P");
            }*/

            //Mise à jour position de la souris
            Mouse.SetPosition((int)g.particle.position.X, (int)g.particle.position.Y);
            g.particle.oldMouseState = Mouse.GetState();
            // TODO: use this.Content to load your game content here

            emitter.OriginPosition = new Vector2(g.particle.position.X + g.particle.texture.Width / 2, g.particle.position.Y + g.particle.texture.Height / 2);
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
            //Mouvement de la particule
            g.particle.HandleInput(Game.XboxController);

            //Mise à jour de la position de la particule
            g.particle.Update(gameTime);

            //Déplacement du fond 
            int displacementX = (int)(5 * g.vitesse);

            //On déplace tous les objets
            List<Ennemies> destroy_ennemies = new List<Ennemies>();
            Parallel.ForEach(g.ennemies, ennemie =>
            {
                ennemie.Update(gameTime, displacementX);
                if (g.particle.nb_frame_invulnerability == 0)
                {
                    //On vérifie si il y a collision avec un ennemie
                    if (Collision.BoundingCircle(Collision.GetCenter((int)g.particle.position.X, (int)g.particle.texture.Width), Collision.GetCenter((int)g.particle.position.Y, (int)g.particle.texture.Height), (int)(g.particle.texture.Width / 2), Collision.GetCenter((int)ennemie.position.X, (int)ennemie.texture.Width), Collision.GetCenter((int)ennemie.position.Y, (int)ennemie.texture.Height), (int)(ennemie.texture.Width / 2)))
                    {
                        mu.WaitOne();
                        g.particle.touched();
                        mu.ReleaseMutex();
                    }
                }

                //On supprime les ennemies disparu de l'écran
                if (ennemie.position.X < 0 - ennemie.texture.Width)
                {
                    mu.WaitOne();
                    destroy_ennemies.Add(ennemie);
                    mu.ReleaseMutex();
                }
            });

            //Mise à jour de la difficulté du jeux en fonction de la distance
            if (distancy_meters >= 1000)
            {
                g.distance++;
                if (g.distance % 3 == 1)
                {
                    if (g.vitesse <= 2.5f)
                    {
                        g.vitesse = g.vitesse * 1.5f;
                    }
                }
                if (g.distance % 3 == 2)
                {
                    if (g.maxEnnemies <= 15)
                    {
                        g.maxEnnemies += 2;
                    }
                }
                distancy_meters -= 1000;
            }
            distancy_meters += 1 * g.vitesse;            

            //On met à jour la liste des ennemies
            foreach (Ennemies ennemie in destroy_ennemies)
            {
                g.ennemies.Remove(ennemie);
            }
            if (scrollX % (int)(10 / g.vitesse) == 0)
            {
                g.generateEnnemies();
            }

            emitter.OriginPosition = new Vector2(g.particle.position.X + g.particle.texture.Width / 2, g.particle.position.Y + g.particle.texture.Height / 2);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //On augmente le scrolling du fond
            scrollX = (int)(scrollX + 5 * g.vitesse);            
            
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);

            //On dessine le fond
            spriteBatch.Draw(background, Vector2.Zero, new Rectangle(scrollX, 0, background.Width, background.Height), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);            

            spriteBatch.Begin();

            //On dessine la particule
            g.particle.Draw(spriteBatch, gameTime);
      
            //On dessine les ennemies
            foreach (Ennemies ennemie in g.ennemies)
            {
                ennemie.Draw(spriteBatch, gameTime);
            }

            spriteBatch.End();

            //On reset le scrolling quand il le faut
            if (scrollX >= background.Width)
            {
                scrollX = 0;      
            }
            
            // TODO: Add your drawing code here            
            
        }
    }
}
