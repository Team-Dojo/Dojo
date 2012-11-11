using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entity.Pickups
{
    class ShotSpeed : Pickup
    {
        public ShotSpeed()
        {
            description = "Shot Speed";
            chance = 100;
            DetermineEffect(-1, 3);
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/ShotSpeed");
        }

        override protected void Effect()
        {
            if (player.damage + effect > 5)
            {
                player.damage += effect;
            }
            else
            {
                player.damage = 5;
            }
        }
    }
}
