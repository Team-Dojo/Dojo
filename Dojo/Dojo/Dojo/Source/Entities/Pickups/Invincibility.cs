using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class Invincibility : Pickup
    {
        public Invincibility()
        {
            description = "Invincibility";
            chance = 100;
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/Damage");
        }

        override protected void Effect()
        {
            player.invincible = !player.invincible;
            BoolEffect(player.invincible);
        }
    }
}
