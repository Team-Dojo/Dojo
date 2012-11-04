using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.Framework.Entity;
using Dojo.Source.Framework.Display;
using System.Timers;

namespace Dojo.Source.Entity
{
    class Player : Dynamic
    {
        public PlayerIndex ID { get; private set; }
        public int team { get; private set; }
        private GamePadState controller;
        public float stamina;
        private Projectile projectile;
        private ContentManager contentMan;
        private List<Projectile> projectiles;
        private int timer;
        private int fireRate;
        private bool isFiring;

        public Player(PlayerIndex _ID, int _team, int _orientation, ContentManager content, int _x, int _y)
            : base(true, _orientation)
        {
            ID = _ID;
            team = _team;

            contentMan = content;
            acceleration.X = 4;
            acceleration.Y = 4;
            SetTexture("PlayerTexture");
            spriteSheet = new SpriteSheet(texture, 40, 50);
            stamina = 100;
            projectiles = new List<Projectile>();
            timer = 0;
            fireRate = 30;
            isFiring = true;
            position.X = _x;
            position.Y = _y;
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

            if ((GamePad.GetState(ID).Buttons.A == ButtonState.Pressed) && (isFiring))
            {
                stamina -= 5;
                projectiles.Add(projectile = new Projectile(team, orientation, position.X, position.Y, contentMan));
                isFiring = false;
            }
            else 
            { 
                timer++; 
            }
                            
        }

        public bool isAlive()
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
            if (isAlive())
            {
                stamina += 0.05f;
                if (stamina >= 100)
                {
                    stamina = 100;
                }
            }

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
