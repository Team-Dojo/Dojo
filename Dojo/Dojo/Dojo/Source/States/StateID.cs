using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source
{
    /// <summary>
    /// Stores unique IDs used to switch between game states
    /// </summary>
    static class StateID
    {
        public const int INTRO = 0;
        public const int SPLASH = 1;
        public const int MAIN_MENU = 2;
        public const int PLAY = 3;
        public const int CREDITS = 4;
        public const int SELECT = 5;
    }
}
