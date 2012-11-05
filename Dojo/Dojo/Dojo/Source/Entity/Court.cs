using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Framework.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.States;
using Dojo.Source.Resources;

namespace Dojo.Source.Entity
{
    class Court
    {
        private Sprite field = new Sprite((int)Sprite.Orientation.NONE);
        public enum Type { RED, BLUE };
        private int type;
        private static int MAX_COLLECTABLES = 5;
        private Collectables[] pickUps = new Collectables[MAX_COLLECTABLES];

        public Court(int _type)
        {
            type = _type;
        }

        public void Init()
        {
            PickUps.Init();

            pickUps[0] = PickUps.PICKUPS[0];
            pickUps[0].position.X = 600;
            pickUps[0].position.Y = 600;

            Play.collisionArray.Add(pickUps[0]);

            switch (type)
            {
                case (int)Type.RED:
                    field.SetTexture("Assets/RedCourt");
                    field.sourceRect = new Rectangle(0, 0, (int)Play.wall.position.X, 600);
                    break;

                case (int)Type.BLUE:
                    field.SetTexture("Assets/BlueCourt");
                    field.sourceRect = new Rectangle((int)Play.wall.position.X + Play.wall.width, 0, ((int)Program.baseScreenSize.X - (int)Play.wall.position.X), 600);
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
                    field.sourceRect = new Rectangle((int)Play.wall.position.X + Play.wall.width, 0, ((int)Program.baseScreenSize.X - (int)Play.wall.position.X), 600);
                    break;
            }
        }

        public void Draw()
        {
            GameManager.spriteBatch.Draw(field.texture, new Vector2(field.sourceRect.X, 120), field.sourceRect, Color.White);

            for (int i = 0; i < MAX_COLLECTABLES; i++)
            {
                if (pickUps[i] != null)
                {
                    pickUps[i].Draw();
                }
            }
        }
    }
}
