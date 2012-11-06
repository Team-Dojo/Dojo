using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Framework.Display;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.States;

namespace Dojo.Source.Entity
{
    class Projectile : Dynamic
    {
        public int team;
        private bool active = true;
        private int range;
        private int distanceTravelled;

        public float damage { get; private set; }

        public Projectile(int _team, int _orientation, float _x, float _y,  ContentManager content, float _damage)
            : base(true, _orientation)
        {
            damage = _damage;
            team = _team;
            speed.X = 10;
            range = 300;
            position.X = _x;
            position.Y = _y;
            SetTexture("Assets/Projectiles/Shuriken");            
            if (orientation == (int)Orientation.LEFT)
            {
                speed.X *= -1;
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
                //rotation += 2f;
                if (distanceTravelled < range)
                {
                    position.X += speed.X;
                    if (speed.X < 0)
                    {
                        distanceTravelled -= (int)speed.X;
                    }
                    else
                    {
                        distanceTravelled += (int)speed.X;
                    }
                }
                else
                {
                    active = false;
                }
            }
        }



    }
}
