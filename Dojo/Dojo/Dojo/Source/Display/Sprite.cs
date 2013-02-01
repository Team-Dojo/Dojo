using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dojo.Source.Display
{
    /// <summary>
    /// Base class for objects that are part of the display.
    /// </summary>
    class Sprite
    {
        public string name; // Use: object referencing
        public int height { get; protected set; }
        public int width { get; protected set; }
        public Texture2D texture { get; protected set; }
        public int orientation { get; private set; } // Determines which way the sprite is facing
        public float rotation;
        public Vector2 scale;
        public bool visible; // Determines if the object should be drawn to the display
        public Vector2 position; // Holds top left x/y values of the sprite
        public SpriteEffects effects;
        public float layer;
        public Rectangle sourceRect;
        public enum Orientation { LEFT, RIGHT, UP, DOWN, NONE }

        public Sprite(int orientation = ((int)Orientation.NONE))
        {
            this.orientation = orientation;

            visible = true;
            name = "undefined";
            texture = null;
            rotation = 0.0f;
            layer = 0.0f;
            position = Vector2.Zero;
            scale = new Vector2(1, 1);
            effects = SpriteEffects.None;
        }

        public void SetTexture(string textureLinkage)
        {
            texture = GameManager.contentManager.Load<Texture2D>(textureLinkage);
            height = texture.Bounds.Height;
            width = texture.Bounds.Width;
        }

        public void Draw()
        {
            if ((visible) && (texture != null))
            {
                if (orientation == (int)Orientation.LEFT)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }

                GameManager.spriteBatch.Draw(texture, position, null, Color.White, rotation, Vector2.Zero, scale, effects, layer);
            }
        }
    }
}
