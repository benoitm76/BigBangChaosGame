using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BigBangChaosGame
{
    class Collision
    {
        public static bool BoundingCircle(int x1, int y1, int radius1, int x2, int y2, int radius2)
        {
            Vector2 V1 = new Vector2(x1, y1); // reference point 1 
            Vector2 V2 = new Vector2(x2, y2); // reference point 2 
            Vector2 Distance = V1 - V2; // get the distance between the two reference points 
            if (Distance.Length() < radius1 + radius2) // if the distance is less than the diameter 
                return true;

            return false;
        }

        public static int GetCenter(int position, int size)
        {
            return position + (size / 2);
        }
    }
}
