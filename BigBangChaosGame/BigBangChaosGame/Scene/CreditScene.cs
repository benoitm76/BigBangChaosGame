using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigBangChaosGame
{
    class CreditScene : AbstractGameScene
    {
        private SceneManager sceneMgr;

        public CreditScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            //new GameplayScene(sceneMgr).Add();
            this.sceneMgr = sceneMgr;
        }
    }
}
