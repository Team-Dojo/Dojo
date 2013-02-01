using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.States;
using Dojo.Source.Resources;

namespace Dojo.Source.Entities
{
    class Projectile : GameObject
    {
        public int team;
        public float range;
        public float distanceTravelled { get; private set; }
        public float damage { get; private set; }
        private bool active = true;

        public bool piercing;
        public bool boomerang;
        public bool arc;
        public bool arcDirUp;

        private const float ARC_ACCELERATION = 0.5f;

        public float arcSpeed = 0.0f;

        public Projectile(  int team, int orientation, Vector2 position, float damage, 
                            float range, Vector2 speed, bool piercing, bool boomerang,
                            bool arc, float arcSpeed)

            : base(true, orientation)
        {
            collidable = false;

            arcDirUp = true;
            this.arcSpeed = arcSpeed;

            this.arc = arc;
            this.damage = damage;
            this.team = team;
            this.speed.X = speed.X;
            this.speed.Y = speed.Y;
            this.range = range;
            this.position = position;
            this.piercing = piercing;
            this.boomerang = boomerang;

            switch (team)
            {
                case Global.RED_TEAM: SetTexture("Assets/Projectiles/RedShot"); break;
                case Global.BLUE_TEAM: SetTexture("Assets/Projectiles/BlueShot"); break;
            }
                     
            if (orientation == (int)Orientation.LEFT)
            {
                this.speed.X *= -1;
            }
        }

        public void SetActive(bool state)
        {
            active = state;
        }

        public bool IsActive()
        {
            return active;
        }

        public void Update()
        {
            if (active)
            {
                if (arc)
                {
                    if (arcDirUp)
                    {
                        if (speed.Y < arcSpeed)
                            speed.Y += ARC_ACCELERATION;
                        else
                            arcDirUp = false;
                    }
                    else
                    {
                        if (speed.Y > -arcSpeed)
                            speed.Y -= ARC_ACCELERATION;
                        else
                            arcDirUp = true;
                    }
                }

                if (distanceTravelled < range)
                {
                    position.X += speed.X;
                    position.Y += speed.Y;

                    if (speed.X < 0)
                        distanceTravelled -= (int)speed.X;
                    else
                        distanceTravelled += (int)speed.X;
                }
                else
                {
                    if (boomerang)
                    {
                        distanceTravelled = 0;
                        speed.X *= -1;
                        boomerang = false;
                    }
                    else
                    {
                        active = false;
                    }
                }
            }
        }
    }
}
