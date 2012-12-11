using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Threading.Tasks;

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
        public Vector2 size_window { get; set; }
        public int controller { get; set; }
        public ContentManager content { get; set; }


        public Random random;

        public Game(Vector2 size_window, ContentManager content)
        {
            vitesse = 1f;
            maxEnnemies = 5;
            ennemies = new List<Ennemies>();
            controller = Game.Keyboard;
            random = new Random();
            this.size_window = size_window;
            this.content = content;
        }

        public void generateEnnemies()
        {
            if (ennemies.Count < maxEnnemies)
            {
                if (random.Next(0, 1000) % 5 == 0)
                {
                    bool collision = false;
                    Ennemies newEnnemie = new Ennemies(size_window);
                    newEnnemie.LoadContent(content, "1P");
                    Vector2 pos = new Vector2((int)size_window.X, random.Next(70, (int)(size_window.Y - newEnnemie.texture.Height - 70)));
                    Parallel.ForEach(ennemies, ennemie =>
                    {
                        if (Collision.BoundingCircle(Collision.GetCenter((int)pos.X, (int)newEnnemie.texture.Width), Collision.GetCenter((int)pos.Y, (int)newEnnemie.texture.Height), (int)(newEnnemie.texture.Width / 2), Collision.GetCenter((int)ennemie.position.X, (int)ennemie.texture.Width), Collision.GetCenter((int)ennemie.position.Y, (int)ennemie.texture.Height), (int)(ennemie.texture.Width / 2)))
                        {
                            collision = true;
                        }
                    });

                    /*foreach (Ennemies ennemie in ennemies)
                    {
                        if (!(pos.Y > ennemie.position.Y + ennemie.texture.Height || ennemie.position.Y > pos.Y + newEnnemie.texture.Height))
                        {
                            collision = true;
                        }
                    }*/
                    if (!collision)
                    {
                        newEnnemie.position = pos;
                        float f = (float)((float)random.Next(5, 20) / (float)10);
                        newEnnemie.coef_dep = f;
                        ennemies.Add(newEnnemie);
                    }
                }
            }
        }
    }
}
