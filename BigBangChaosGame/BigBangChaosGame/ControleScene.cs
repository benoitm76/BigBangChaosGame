using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigBangChaosGame
{
    class ControleScene: AbstractGameScene
    {
        private SceneManager sceneMgr;

        public ControleScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            //new GameplayScene(sceneMgr).Add();
            this.sceneMgr = sceneMgr;
        }
    }
}
