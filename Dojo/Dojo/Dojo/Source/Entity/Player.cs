using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.Framework.Display;
using Dojo.Source.States;
using System.Timers;

namespace Dojo.Source.Entity
{
    class Player : Dynamic
    {
        public PlayerIndex ID { get; private set; }
        public int team { get; private set; }
        private GamePadState controller;
        public float stamina = 100;
        public float maxStamina = 100;
        public float percentStamina;
        private Projectile projectile;
        private ContentManager contentMan;
        public List<Projectile> projectiles;
        private int timer;
        private int FireRate;
        private bool isFiring;
        public float damage = 5;

        public Player(PlayerIndex _ID, int _team, int _orientation, ContentManager content, int _x, int _y)
            : base(true, _orientation)
        {
            ID = _ID;
            team = _team;

            contentMan = content;
            acceleration.X = 4;
            acceleration.Y = 4;
            SetTexture("Assets/PlayerTexture");
            spriteSheet = new SpriteSheet(texture, 40, 50);
            projectiles = new List<Projectile>();
            timer = 0;
            FireRate = 10;
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

            HandlePosition();
        }

        private void HandlePosition()
        {
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.X + width > Program.baseScreenSize.X)
            {
                position.X = Program.baseScreenSize.X - width;
            }

            if (position.Y < 120)
            {
                position.Y = 120;
            }
            if ((position.Y + height) > Program.baseScreenSize.Y)
            {
                position.Y = (Program.baseScreenSize.Y - height);
            }
        }

        private void HandleCollision()
        {
            List<Sprite> collisionArray = Play.collisionArray;
            for (int i = 0; i < Play.collisionArray.Count; i++)
            {
                if (Play.collisionArray[i] is Projectile)
                {
                    //
                }

                if (Play.collisionArray[i] is Collectables)
                {
                    Collectables collect = (Collectables)Play.collisionArray[i];
                    if(this.HitTestObject(collect))
                    {
                        if (collect.active)
                        {
                            collect.Activate(this);
                            Play.collisionArray.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private void Fire()
        {
            if (timer == FireRate)
            {
                timer = 0;
                isFiring = true;
            }
            float RightTriggerPull = controller.Triggers.Right;
            if ((RightTriggerPull > 0) && (isFiring))
            {
                stamina -= 5;
                Play.collisionArray.Add(projectile = new Projectile(team, orientation, position.X, position.Y, contentMan, damage));
                isFiring = false;
            }
            else 
            { 
                timer++; 
            }
                            
        }

        public bool IsAlive()
        {
            if (stamina < 0)
            {
                return false;
            }
            return true;
        }

        public void DrawProj()
        {
            for (int i = 0; i < Play.collisionArray.Count; i++)
            {
                if (Play.collisionArray.ElementAt(i) is Projectile)
                {
                    Play.collisionArray.ElementAt(i).Draw();
                }
            }
        }

        public void Update()
        {
            HandleCollision();

            percentStamina = (stamina / maxStamina);

            if (IsAlive())
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
                Fire();

                for (int i = 0; i < Play.collisionArray.Count; i++)
                {
                    int or = 0;
                    if (Play.collisionArray.ElementAt(i) is Projectile)
                    {
                        or++;
                        Projectile proj = (Projectile)Play.collisionArray.ElementAt(i);
                        if (proj.IsActive())
                        {
                            proj.Update();

                            if (this.HitTestObject(proj))
                            {
                                if (proj.team != this.team)
                                {
                                    stamina-=proj.damage;
                                    proj.SetActive(false);
                                }
                            }
                        }
                        else
                        {
                            Play.collisionArray.RemoveAt(i);
                        }
                    }

                    System.Console.WriteLine(or);
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
