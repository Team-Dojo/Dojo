using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dojo.Source.Framework.Display
{
    class SpriteSheet
    {
        private float timer = 0f; // Timer.
        private Texture2D sheet; // Holds the sprite sheet.
        public int currentFrame { get; private set; } // Pointer to the current frame.
        public int frameWidth { get; private set; } // Width of a frame.
        public int frameHeight { get; private set; } // Height of a frame.
        public Rectangle sourceRect; // Holds the dimensions of the frame to be drawn.

        public SpriteSheet(Texture2D _sheet, int _frameWidth, int _frameHeight)
        {
            sheet = _sheet;
            frameWidth = _frameWidth;
            frameHeight = _frameHeight;
            currentFrame = 1;
        }

        public void Animate(int initialFrame, int targetFrame, float interval)
        {
            if (timer > interval)
            {
                currentFrame++; // Increment frame of animation.
                timer = 0f; // Reset.
            }
            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight); // Grab frame to be displayed.
        }
    }
}
