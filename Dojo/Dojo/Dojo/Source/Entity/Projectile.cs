using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Engine.Entity;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dojo.Source.Entity
{
    class Projectile : Dynamic
    {
        private int grouping;
        private bool active = true;
        private int range;
        private int distanceTravelled;
        private int bounces;

        public int damage { get; private set; }

        public Projectile(int _grouping, int _orientation, float _x, float _y,  ContentManager content)
            : base(true, _orientation)
        {
            grouping = _grouping;
            speed.X = 10;
            range = 500;
            position.X = _x;
            position.Y = _y;
            texture = content.Load<Texture2D>("Projectile");

        }

        public bool isActive()
        {
            return active;
        }

        public void Update()
        {
            if (active)
            {
                if (orientation == LEFT)
                {
                    speed.X *= -1;
                }

                if (distanceTravelled < range)
                {
                    position.X += speed.X;
                    distanceTravelled += (int)speed.X;
                    
                }
                else
                {
                    active = false;
                }
            }
        }



    }
}
