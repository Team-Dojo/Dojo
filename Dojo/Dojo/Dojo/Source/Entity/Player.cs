using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.Engine.Entity;
using Dojo.Source.Engine.Display;

namespace Dojo.Source.Entity
{
    class Player : Dynamic
    {
        public PlayerIndex ID { get; private set; }
        public int group { get; private set; }
        private GamePadState controller;

        public Player(PlayerIndex _ID, int _group, int _orientation, ContentManager content, int X, int Y)
            : base(true, _orientation)
        {
            ID = _ID;
            group = _group;
            acceleration.X = 4;
            acceleration.Y = 4;
            texture = content.Load<Texture2D>("PlayerTexture");
            spriteSheet = new SpriteSheet(texture, 40, 50);
            position.X = X;
            position.Y = Y;
        }

        private void ProcessInput()
        {
            Vector2 leftStick = controller.ThumbSticks.Left;

            speed.X = (leftStick.X * acceleration.X);
            speed.Y = (leftStick.Y * acceleration.Y);
        }

        private void UpdatePosition()
        {
            position.X += speed.X;
            position.Y -= speed.Y;
        }

        public void Update()
        {
            controller = GamePad.GetState(ID);

            if (controller.IsConnected)
            {
                ProcessInput();
                UpdatePosition();
            }
            else
            {
                // Handle controller not connected.
                System.Console.WriteLine("Player " + ID + " controller not connected - please reconnect.");
            }
        }
    }
}
