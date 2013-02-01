using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class Arc : Pickup
    {
        public Arc()
        {
            description = "Arcing Shots";
            chance = 100;
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/Speed");
        }

        override protected void Effect()
        {
            player.arc = !player.arc;
            BoolEffect(player.arc);
        }
    }
}
