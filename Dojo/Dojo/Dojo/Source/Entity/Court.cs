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
        private static int MAX_PICKUPS = 5;
        private Pickup[] pickups = new Pickup[MAX_PICKUPS];
        private int pickupCount = 0;
        private int timer = 0;
        private int spawnTime = 100;

        public Court(int _type)
        {
            type = _type;
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
                    field.sourceRect = new Rectangle((int)Play.wall.position.X + Play.wall.width, 0, ((int)Program.baseScreenSize.X - (int)Play.wall.position.X), 600);
                    break;
            }
        }

        public void Update()
        {
            if (timer == spawnTime)
            {
                if (pickupCount != MAX_PICKUPS)
                {
                    pickupCount++;
                    int i = pickupCount - 1;
                    Random rand = new Random();
                    int x = rand.Next(field.sourceRect.X, field.sourceRect.Width);
                    int y = rand.Next(120, 720);

                    //System.Console.WriteLine(PickUps.PICKUPS[0].ToString());
                    pickups[i] = PickUps.ReturnPickup();
                    pickups[i].position.X = x;
                    pickups[i].position.Y = y;
                    Play.collisionArray.Add(pickups[i]);
                }
                timer = 0;
            }

            switch (type)
            {
                case (int)Type.RED:
                    field.sourceRect = new Rectangle(0, 0, (int)Play.wall.position.X, 600);
                    break;

                case (int)Type.BLUE:
                    field.sourceRect = new Rectangle((int)Play.wall.position.X + Play.wall.width, 0, ((int)Program.baseScreenSize.X - (int)Play.wall.position.X), 600);
                    break;
            }

            timer++;
        }

        public void Draw()
        {
            GameManager.spriteBatch.Draw(field.texture, new Vector2(field.sourceRect.X, 120), field.sourceRect, Color.White);

            for (int i = 0; i < pickupCount; i++)
            {
                pickups[i].Draw();
            }
        }
    }
}
