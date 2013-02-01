using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class Piercing : Pickup
    {
        public Piercing()
        {
            description = "Piercing Shots";
            chance = 100;
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/Speed");
        }

        override protected void Effect()
        {
            player.piercing = !player.piercing;
            BoolEffect(player.piercing);
        }
    }
}
