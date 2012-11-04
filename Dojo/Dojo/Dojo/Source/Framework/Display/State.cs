using System;
using System.Collections.Generic;
using System.Linq;

namespace Dojo.Source.Framework.Display
{
    public class State
    {
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
