using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigBangChaosGame
{
    class ScoreScene: AbstractGameScene
    {
        private SceneManager sceneMgr;

        public ScoreScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            //new GameplayScene(sceneMgr).Add();
            this.sceneMgr = sceneMgr;
        }
    }
}
