using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigBangChaosGame
{
    class Game
    {
        public int distance { get; set; }
        public float vitesse { get; set; }
        public Particle particle { get; set; }
        public List<Sprite> ennemies { get; set; }
        public int maxEnnemies { get; set; }

        public Game()
        {
            vitesse = 1f;
            maxEnnemies = 10;
        }
    }
}
