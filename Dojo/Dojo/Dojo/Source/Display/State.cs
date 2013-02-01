using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Dojo.Source.Resources;
using Dojo.Source.UI;

namespace Dojo.Source.Display
{
    public class State
    {
        protected GamePadState[] controller;
        protected string[] prevBtn;
        public bool exit { get; protected set; }
        public int args { get; protected set; }

        public State()
        {
            // Constructor
            controller = new GamePadState[Global.MAX_PLAYERS];
            prevBtn = new string[Global.MAX_PLAYERS];

            args = 0;

            for (int i = 0; i < Global.MAX_PLAYERS; i++)
            {
                prevBtn[i] = null;
            }

            exit = false;
        }

        public virtual void Init()
        {
            // Initialisation logic
        }

        public virtual void Unload()
        {
            // Unload content
            controller = null;
        }

        public virtual void Load()
        {
            // Load assets
        }

        public virtual void Update()
        {
            // State logic
            for (int i = 0; i < Global.MAX_PLAYERS; i++)
            {
                controller[i] = GamePad.GetState((PlayerIndex)i);
            }
        }

        public virtual void Draw()
        {
            // Draw
        }

        protected bool Pressed(Buttons button)
        {
            for (int i = 0; i < Global.MAX_PLAYERS; i++)
            {
                if (controller[i].IsConnected)
                {
                    if (controller[i].IsButtonDown(button))
                    {
                        prevBtn[i] = button.ToString();
                    }
                    if (controller[i].IsButtonUp(button) && (prevBtn[i] == button.ToString()))
                    {
                        prevBtn[i] = null;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
