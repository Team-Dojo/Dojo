using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class PushSpeed : Pickup
    {
        public PushSpeed()
        {
            description = "Push Speed";
            chance = 100;
            DetermineEffect();
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/FireRate");
        }

        override protected void Effect()
        {
            player.pushSpeed.Update(effect);
        }
    }
}

