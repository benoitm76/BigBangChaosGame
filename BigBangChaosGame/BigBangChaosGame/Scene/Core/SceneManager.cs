using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BigBangChaosGame
{
    public class SceneManager : DrawableGameComponent
    {
        private readonly List<AbstractGameScene> _scenes = new List<AbstractGameScene>();
        private readonly List<AbstractGameScene> _scenesToUpdate = new List<AbstractGameScene>();
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D _blankTexture;

        #region Properties

        /// <summary>
        /// Un SpriteBatch partagé pour toutes les scènes.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
        }

        /// <summary>
        /// Une police partagée pour toutes les scènes.
        /// </summary>
        public SpriteFont Font
        {
            get { return _font; }
        }

        #endregion

        public SceneManager(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        protected override void LoadContent()
        {
            ContentManager content = Game.Content;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = content.Load<SpriteFont>("menufont");
            _blankTexture = content.Load<Texture2D>("blank");
        }

        public override void Update(GameTime gameTime)
        {
            // Fait un copie de la liste principale pour éviter la confusion des
            // processus mettant à jour une scène ou en retirant une.
            _scenesToUpdate.Clear();

            foreach (AbstractGameScene scene in _scenes)
                _scenesToUpdate.Add(scene);

            bool othersceneHasFocus = !Game.IsActive;
            bool coveredByOtherscene = false;

            while (_scenesToUpdate.Count > 0)
            {
                AbstractGameScene scene = _scenesToUpdate[_scenesToUpdate.Count - 1];
                _scenesToUpdate.RemoveAt(_scenesToUpdate.Count - 1);
                scene.Update(gameTime, othersceneHasFocus, coveredByOtherscene);

                if (scene.SceneState == SceneState.TransitionOn ||
                    scene.SceneState == SceneState.Active)
                {
                    // Si c'est la première scène, lui donner l'accès aux entrées utilisateur.
                    if (!othersceneHasFocus)
                    {
                        scene.HandleInput();
                        othersceneHasFocus = true;
                    }

                    // Si la scène courant n'est pas un popup et est active,
                    // informez les scènes suivantes qu'elles sont recouverte.
                    if (!scene.IsPopup)
                        coveredByOtherscene = true;
                }
            }
        }

        public void AddScene(AbstractGameScene scene)
        {
            scene.IsExiting = false;
            Game.Components.Add(scene);
            _scenes.Add(scene);
        }

        public void RemoveScene(AbstractGameScene scene)
        {
            Game.Components.Remove(scene);
            _scenes.Remove(scene);
            _scenesToUpdate.Remove(scene);
        }

        public void FadeBackBufferToBlack(float alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            _spriteBatch.Begin();
            _spriteBatch.Draw(_blankTexture,
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             Color.Black * alpha);
            _spriteBatch.End();
        }
    }
}
