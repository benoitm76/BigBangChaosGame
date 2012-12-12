using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigBangChaosGame
{
    public class MainMenuScene : AbstractMenuScene
    {
        public MainMenuScene(SceneManager sceneMgr)
            : base(sceneMgr, "Menu principal")
        {
        }

        protected override void OnCancel()
        {
        }
    }
}
