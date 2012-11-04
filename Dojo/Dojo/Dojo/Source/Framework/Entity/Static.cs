using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.Framework.Display;

namespace Dojo.Source.Framework.Entity
{
    /// <summary>
    /// Base class for game objects.
    /// </summary>
    class Static : Sprite
    {
        public const int LEFT = 0;
        public const int RIGHT = 1;
        public const int UP = 2;
        public const int DOWN = 3;
        public const int NONE = 4;

        public bool collidable;
        public int orientation { get; private set; }
        protected SpriteSheet spriteSheet;

        public Static(bool _collidable, int _orientation)
        {
            collidable = _collidable;
            orientation = _orientation;
        }
    }
}
