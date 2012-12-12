using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace BigBangChaosGame
{
    class MouseEvent
    {
        MouseState buttonPressed;
        public Rectangle mouseDetection;

        public MouseEvent()
        {
        }

        public bool UpdateMouse()
        {
            buttonPressed = Mouse.GetState();

            if (buttonPressed.LeftButton == ButtonState.Pressed)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public Rectangle getMouseContainer()
        {
            mouseDetection = new Rectangle((int)buttonPressed.X,
                (int)buttonPressed.Y,
                (int)1,
                (int)1);
            return mouseDetection;
        }
    }
}
