using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.Display;
using Dojo.Source.States;
using Dojo.Source.Resources;

namespace Dojo.Source.Entities
{
    class Court
    {
        private Sprite field;
        public enum Type { RED, BLUE };
        private int type;

        public Court(int type)
        {
            this.type = type;
            field = new Sprite((int)Sprite.Orientation.NONE);
        }

        public void Init()
        {
            switch (type)
            {
                case (int)Type.RED:
                    field.SetTexture("Assets/RedCourt");
                    field.sourceRect = new Rectangle(0, 0, (int)Play.wall.position.X, 600);
                    break;

                case (int)Type.BLUE:
                    field.SetTexture("Assets/BlueCourt");
                    field.sourceRect = new Rectangle((int)Play.wall.position.X + Play.wall.width, 0, (Program.SCREEN_WIDTH - (int)Play.wall.position.X + Play.wall.width), 600);
                    break;
            }
        }

        public void Update()
        {
            switch (type)
            {
                case (int)Type.RED:
                    field.sourceRect = new Rectangle(0, 0, (int)Play.wall.position.X, 600);
                    break;

                case (int)Type.BLUE:
                    field.sourceRect = new Rectangle((int)Play.wall.position.X + Play.wall.width, 0, (Program.SCREEN_WIDTH - (int)Play.wall.position.X), 600);
                    break;
            }
        }

        public void Draw()
        {
            GameManager.spriteBatch.Draw(field.texture, new Vector2(field.sourceRect.X, 120), field.sourceRect, Color.White);
        }
    }
}
