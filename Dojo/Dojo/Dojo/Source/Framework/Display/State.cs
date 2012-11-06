using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Dojo.Source.Resources;
using Dojo.Source.UI;

namespace Dojo.Source.Framework.Display
{
    public class State
    {
        protected GamePadState[] controller;
        //protected ErrorBox error;

        public State()
        {
            // Constructor
            controller = new GamePadState[Ref.MAX_PLAYERS];
            //error = new ErrorBox("Controller not connected", Formats.arial);
        }

        public virtual void Init()
        {
            // Initialisation logic
        }

        public virtual void Load()
        {
            // Load assets
        }

        public virtual void Update()
        {
            // State logic
            for (int i = 0; i < Ref.MAX_PLAYERS; i++)
            {
                controller[i] = GamePad.GetState((PlayerIndex)i);
            }
        }

        public virtual void Draw()
        {
            // Draw
        }
    }
}
