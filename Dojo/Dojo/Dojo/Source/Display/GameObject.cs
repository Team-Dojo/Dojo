using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Dojo.Source.Display
{
    /// <summary>
    /// Base class for game objects.
    /// Provides extensions for movement, collision, and animation.
    /// </summary>
    class GameObject : Sprite
    {
        public Vector2 speed = new Vector2(0, 0);
        public Vector2 acceleration = new Vector2(0, 0);
        public bool collidable;
        protected SpriteSheet spriteSheet;

        public GameObject(bool collidable, int orientation = ((int)Orientation.NONE)) 
            : base(orientation)
        {
            this.collidable = collidable;
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
