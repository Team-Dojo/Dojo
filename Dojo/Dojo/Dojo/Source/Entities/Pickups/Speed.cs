using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class Speed : Pickup
    {
		public Speed()
		{
			description = "Speed";
			chance = 100;
			DetermineEffect();
		}

        override public void Init() 
        {
            SetTexture("Assets/Pickups/Speed");
        }

		override protected void Effect()
		{
            player.maxSpeedX.Update(effect);
            player.maxSpeedY.Update(effect);
            player.UpdateAcceleration();
		}
    }
}
