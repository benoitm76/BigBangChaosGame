using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ParticleEmitter;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using BigBangChaosGame.Scene;

namespace BigBangChaosGame
{
    class GameplayScene : AbstractGameScene
    {
        SpriteBatch spriteBatch;

        private Vector2 size_window;

        private Texture2D background;

        private int scrollX = 1;

        private BBCGame g;

        static Mutex mu;

        private float distancy_meters = 0;

        private ContentManager Content;

        private SceneManager sceneMgr;

        private float _pauseAlpha;

        // ajout 12/12 9h by Simon, barre de vie et texte barre de vie.
        private SpriteFont _lifePourcent;
        Texture2D mHealthBar;

        // ajout 12/12 9h by Simon, Affiche de la distance
        private SpriteFont _Dist;

        public GameplayScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            this.sceneMgr = sceneMgr;
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            if (Content == null)
                Content = new ContentManager(SceneManager.Game.Services, "Content");

            size_window = new Vector2(1280, 720);
            mu = new Mutex();

            g = new BBCGame(size_window, Content); 
        }

        public override void Initialize()
        {               
            // TODO: Add your initialization logic here
            base.Initialize();           
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Chargement du fond
            background = Content.Load<Texture2D>("pipe_v2.0");

            //Chargement de la particule
            g.particle = new Particle(new Vector2(size_window.X, size_window.Y), SceneManager.Game);
            g.particle.Initialize();
            g.particle.LoadContent(Content, "particle_v2.1");

            mHealthBar = Content.Load<Texture2D>("vie") as Texture2D;
            _lifePourcent = Content.Load<SpriteFont>("Life");
            // chargement barre de vie et texte by Simon 12/12 9h
            _Dist = Content.Load<SpriteFont>("Dist");

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
        }

        public override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                sceneMgr.Game.Exit();

            // TODO: Add your update logic here
            //Mouvement de la particule

            if (IsActive)
            {
                if (g.particle.health <= 0)
                {
                    this.Remove();
                    new HighScoreMenuScene(sceneMgr).Add();
                }
                //On augmente le scrolling du fond
                scrollX = (int)(scrollX + 5 * g.vitesse);

                //On reset le scrolling quand il le faut
                if (scrollX >= background.Width)
                {
                    scrollX = 0;
                }                
                g.particle.HandleInput(BBCGame.Keyboard);

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
                        /*if (Collision.BoundingCircle(Collision.GetCenter((int)g.particle.position.X, (int)g.particle.texture.Width), Collision.GetCenter((int)g.particle.position.Y, (int)g.particle.texture.Height), (int)(g.particle.texture.Width / 2), Collision.GetCenter((int)ennemie.position.X, (int)ennemie.texture.Width), Collision.GetCenter((int)ennemie.position.Y, (int)ennemie.texture.Height), (int)(ennemie.texture.Width / 2)))
                        {
                            mu.WaitOne();
                            g.particle.touched();
                            mu.ReleaseMutex();
                        }*/
                        if (Collision.CheckCollision(g.particle.getRectangle(), g.particle.color, ennemie.getRectangle(), ennemie.color))
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


                List<Bonus> destroy_bonus = new List<Bonus>();
                Parallel.ForEach(g.bonus, lbonus =>
                {
                    lbonus.Update(gameTime, displacementX);
                    if (Collision.CheckCollision(g.particle.getRectangle(), g.particle.color, lbonus.getRectangle(), lbonus.color))
                    {
                        mu.WaitOne();
                        lbonus.applyBonus(g);
                        destroy_bonus.Add(lbonus);
                        mu.ReleaseMutex();
                    }
                    else if (lbonus.position.X < 0 - lbonus.texture.Width)
                    {
                        mu.WaitOne();
                        destroy_bonus.Add(lbonus);
                        mu.ReleaseMutex();
                    }
                });

                //Mise à jour de la difficulté du jeux en fonction de la distance
                if (distancy_meters >= 1000)
                {
                    g.distance++;
                    if (g.distance % 4 == 1)
                    {
                        if (g.vitesse <= 2.5f)
                        {
                            g.vitesse = g.vitesse * 1.3f;
                        }
                    }
                    if (g.distance % 4 == 2)
                    {
                        if (g.maxEnnemies <= 15)
                        {
                            g.maxEnnemies += 1;
                        }
                    }
                    if (g.distance % 4 == 0)
                    {
                        if (g.maxEnnemies <= 15)
                        {
                            g.maxEnnemies += 1;
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
                foreach (Bonus lbonus in destroy_bonus)
                {
                    g.bonus.Remove(lbonus);
                }
                if (scrollX % (int)(10 / g.vitesse) == 0)
                {
                    Parallel.Invoke(g.generateEnnemies, g.generateBonus);
                }                
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);

            //On dessine le fond
            spriteBatch.Draw(background, Vector2.Zero, new Rectangle(scrollX, 0, background.Width, background.Height), Color.White);          
            
            // Desinne la barre de vie et son texte, by Simon
            decimal pourcent = 100 - ((decimal)g.particle.health / 5) * 100;
            if (pourcent > 100)
            {
                pourcent = 100;

            }
            
            spriteBatch.Draw(mHealthBar, new Rectangle(10,
            10, mHealthBar.Width, mHealthBar.Height), new Rectangle(0, 0, mHealthBar.Width, mHealthBar.Height), Color.Blue);

            spriteBatch.Draw(mHealthBar, new Rectangle(10,
             10, (int)(mHealthBar.Width * (pourcent / 100)), mHealthBar.Height), new Rectangle(0, 0, mHealthBar.Width, mHealthBar.Height), Color.Red);

            string text = string.Format("Formation of Bigbang: {0} %", (int)pourcent);
            spriteBatch.DrawString(_lifePourcent, text, new Vector2(40, 13), Color.White);

            //Texte Distance
            string text2 = string.Format("{0},{1:000}", g.distance, distancy_meters);
            spriteBatch.DrawString(_Dist, text2, new Vector2(800, 13), Color.Red);
            spriteBatch.DrawString(_Dist, "K Km traveled", new Vector2(840, 13), Color.Red);

            //On dessine la particule
            g.particle.Draw(spriteBatch, gameTime);

            //On dessine les ennemies
            foreach (Ennemies ennemie in g.ennemies)
            {
                ennemie.Draw(spriteBatch, gameTime);
            }

            foreach (Bonus lbonus in g.bonus)
            {
                lbonus.Draw(spriteBatch, gameTime);
            }

            spriteBatch.End();


            base.Draw(gameTime);

            if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);
                SceneManager.FadeBackBufferToBlack(alpha);
            }

            // TODO: Add your drawing code here            

        }
    }
}
