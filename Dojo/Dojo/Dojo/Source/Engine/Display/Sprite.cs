using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dojo.Source.Engine.Display
{
    /// <summary>
    /// Base class for objects that are part of the display.
    /// Provides basic collision detection.
    /// </summary>
    class Sprite
    {
        public int height { get; protected set; }
        public int width { get; protected set; }
        public bool visible = true; // Determines if the object should be drawn to the display.
        public Vector2 position = new Vector2(0, 0); // Holds x/y values.
        public Texture2D texture = null;

        public Sprite()
        {
        }

        public void Draw()
        {
            if (visible)
            {
                Game.spriteBatch.Draw(texture, position, Color.White);
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
