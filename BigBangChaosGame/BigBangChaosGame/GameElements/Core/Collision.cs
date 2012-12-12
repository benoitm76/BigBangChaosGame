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

        public static bool CheckCollision(Rectangle rect_A, Color[] coul_A, Rectangle rect_B, Color[] coul_B)
        {
            if (rect_A.Intersects(rect_B))
            {
                int top = Math.Max(rect_A.Top, rect_B.Top);
                int bottom = Math.Min(rect_A.Bottom, rect_B.Bottom);
                int left = Math.Max(rect_A.Left, rect_B.Left);
                int right = Math.Min(rect_A.Right, rect_B.Right);

                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        Color colorA = coul_A[(x - rect_A.Left) + (y - rect_A.Top) * rect_A.Width];
                        Color colorB = coul_A[(x - rect_B.Left) + (y - rect_B.Top) * rect_B.Width];

                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
            
        }
    }
}
