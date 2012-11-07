using System;

namespace Dojo.Source
{
#if WINDOWS || XBOX
    static class Program
    {
        // Properties
        public static int SCREEN_WIDTH  = 1280;
        public static int SCREEN_HEIGHT = 720;
        public static string BUILD = "Alpha Build 6.5";

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