using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dojo.Source.Framework.Display
{
    /// <summary>
    /// Base class for objects that are part of the display.
    /// Provides basic collision detection.
    /// </summary>
    class Sprite
    {
        public int height { get; protected set; }
        public int width { get; protected set; }
        public Texture2D texture { get; protected set; }
        public int orientation { get; private set; } // Determines which way the sprite is facing

        public float rotation = 0.0f;
        public Vector2 scale = new Vector2(1, 1);
        public bool visible = true; // Determines if the object should be drawn to the display
        public Vector2 position = new Vector2(0, 0); // Holds top left x/y values of the sprite
        public SpriteEffects effects = SpriteEffects.None;
        public float layer = 0.0f;
        public Rectangle sourceRect;
        public enum Orientation { LEFT, RIGHT, UP, DOWN, NONE }

        public Sprite(int _orientation)
        {
            texture = null;
            orientation = _orientation;
        }

        public void SetTexture(string _relativeTexturePath)
        {
            texture = GameManager.contentManager.Load<Texture2D>(_relativeTexturePath);
            height = texture.Bounds.Height;
            width = texture.Bounds.Width;
        }

        public void Draw()
        {
            if (visible)
            {
                if (orientation == (int)Orientation.LEFT)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }

                GameManager.spriteBatch.Draw(texture, position, null, Color.White, rotation, Vector2.Zero, scale, effects, layer);
            }
        }

        public bool HitTestObject(Sprite collisionObject)
        {
            if (((position.X + (width / 2)) + (width / 2)) < ((collisionObject.position.X + (collisionObject.width / 2)) - (collisionObject.width / 2)))
            {
                return false;
            }
            if (((position.X + (width / 2)) - (width / 2)) > ((collisionObject.position.X + (collisionObject.width / 2)) + (collisionObject.width / 2)))
            {
                return false;
            }
            if (((position.Y + (height / 2)) + (height / 2)) < ((collisionObject.position.Y + (collisionObject.height / 2)) - (collisionObject.height / 2)))
            {
                return false;
            }
            if (((position.Y + (height / 2)) - (height / 2)) > ((collisionObject.position.Y + (collisionObject.height / 2)) + (collisionObject.height / 2)))
            {
                return false;
            }
	        return true;
        }
    }
}
