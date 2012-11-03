using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Engine.Entity;

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

        public Projectile(int _grouping, int _orientation)
            : base(true, _orientation)
        {
            grouping = _grouping;
            speed.X = 10;
        }

        public void Update()
        {
            if (active)
            {
                if (orientation == (int) Orientation.LEFT)
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
                    // Destroy.
                }
            }
        }



    }
}
