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
        public float damage = 10;
        private string text;
        private int textTimer;
        private int pushFactor;
        private bool disabled;
        private bool canFire;
        private int wallTimer;
        private bool wallHit;
        public int range;
        public int shotSpeed;

        public Player(PlayerIndex _ID, int _team, int _orientation, ContentManager content, int _x, int _y)
            : base(true, _orientation)
        {
            ID = _ID;
            team = _team;

            range = 300;
            shotSpeed = 5;
            wallHit = false;
            wallTimer = 0;
            canFire = true;
            disabled = false;
            pushFactor = 0;
            textTimer = 0;
            contentMan = content;
            acceleration.X = 4;
            acceleration.Y = 4;
            SetTexture("Assets/PlayerTexture");
            spriteSheet = new SpriteSheet(texture, 40, 50);
            projectiles = new List<Projectile>();
            timer = 0;
            FireRate = 20;
            isFiring = true;
            position.X = _x;
            position.Y = _y;
        }

        public void Disable()
        {
            if (!disabled)
            {
                disabled = true;
            }
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
            canFire = true;
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
                            stamina -= 0.25f;
                            canFire = false;
                            wallHit = true;
                            isFiring = false;
                        }
                    }
                }
                if (wallHit)
                {
                    wallTimer++;
                    if (wallTimer >= 600)
                    {
                        wallTimer = 0;
                        wallHit = false;
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
                            textTimer = 75;
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
            if (!wallHit)
            {
                if (!isFiring && canFire)
                {
                    if (timer >= FireRate)
                    {
                        isFiring = true;
                        timer = 0;
                    }
                    timer++;
                }
            }
            float RightTriggerPull = controller.Triggers.Right;
            if ((RightTriggerPull > 0) && (isFiring))
            {
                stamina -= 2;
                Play.collisionArray.Add(projectile = new Projectile(team, orientation, position.X, position.Y, contentMan, damage, range, shotSpeed));
                isFiring = false;
                
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
                    Vector2 strDim = new Vector2(Formats.arialSmall.MeasureString(text).X, Formats.arialSmall.MeasureString(text).Y);
                    int hX = (int)position.X + (width / 2);
                    int offset = 2;

                    GameManager.spriteBatch.DrawString(Formats.arialSmall, text, new Vector2((hX + offset) - ((int)strDim.X / 2), (position.Y - 40 + offset)), Color.Black);
                    GameManager.spriteBatch.DrawString(Formats.arialSmall, text, new Vector2(hX - ((int)strDim.X / 2), (position.Y - 40)), Color.White);
                    textTimer--;
                }
            }
        }

        public void Update()
        {
            if (!disabled)
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
}
