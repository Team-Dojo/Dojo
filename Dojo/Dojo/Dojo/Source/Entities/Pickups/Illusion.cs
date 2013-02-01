using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class Illusion : Pickup
    {
        public Illusion()
        {
            description = "Illusion";
            chance = 100;
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/Stamina");
        }

        override protected void Effect()
        {
            player.illusion.Trigger(player, 500);
        }
    }
}
