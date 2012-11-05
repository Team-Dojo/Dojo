using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.Framework.Display;

namespace Dojo.Source.Framework.Display
{
    /// <summary>
    /// Base class for game objects that are static (do not move).
    /// Adds support for collision and animation.
    /// </summary>
    class Static : Sprite
    {   
        public bool collidable;       
        protected SpriteSheet spriteSheet;

        public Static(bool _collidable, int _orientation) 
            : base(_orientation)
        {
            collidable = _collidable;
        }
    }
}
