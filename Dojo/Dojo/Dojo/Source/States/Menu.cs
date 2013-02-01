using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Dojo.Source.Display;
using Dojo.Source.UI;
using Dojo.Source.Resources;

namespace Dojo.Source.States
{
    class Menu : State
    {
        private Sprite screen;
        private Sprite play, credits, quit;
        private int cursorPosition;
        private Vector2 menuPosition;

        private const int PLAY = 0;
        private const int CREDITS = 1;
        private const int QUIT = 2;

        public Menu()
        {
            screen = new Sprite();
            play = new Sprite();
            credits = new Sprite();
            quit = new Sprite();
            cursorPosition = PLAY;
            menuPosition = new Vector2(395, 410);
        }

        public override void Init()
        {
            screen.SetTexture("Assets/States/Splash");
            play.SetTexture("Assets/States/MenuPlay");
            credits.SetTexture("Assets/States/MenuCredits");
            quit.SetTexture("Assets/States/MenuQuit");

            base.Init();
        }

        public override void Load()
        {
            base.Load();
        }

        public override void Update()
        {
            if (Pressed(Buttons.DPadRight) || Pressed(Buttons.LeftThumbstickRight))
            {
                cursorPosition++;
            }
            if (Pressed(Buttons.DPadLeft) || Pressed(Buttons.LeftThumbstickLeft))
            {
                cursorPosition--;
            }
            if (cursorPosition > QUIT)
            {
                cursorPosition = PLAY;
            }
            if (cursorPosition < PLAY)
            {
                cursorPosition = QUIT;
            }

            // Switch state
            if (Pressed(Buttons.A))
            {
                switch (cursorPosition)
                {
                    case PLAY: 
                        GameManager.SwitchState(StateID.SELECT);
                        break;

                    case CREDITS:
                        GameManager.SwitchState(StateID.CREDITS);
                        break;

                    case QUIT:
                        exit = true;
                        break;
                }
            }       
   
            base.Update();
        }

        public override void Draw()
        {
            GameManager.spriteBatch.Draw(screen.texture, Vector2.Zero, Color.White);

            switch (cursorPosition)
            {
                case PLAY:
                    GameManager.spriteBatch.Draw(play.texture, menuPosition, Color.White);
                    break;

                case CREDITS:
                    GameManager.spriteBatch.Draw(credits.texture, menuPosition, Color.White);
                    break;

                case QUIT:
                    GameManager.spriteBatch.Draw(quit.texture, menuPosition, Color.White);
                    break;
            }
          
            base.Draw();
        }
    }
}
