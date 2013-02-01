using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class Invisibility : Pickup
    {
        public Invisibility()
        {
            description = "Invisibility";
            chance = 100;
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/Range");
        }

        override protected void Effect()
        {
            player.invisibility.Trigger(player, 500);
        }
    }
}
