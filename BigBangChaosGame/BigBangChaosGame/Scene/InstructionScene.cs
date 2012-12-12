using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigBangChaosGame
{
    class InstructionScene : AbstractGameScene
    {
        private SceneManager sceneMgr;

        public InstructionScene(SceneManager sceneMgr)
            : base(sceneMgr)
        {
            //new GameplayScene(sceneMgr).Add();
            this.sceneMgr = sceneMgr;
        }
    }
}
