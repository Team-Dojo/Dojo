using System;
using Microsoft.Xna.Framework;

namespace Dojo.Source
{
#if WINDOWS || XBOX
    static class Program
    {
        // Properties
        public static Vector2 baseScreenSize = new Vector2(1280, 720); // Important game elements have to be rendered in 80% - 90% of the fullscreen size.

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameManager gameManager = new GameManager())
            {
                gameManager.Run();
            }
        }
    }
#endif
}