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
        public const int SPLASH = 0;
        public const int MAIN_MENU = 1;
        public const int CHARACTER_SELECT = 2;
        public const int PLAY = 3;
        public const int STATS = 4;
    }
}
