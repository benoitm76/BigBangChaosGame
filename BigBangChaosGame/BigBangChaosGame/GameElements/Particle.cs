using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;
using ParticleEmitter;
using Microsoft.Xna.Framework.Audio;

namespace BigBangChaosGame
{
    public class Particle : Sprite
    {
        public float coefDep { get; set; }
        public int nb_frame_invulnerability { get; set; }
        public int health;

        private SoundEffect collisionSound;
        private SoundEffect alerteSound;

        public Texture2D texture2 { get; set; }

        private Game game;

        ParticleEmitter.ParticleSystem emitter = null;

        public MouseState oldMouseState { get; set; }

        public Particle(Vector2 size_window, Game game) : base (size_window)
        {
            this.game = game;
            health = 5;
            coefDep = 10f;
        }

        public override void Initialize()
        {
            ParticleSystemSettings settings = new ParticleSystemSettings();
            settings.ParticleTextureFileName = "ParticleStar";
            settings.IsBurst = false;
            settings.SetLifeTimes(0.5f, 1f);
            settings.SetScales(0.1f, 0.4f);
            settings.ParticlesPerSecond = 50.0f;
            settings.InitialParticleCount = (int)(settings.ParticlesPerSecond * settings.MaximumLifeTime);
            settings.SetDirectionAngles(0, 360);

            emitter = new ParticleEmitter.ParticleSystem(game, settings);
            emitter.Initialize();

            base.Initialize();
        }
        
        public override void LoadContent(ContentManager content, string assetName) 
        {
            base.LoadContent(content, assetName);
            //On défini la position de la particule
            position = new Vector2(50, (size_window.Y - texture.Height) / 2);

            emitter.OriginPosition = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);            
        }

        public void LoadContent(ContentManager content)
        {
            base.LoadContent(content, "particle_v2.1");
            texture2 = content.Load<Texture2D>("particle2_v1.0");
            //On défini la position de la particule
            position = new Vector2(50, (size_window.Y - texture.Height) / 2);

            emitter.OriginPosition = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);

            collisionSound = content.Load<SoundEffect>("Sounds/collision_v1.2");
            alerteSound = content.Load<SoundEffect>("Sounds/alert_v1.0");
        }

        public void HandleInput(int controller)
        {            
            //Permet de déplacer la particule
            Vector2 displacement = new Vector2();
            Vector2 newPos = new Vector2(position.X, position.Y);
            if(controller == BBCGame.Keyboard)
            {
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    displacement.Y = -1;
                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    displacement.Y = 1;
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    displacement.X = 1;
                }
                else if (keyboardState.IsKeyDown(Keys.Left))
                {
                    displacement.X = -1;
                }
                newPos.X = newPos.X + displacement.X * coefDep;
                newPos.Y = newPos.Y + displacement.Y * coefDep;
            }

            else if (controller == BBCGame.XboxController)
            {
                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
                GamePadCapabilities gamepadCaps = GamePad.GetCapabilities(PlayerIndex.One);
                Vector2 xbox360 = gamepadState.ThumbSticks.Left;
                displacement.X = xbox360.X;
                displacement.Y = -xbox360.Y;
                newPos.X = newPos.X + displacement.X * coefDep;
                newPos.Y = newPos.Y + displacement.Y * coefDep;
            }
            else if (controller == BBCGame.Mouse)
            {
                MouseState mouseState = Mouse.GetState();
                displacement.X = mouseState.X - oldMouseState.X;
                displacement.Y = mouseState.Y - oldMouseState.Y;
                oldMouseState = mouseState;
                newPos.X = newPos.X + displacement.X * (coefDep / 10);
                newPos.Y = newPos.Y + displacement.Y * (coefDep / 10);
                
            }
            position = newPos;
        }

        public override void Update(GameTime gameTime)
        {
            //On vérifie que la particule ne sorte pas de l'écran
            Vector2 newPos = new Vector2(position.X, position.Y);
            if (newPos.X + texture.Width > size_window.X)
            {
                newPos.X = size_window.X - texture.Width;
            }
            if (newPos.X < 0)
            {
                newPos.X = 0;
            }
            if (newPos.Y + texture.Height > size_window.Y - 70)
            {
                newPos.Y = size_window.Y - texture.Height - 70;
            }
            if (newPos.Y < 70)
            {
                newPos.Y = 70;
            }            
            position = newPos;
            emitter.OriginPosition = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            emitter.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.End();
            emitter.Draw(gameTime);
            spriteBatch.Begin();
            Color color = Color.White;
            Color color2 = Color.Black;
            color2 = new Color(Math.Abs((health - 5)) * 127, Math.Abs((health - 5)) * 127, Math.Abs((health - 5)) * 51, Math.Abs((health - 5)) * 51);
            color = new Color(health * 51, health * 51, health * 51, health * 51);   
            if (nb_frame_invulnerability != 0)
            {
                if ((int)(nb_frame_invulnerability / 20) % 2 == 0)
                {
                    color = Color.Transparent;
                    color2 = Color.Transparent;
                }
                nb_frame_invulnerability--;
            }                  
            spriteBatch.Draw(texture, position, color);
            spriteBatch.Draw(texture2, position, color2);             
        }

        public void touched()
        {
            health--;
            nb_frame_invulnerability = 60;
            collisionSound.Play();
            if (health == 1)
            {
                alerteSound.Play();
            }
            new Task(() =>
                {
                    GamePad.SetVibration(PlayerIndex.One, 0.7f, 0.25f);
                    System.Threading.Thread.Sleep(500);
                    GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
                }).Start();
        }
    }
}
