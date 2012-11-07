using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Dojo.Source.Resources
{
    /// <summary>
    /// Stores all fonts used by the game
    /// </summary>
    static class Formats
    {
        public static SpriteFont arial;
        public static SpriteFont arialLarge;
        public static SpriteFont arialSmall;
        public static SpriteFont arialTiny;

        public static void Init()
        {
            arial = GameManager.contentManager.Load<SpriteFont>("Fonts/Arial");
            arialLarge = GameManager.contentManager.Load<SpriteFont>("Fonts/ArialLarge");
            arialSmall = GameManager.contentManager.Load<SpriteFont>("Fonts/ArialSmall");
            arialTiny = GameManager.contentManager.Load<SpriteFont>("Fonts/ArialTiny");
        }
    }
}
