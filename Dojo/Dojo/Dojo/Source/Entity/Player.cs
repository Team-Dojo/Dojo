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
using Dojo.Source.Resources;

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
        public Projectile projectile;
        private ContentManager contentMan;
        public List<Projectile> projectiles;
        private int timer;
        public int FireRate;
        private bool isFiring;
        public float damage = 5;
        private string text;
        private int textTimer;
        private int pushFactor;

        public Player(PlayerIndex _ID, int _team, int _orientation, ContentManager content, int _x, int _y)
            : base(true, _orientation)
        {
            ID = _ID;
            team = _team;

            pushFactor = 0;
            textTimer = 0;
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
            if (position.X + width > Program.SCREEN_WIDTH)
            {
                position.X = (Program.SCREEN_WIDTH - width);
            }

            if (position.Y < 120)
            {
                position.Y = 120;
            }
            if ((position.Y + height) > Program.SCREEN_HEIGHT)
            {
                position.Y = (Program.SCREEN_HEIGHT - height);
            }
        }

        private void HandleCollision()
        {
            List<Sprite> collisionArray = Play.collisionArray;
            for (int i = 0; i < collisionArray.Count; i++)
            {
                if (collisionArray[i].name == "wall")
                {
                    if (HitTestObject(collisionArray[i]))
                    {
                        switch (team)
                        {
                            case Ref.TEAM_ONE:
                                pushFactor = 1;
                                break;

                            case Ref.TEAM_TWO:
                                pushFactor = -1;
                                break;
                        }
                        if (controller.IsButtonDown(Buttons.RightShoulder))
                        {
                            collisionArray[i].position.X += pushFactor;
                        }
                    }
                }

                if (collisionArray[i] is Projectile)
                {
                    if (collisionArray.ElementAt(i) is Projectile)
                    {
                        Projectile proj = (Projectile)collisionArray.ElementAt(i);
                        if (proj.IsActive())
                        {
                            proj.Update();

                            if (this.HitTestObject(proj))
                            {
                                if (proj.team != this.team)
                                {
                                    stamina -= proj.damage;
                                    proj.SetActive(false);
                                }
                            }
                        }
                        else
                        {
                            collisionArray.RemoveAt(i);
                            i--;
                        }
                    }
                }

                if (collisionArray[i] is Pickup)
                {
                    Pickup collect = (Pickup)collisionArray[i];
                    if (this.HitTestObject(collect))
                    {
                        if (collect.active)
                        {
                            text = collect.description;
                            textTimer = 50;
                            collect.Activate(this);
                            collisionArray.RemoveAt(i);
                            Play.pickupManager.RemovePickup();
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
                stamina -= 1;
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
            if (stamina <= 0)
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
            if (text != null)
            {
                if (textTimer != 0)
                {
                    GameManager.spriteBatch.DrawString(Formats.arialSmall, text, new Vector2(position.X+2, (position.Y - 40+2)), Color.Black);
                    GameManager.spriteBatch.DrawString(Formats.arialSmall, text, new Vector2(position.X, (position.Y - 40)), Color.White);
                    textTimer--;
                }
            }
        }

        public void Update()
        {
            controller = GamePad.GetState(ID);

            HandleCollision();
            ProcessInput();
            UpdatePosition();
            Fire();

            if (IsAlive())
            {
                stamina += 0.05f;
                
            }

            percentStamina = (stamina / maxStamina);

            if (stamina >= 100)
            {
                stamina = 100;
            }
            if (stamina < 0)
            {
                stamina = 0;
            }
        }
    }
}
