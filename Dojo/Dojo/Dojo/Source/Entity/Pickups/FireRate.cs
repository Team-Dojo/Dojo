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
			DetermineEffect(-5, 25);
		}
        override public void Init() 
        {
            SetTexture("Assets/Pickups/FireRate");
        }
		override protected void Effect()
		{
            player.FireRate += effect;
		}
    }
}

