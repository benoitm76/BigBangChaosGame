using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BigBangChaosGame.Scene
{
    public class HighScoreMenuScene : AbstractMenuScene
    {
        private BBCGame game;
        public HighScoreMenuScene(SceneManager sceneMgr, BBCGame game)
            : base(sceneMgr, "")
        {
            this.game = game;
        }


        public override void Initialize()
        {               
            // TODO: Add your initialization logic here
            base.Initialize();           
        }
        protected override void LoadContent()
        {


            
        }

        public override void Update(GameTime gameTime)
        {


        }


        public override void Draw(GameTime gameTime)
        {


        }



        protected override void OnCancel()
        {


        }
    }
}
