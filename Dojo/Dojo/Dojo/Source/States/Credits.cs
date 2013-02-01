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
    class Credits : State
    {
        private Sprite devPage;
        private Sprite spcPage;
        private int pageNum;

        private const int DEV_PAGE = 0;
        private const int SPC_PAGE = 1;

        public Credits()
        {
            devPage = new Sprite();
            spcPage = new Sprite();

            pageNum = DEV_PAGE;
        }

        public override void Init()
        {
            devPage.SetTexture("Assets/States/CreditsP1");
            spcPage.SetTexture("Assets/States/CreditsP2");

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
                pageNum++;
            }
            if (Pressed(Buttons.DPadLeft) || Pressed(Buttons.LeftThumbstickRight))
            {
                pageNum--;
            }

            if (pageNum > SPC_PAGE)
            {
                pageNum = DEV_PAGE;
            }
            if (pageNum < DEV_PAGE)
            {
                pageNum = SPC_PAGE;
            }

            if (Pressed(Buttons.B))
            {
                GameManager.SwitchState(StateID.MAIN_MENU);
            }

            base.Update();
        }

        public override void Draw()
        {
            switch (pageNum)
            {
                case DEV_PAGE: GameManager.spriteBatch.Draw(devPage.texture, Vector2.Zero, Color.White); break;
                case SPC_PAGE: GameManager.spriteBatch.Draw(spcPage.texture, Vector2.Zero, Color.White); break;
            }

            base.Draw();
        }
    }
}
