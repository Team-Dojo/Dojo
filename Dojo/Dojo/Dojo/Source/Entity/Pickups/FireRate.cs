using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entity.Pickups
{
    class FireRate : Pickup
    {
         //--------------------------------------
		//  Constructor
		//--------------------------------------
		public FireRate()
		{
			description = "Fire Rate";
			chance = 100;
			DetermineEffect(-10, 1);
		}
        override public void Init() 
        {
            SetTexture("Assets/Pickups/FireRate");
        }
		override protected void Effect()
		{
            if ((player.FireRate + effect > 0) && (player.FireRate + effect < 100))
            {
                player.FireRate += effect;
            }
            else
            {
                player.FireRate = 1;
            }
		}
    }
}

