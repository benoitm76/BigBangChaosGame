using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigBangChaosGame.Scene
{
    public class HighScoreMenuScene : AbstractMenuScene
    {
        private BBCGame game;
        public HighScoreMenuScene(SceneManager sceneMgr, BBCGame game)
            : base(sceneMgr, "High Scores")
        {
            this.game = game;
        }

        protected override void OnCancel()
        {


        }
    }
}
