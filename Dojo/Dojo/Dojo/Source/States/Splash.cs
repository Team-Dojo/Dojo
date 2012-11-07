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
        private Static screen;
        private Sprite instruction;
        private int timer;
        private bool drawInstruction;
        private const int TIMER_END = 75;
        private const int TIMER_START = 0;
        private const int DELAY = 20;

        public Splash()
        {
            drawInstruction = true;
            screen = new Static(false, (int)Sprite.Orientation.RIGHT);
            instruction = new Sprite((int)Sprite.Orientation.NONE);
            timer = 0;
        }

        public override void Init()
        {
            screen.SetTexture("Assets/States/Splash");
            instruction.SetTexture("Assets/States/SplashPlay");

            base.Init();
        }

        public override void Load()
        {
            base.Load();
        }

        public override void Update()
        {
            if (timer == TIMER_END)
            {
                if (drawInstruction)
                {
                    timer = TIMER_END - DELAY;
                }
                else
                {
                    timer = TIMER_START;
                }

                drawInstruction = !drawInstruction;
            }
          

            // Switch state
            if (Pressed(Buttons.A))
            {
                GameManager.SwitchState(StateID.PLAY);
            }

            timer++;

            base.Update();
        }

        public override void Draw()
        {
            GameManager.spriteBatch.Draw(screen.texture, Vector2.Zero, Color.White);
            if (drawInstruction)
            {
                GameManager.spriteBatch.Draw(instruction.texture, new Vector2(440, 615), Color.White);
            }

            base.Draw();
        }
    }
}
