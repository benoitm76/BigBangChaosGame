using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BigBangChaosGame
{
    class Particle
    {
        public Vector2 position { get; set; }
        public float coefDep { get; set; }
        public Vector2 size_window { get; set; }
        public Vector2 size_particle { get; set; }

        public Particle(Vector2 size_particle, Vector2 size_window)
        {
            position = new Vector2(50, (size_window.Y - size_particle.Y) / 2);
            this.size_window = size_window;
            this.size_particle = size_particle;
            coefDep = 1f;
        }

        public void Displacement(Vector2 displacement)
        {
            Vector2 newPos = new Vector2(position.X, position.Y);
            newPos.X = newPos.X + displacement.X * coefDep;
            if (newPos.X + size_particle.X > size_window.X)
            {
                newPos.X = size_window.X - size_particle.X;
            }
            if (newPos.X < 0)
            {
                newPos.X = 0;
            }
            if (newPos.Y + size_particle.Y > size_window.Y)
            {
                newPos.Y = size_window.Y - size_particle.Y;
            }
            if (newPos.Y < 0)
            {
                newPos.Y = 0;
            }
            newPos.Y = newPos.Y + displacement.Y * coefDep;
            position = newPos;
        }
    }
}
