using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigBangChaosGame.Scene
{
    public class HighScoreMenuScene : AbstractMenuScene
    {
        public HighScoreMenuScene(SceneManager sceneMgr)
            : base(sceneMgr, "High Scores")
        {
        }

        protected override void OnCancel()
        {


        }
    }
}
