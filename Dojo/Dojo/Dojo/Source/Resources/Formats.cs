using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Dojo.Source.Resources
{
    static class Formats
    {
        public static SpriteFont arial;

        public static void Init()
        {
            arial = GameManager.contentManager.Load<SpriteFont>("Fonts/Arial");
        }
    }
}
