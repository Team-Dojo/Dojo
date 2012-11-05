using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Dojo.Source.Framework.Display;
using Dojo.Source.UI;
using Dojo.Source.Resources;

namespace Dojo.Source.States
{
    class Splash : State
    {
        private Static screen = new Static(false, (int)Sprite.Orientation.RIGHT);
        //private ErrorBox error;

        public Splash()
        {
            
        }

        public override void Init()
        {
            screen.SetTexture("Assets/Splash");

            base.Init();
        }

        public override void Load()
        {
            //error = new ErrorBox("TEST TEST TEST", Formats.arial);

            base.Load();
        }

        public override void Update()
        {
            // Switch state
            for (int i = 0; i < Ref.MAX_PLAYERS; i++)
            {
                if (controller[i].IsConnected)
                {
                    if (controller[i].Buttons.A == ButtonState.Pressed)
                    {
                        GameManager.SwitchState(StateID.PLAY);
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                GameManager.SwitchState(StateID.PLAY);
            }

            base.Update();
        }

        public override void Draw()
        {
            GameManager.spriteBatch.Draw(screen.texture, Vector2.Zero, Color.White);
            //error.Draw();

            base.Draw();
        }
    }
}
