using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class Wildfire : Pickup
    {
        public Wildfire()
        {
            description = "Wildfire";
            chance = 100;
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/Stamina");
        }

        override protected void Effect()
        {
            player.wildfire.Trigger(player, 1000);
        }
    }
}
