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
    class Select : State
    {
        private Sprite vs1;
        private Sprite vs2;
        private int choice;

        private const int VS1 = 0;
        private const int VS2 = 1;

        public Select()
        {
            vs1 = new Sprite();
            vs2 = new Sprite();

            choice = VS1;
        }

        public override void Init()
        {
            vs1.SetTexture("Assets/States/PlayerSelect1");
            vs2.SetTexture("Assets/States/PlayerSelect2");

            base.Init();
        }

        public override void Load()
        {
            base.Load();
        }

        public override void Update()
        {
            if (Pressed(Buttons.DPadRight) || Pressed(Buttons.LeftThumbstickLeft))
            {
                choice++;
            }
            if (Pressed(Buttons.DPadLeft) || Pressed(Buttons.LeftThumbstickRight))
            {
                choice--;
            }

            if (choice > VS2)
            {
                choice = VS1;
            }
            if (choice < VS1)
            {
                choice = VS2;
            }

            if (Pressed(Buttons.B))
            {
                GameManager.SwitchState(StateID.MAIN_MENU);
            }

            if (Pressed(Buttons.A))
            {
                switch (choice)
                {
                    case VS1: args = 1; break;
                    case VS2: args = 2; break;
                }

                GameManager.SwitchState(StateID.PLAY);
            }

            base.Update();
        }

        public override void Draw()
        {
            switch (choice)
            {
                case VS1: GameManager.spriteBatch.Draw(vs1.texture, Vector2.Zero, Color.White); break;
                case VS2: GameManager.spriteBatch.Draw(vs2.texture, Vector2.Zero, Color.White); break;
            }

            base.Draw();
        }
    }
}
