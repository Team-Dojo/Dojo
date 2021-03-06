﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dojo.Source.Display
{
    class SpriteSheet
    {
        private float timer; // Timer.
        private Texture2D sheet; // Holds the sprite sheet.
        public int currentFrame { get; private set; } // Pointer to the current frame.
        public int frameWidth { get; private set; } // Width of a frame.
        public int frameHeight { get; private set; } // Height of a frame.
        public Rectangle sourceRect; // Holds the dimensions of the frame to be drawn.

        public SpriteSheet(Texture2D sheet, int frameWidth, int frameHeight)
        {
            this.sheet = sheet;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;

            currentFrame = 1;
            timer = 0f;
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
