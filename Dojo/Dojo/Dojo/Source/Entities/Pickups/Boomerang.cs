using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class Boomerang : Pickup
    {
        public Boomerang()
        {
            description = "Boomerang Shots";
            chance = 100;
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/Speed");
        }

        override protected void Effect()
        {
            player.boomerang = !player.boomerang;
            BoolEffect(player.boomerang);
        }
    }
}
