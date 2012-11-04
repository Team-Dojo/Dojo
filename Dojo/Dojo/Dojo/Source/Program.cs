using System;
//using Dojo.Source.States;

namespace Dojo.Source
{
#if WINDOWS || XBOX
    static class Program
    {
        // Properties
        static int state = 0;
        //Game game = new Game();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game game = new Game())
            {
				game.Run();
            }
            
            //switch (state)
            //{
            //    //case States.SPLASH:
            //    //    break;
            //    //case States.MAIN_MENU:
            //    //    break;               
            //    //case States.GAME:
            //    // //
            //    //    break;
            //    //case States.STATS:
            //    //    break;

            //}
        }
    }
#endif
}

