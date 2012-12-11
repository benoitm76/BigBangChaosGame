using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigBangChaosGame
{
    class Game
    {

        #region Constantes
        public const int Keyboard = 0;
        public const int XboxController = 1;
        public const int Mouse = 2;
        #endregion

        public int distance { get; set; }
        public float vitesse { get; set; }
        public Particle particle { get; set; }
        public List<Ennemies> ennemies { get; set; }
        public int maxEnnemies { get; set; }
        public int controller;



        public Game()
        {
            vitesse = 1f;
            maxEnnemies = 10;
            ennemies = new List<Ennemies>();
            controller = Game.Keyboard;
        }
    }
}
