using System;
using Dojo.Source.States;

namespace Dojo.Source
{
#if WINDOWS || XBOX
    static class Program
    {
        // Properties
        static int state = 0;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            switch (state)
            {
                case States.SPLASH:
                    break;
            }
        }
    }
#endif
}

