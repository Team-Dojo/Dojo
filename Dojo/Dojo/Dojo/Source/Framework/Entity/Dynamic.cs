using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Dojo.Source.Framework.Display;

namespace Dojo.Source.Framework.Entity
{
    /// <summary>
    /// Base class for objects which can move and have animation.
    /// </summary>
    class Dynamic : Static
    {
        public Vector2 speed = new Vector2(0, 0);
        public Vector2 acceleration = new Vector2(0, 0);
        public Vector2 deceleration = new Vector2(0, 0);
        public Vector2 maxSpeed = new Vector2(0, 0);

        public Dynamic(bool _collidable, int _orientation) 
            : base(_collidable, _orientation)
        {
            
        }
    }
}
