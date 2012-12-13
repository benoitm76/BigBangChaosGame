﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace BigBangChaosGame
{
    public class BBCGame
    {

        #region Constantes
        public const int Keyboard = 0;
        public const int XboxController = 1;
        public const int Mouse = 2;
        #endregion

        public float distance { get; set; }
        public float vitesse { get; set; }
        public Particle particle { get; set; }
        public List<Ennemies> ennemies { get; set; }
        public List<Bonus> bonus { get; set; }
        public int maxEnnemies { get; set; }
        public int maxBonus { get; set; }
        public Vector2 size_window { get; set; }
        public static int controller { get; set; }
        public ContentManager content { get; set; }
        public float distancy_meters { get; set; }
        private SoundEffect accelerateSound;

        public Random random;

        public BBCGame(Vector2 size_window, ContentManager content)
        {
            vitesse = 1f;
            maxEnnemies = 5;
            maxBonus = 2;
            ennemies = new List<Ennemies>();
            bonus = new List<Bonus>();
            random = new Random();
            this.size_window = size_window;
            this.content = content;
            accelerateSound = content.Load<SoundEffect>("Sounds/accelerator_v1.2");
        }

        public void generateEnnemies()
        {
            if (ennemies.Count < maxEnnemies)
            {
                if (random.Next(0, 1000) % 3 == 0)
                {
                    bool collision = false;
                    Ennemies newEnnemie = new Ennemies(size_window);
                    newEnnemie.LoadContent(content, newEnnemie.type_ennemies[random.Next(0,3)]);
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

        public void generateBonus()
        {
            if (bonus.Count < maxBonus)
            {
                if (random.Next(0, 100 * (int)(distance / 1000) + 1) == 55)
                {
                    Bonus newBonus;
                    if (random.Next(0, 100) % 2 == 0)
                    {
                        newBonus = new MediKit(size_window);                                           
                    }
                    else
                    {
                        newBonus = new Invulnerability(size_window);                       
                    }
                    newBonus.LoadContent(content);
                    Vector2 pos = new Vector2((int)size_window.X, random.Next(70, (int)(size_window.Y - newBonus.texture.Height - 70)));
                    newBonus.position = pos;
                    bonus.Add(newBonus);
                }
            }
        }

        public void updateDistancy()
        {
            if (distancy_meters >= 1000)
            {
                if ((int)(distance / 1000) % 4 == 1)
                {
                    if (vitesse <= 3f)
                    {
                        vitesse = vitesse * 1.3f;
                        accelerateSound.Play();
                    }
                }
                if ((int)(distance / 1000) == 2)
                {
                    if (maxEnnemies <= 15)
                    {
                        maxEnnemies += 1;
                    }
                }
                if ((int)(distance / 1000) == 0)
                {
                    if (maxEnnemies <= 15)
                    {
                        maxEnnemies += 1;
                    }
                }
                distancy_meters -= 1000;
            }
            distancy_meters += 1 * vitesse;
            distance += 1 * vitesse;
        }
    }
}
