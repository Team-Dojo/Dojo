using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.Display;
using Dojo.Source.States;
using Dojo.Source.Resources;
using Dojo.Source.Data;
using Dojo.Source.Entities.Abilities;

namespace Dojo.Source.Entities
{
    class Player : GameObject
    {
        public PlayerIndex ID { get; private set; }
        public int team { get; private set; }
        private GamePadState controller;
        public List<Projectile> projectiles;
        public Vector2 previousPosition;
        public float shotDir = 0.0f;
        public bool piercing;
        public bool boomerang;
        public bool arc;
        public bool invincible;

        private const float STAMINA_REGEN = 0.05f;

        public Stat damage = new Stat(4, 2.5f);         // INI: 10, MAX: 25, MIN: 2.5
        //public Stat maxStamina = new Stat(4, 25);     // INI: 100, MAX: 250, MIN: 25
        public Stat fireRate = new Stat(5, 5);          // INI: 25, MAX: 50, MIN: 5
        public Stat range = new Stat(5, 100);           // INI: 300, MAX: 1000, MIN: 100
        public Stat shotSpeed = new Stat(2, 2.5f);      // INI: 5, MAX: 25, MIN: 2.5
        public Stat maxSpeedX = new Stat(2, 2.5f);      // INI: 5, MAX: 25, MIN: 2.5
        public Stat maxSpeedY = new Stat(2, 2.5f);      // INI: 5, MAX: 25, MIN: 2.5
        public Stat arcScale = new Stat(5, 1);          // INI: 3, MAX: 10, MIN: 1
        public Stat pushSpeed = new Stat(5, 0.2f);      // INI: 1.0, MAX: 2, MIN: 0.2

        public List<Ability> abilities;
        public Invisibility invisibility;
        public Wildfire wildfire;
        public Illusion illusion;
        public Invincibility invinciblity;

        private string text;
        private int textTimer;
        private int timer;
        private bool disabled;
        private bool canFire;
        private int wallTimer;
        private bool wallHit;
        private float staminaUsed;

        public Player(PlayerIndex ID, int team, int orientation, Vector2 position)
            : base(true, orientation)
        {
            invisibility = new Invisibility();
            wildfire = new Wildfire();
            illusion = new Illusion();
            invinciblity = new Invincibility();

            abilities = new List<Ability>();
            abilities.Add(invisibility);
            abilities.Add(wildfire);
            abilities.Add(illusion);
            abilities.Add(invinciblity);

            piercing = false;
            boomerang = false;
            arc = false;
            invincible = false;

            this.ID = ID;
            this.team = team;
            this.position = position;

            //stamina = maxStamina.value;
            previousPosition = position;
            wallHit = false;
            wallTimer = 0;
            canFire = true;
            disabled = false;
            textTimer = 0;
            acceleration.X = 4;
            acceleration.Y = 4;
            spriteSheet = new SpriteSheet(texture, 40, 50);
            projectiles = new List<Projectile>();
            timer = 0;
        }

        public void AlterStamina(ref float teamStamina)
        {
            if (!invincible)
                teamStamina += staminaUsed;
            else
                teamStamina += STAMINA_REGEN;
        }

        public void Load()
        {
            if (team == Global.RED_TEAM)
                SetTexture("Assets/RedPlayer");
            else if (team == Global.BLUE_TEAM)
                SetTexture("Assets/BluePlayer");
            else
                System.Console.WriteLine("LOADING PLAYER IMAGE FAILED: NO TEAM ASSIGNED!");
        }

        public void Disable()
        {
            if (!disabled)
            {
                disabled = true;
            }
        }

        public void TimedText(string text)
        {
            textTimer = 75;
            this.text = text;
        }

        public void UpdateAcceleration()
        {
            acceleration = new Vector2((maxSpeedX.value / 2.5f), (maxSpeedY.value / 2.5f));
        }

        private void ProcessInput()
        {
            Vector2 leftStick = controller.ThumbSticks.Left;
            float rightTrigger = controller.Triggers.Right;

            speed.X = (leftStick.X * maxSpeedX.value);
            speed.Y = (leftStick.Y * maxSpeedY.value);

            if ((rightTrigger > 0) && (canFire))
            {
                Fire();
            }
        }

        private void UpdatePosition()
        {
            previousPosition = position;

            position.X += speed.X;
            position.Y -= speed.Y;

            KeepInBounds();
        }

        private void KeepInBounds()
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

        private void HandleCollision(GameObject obj, int index)
        {
            bool collidable = obj.collidable;
            
            if (obj.name == "wall")
            {
                if (team == Global.RED_TEAM)
                    obj.position.X += pushSpeed.value;
                else
                    obj.position.X -= pushSpeed.value;

                staminaUsed -= 0.25f;
                wallHit = true;
                canFire = false;
            }
            
            if (obj is Projectile)
            {
                Projectile proj = (Projectile)obj;

                if (proj.team != this.team)
                {
                    if (!proj.piercing)
                    {
                        proj.SetActive(false);
                        staminaUsed -= proj.damage;
                    }
                    else
                    {
                        staminaUsed -= (proj.damage / (damage.level * 3));
                    }
                }
            }

            if (obj is Pickup)
            {
                Pickup pickup = (Pickup)obj;
                if (pickup.active)
                {
                    pickup.Activate(this);
                    TimedText(pickup.description);
                    pickup.Destroy();
                    Play.collisionArray.RemoveAt(index);
                    Play.pickupManager.RemovePickup();
                }
            }

            if (collidable)
            {
                // Please note these are approximations
                bool above = false;
                bool below = false;

                // Above object
                if ((previousPosition.Y + height + speed.Y) <= obj.position.Y)
                {
                    position.Y = obj.position.Y - height;
                    above = true;
                    speed.Y = 0;
                }

                // Below object
                if ((previousPosition.Y + speed.Y) >= (obj.position.Y + obj.height))
                {
                    position.Y = obj.position.Y + obj.height;
                    speed.Y = 0;
                    below = true;
                }

                if (!above && !below)
                {
                    // Left of object
                    if ((previousPosition.X - speed.X) < obj.position.X)
                    {
                        position.X = obj.position.X - width;
                        speed.X = 0;
                    }

                    // Right of object
                    if ((previousPosition.X - speed.X) > obj.position.X)
                    {
                        position.X = obj.position.X + obj.width;
                        speed.X = 0;
                    }
                }
            }
        }

        private void Fire()
        {
            if (!wallHit)
            {
                staminaUsed -= 2;

                Vector2 spawn = new Vector2();

                switch (team)
                {
                    case Global.RED_TEAM:   spawn = new Vector2(position.X + 50.0f, position.Y + 20.0f); break;
                    case Global.BLUE_TEAM:  spawn = new Vector2(position.X - 20.0f, position.Y + 20.0f); break;
                }

                Play.collisionArray.Add(new Projectile( team, orientation, spawn, damage.value, range.value, new Vector2(shotSpeed.value, shotDir), 
                                                        piercing, boomerang, arc, arcScale.value));
                canFire = false;
            }
        }

        public bool IsAlive()
        {
            if (staminaUsed <= 0)
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

            for (int i = 0; i < abilities.Count; i++)
            {
                if (abilities.ElementAt(i).draw)
                {
                    abilities.ElementAt(i).Draw();
                }
            }
        }

        public void Update()
        {
            if (!disabled)
            {
                staminaUsed = 0;

                controller = GamePad.GetState(ID);

                // Handles firing
                if (timer >= fireRate.value)
                {
                    canFire = true;
                    timer = 0;
                }
                timer++;

                // Handles firing after colliding with the wall
                if (wallHit)
                {
                    wallTimer++;
                    if (wallTimer >= 100)
                    {
                        wallTimer = 0;
                        wallHit = false;
                    }
                }

                ProcessInput();

                staminaUsed += STAMINA_REGEN;

                // Update projectiles
                for (int i = 0; i < Play.collisionArray.Count; i++)
                {
                    if (Play.collisionArray[i] is Projectile)
                    {
                        Projectile proj = (Projectile)Play.collisionArray[i];
                        if (proj.IsActive())
                        {
                            proj.Update();
                        }
                        else
                        {
                            Play.collisionArray.RemoveAt(i);
                        }
                    }
                }
            }

            UpdatePosition();

            for (int i = 0; i < abilities.Count; i++)
            {
                abilities.ElementAt(i).Update();
            }

            // Check for collisions and handle if collision detected
            for (int i = 0; i < Play.collisionArray.Count; i++)
            {
                if (this != Play.collisionArray[i])
                {
                    if (this.HitTestObject(Play.collisionArray[i]))
                    {
                        HandleCollision(Play.collisionArray[i], i);
                    }
                }
            }
        }
    }
}
