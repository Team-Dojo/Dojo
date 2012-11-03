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
using System.Timers;

namespace Dojo.Source.Entity
{
    class Player : Dynamic
    {
        public PlayerIndex ID { get; private set; }
        public int group { get; private set; }
        private GamePadState controller;
        private int stamina;
        private Projectile projectile;
        private ContentManager contentMan;
        private List<Projectile> projectiles;
        private int timer;
        private int fireRate;
        private bool isFiring;

        public Player(PlayerIndex _ID, int _group, int _orientation, ContentManager content, int X, int Y)
            : base(true, _orientation)
        {
            ID = _ID;
            group = _group;

            contentMan = content;
            acceleration.X = 4;
            acceleration.Y = 4;
            texture = content.Load<Texture2D>("PlayerTexture");
            spriteSheet = new SpriteSheet(texture, 40, 50);
            stamina = 100;
            projectiles = new List<Projectile>();
            timer = 0;
            fireRate = 30;
            isFiring = true;
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

        private void fire()
        {
            if (timer == fireRate)
            {
                timer = 0;
                isFiring = true;
            }

            if (GamePad.GetState(ID).Buttons.A == ButtonState.Pressed && isFiring)
            {
                stamina -= 5;
                projectiles.Add(projectile = new Projectile(group, orientation, position.X, position.Y, contentMan));
                isFiring = false;
            }
            else { timer++; }
                            
        }

        private bool isAlive()
        {
            if (stamina < 0)
            {
                return false;
            }
            return true;
        }

        public void DrawProj()
        {
            if (projectiles != null)
            {
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles.ElementAt(i).Draw();
                }
            }
        }

        public void Update()
        {
            
            controller = GamePad.GetState(ID);

            if (controller.IsConnected)
            {
                ProcessInput();
                UpdatePosition();
                fire();

                if (projectile != null)
                {   
                    for (int i = 0; i < projectiles.Count; i++)
                    {
                        if (projectiles.ElementAt(i).isActive())
                        {
                            projectiles.ElementAt(i).Update();
                        }
                        else
                        {
                            projectiles.RemoveAt(i);
                        }
                    } 
                    
                }
              
            }
            else
            {
                // Handle controller not connected.
                System.Console.WriteLine("Player " + ID + " controller not connected - please reconnect.");
            }
        }
    }
}
