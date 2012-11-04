using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Dojo.Source.Framework.Entity;
using Dojo.Source.Framework.Display;

namespace Dojo.Source.States
{
    class Splash : State
    {
        private Static screen = new Static(false, Static.RIGHT);

        public Splash()
        {
            
        }

        public override void Init()
        {
            // Configure screen
        }

        public override void Draw()
        {
            GameManager.spriteBatch.Draw(screen.texture, Vector2.Zero, Color.White);
        }

        public override void Load()
        {
            screen.texture = GameManager.contentManager.Load<Texture2D>("Splash");
        }

        public override void Update()
        {
            // Switch state
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                GameManager.SwitchState(StateID.PLAY);
                System.Console.WriteLine("A");
            }
        }
    }
}
