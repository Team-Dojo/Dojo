using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.Engine.Display;

namespace Dojo.Source.Engine.Entity
{
    /// <summary>
    /// Base class for game objects.
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
