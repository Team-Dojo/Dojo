using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class FireRate : Pickup
    {
		public FireRate()
		{
			description = "Fire Rate";
			chance = 100;
			DetermineEffect();
		}

        override public void Init() 
        {
            SetTexture("Assets/Pickups/FireRate");
        }

		override protected void Effect()
		{
            player.fireRate.Update(effect);
		}
    }
}

