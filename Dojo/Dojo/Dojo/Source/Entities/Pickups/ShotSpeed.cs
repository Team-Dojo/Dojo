using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class ShotSpeed : Pickup
    {
        public ShotSpeed()
        {
            description = "Shot Speed";
            chance = 100;
            DetermineEffect();
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/ShotSpeed");
        }

        override protected void Effect()
        {
            player.shotSpeed.Update(effect);
        }
    }
}
