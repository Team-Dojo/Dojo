using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Dojo.Source.Framework.Display
{
    public class State
    {
        //protected GamePadState controller;

        public State()
        {
            
        }

        public virtual void Init()
        {
            // Initialisation logic
        }

        public virtual void Draw()
        {
            // Draw
        }

        public virtual void Load()
        {
            // Load assets
        }

        public virtual void Update()
        {
            // State logic
        }
    }
}
