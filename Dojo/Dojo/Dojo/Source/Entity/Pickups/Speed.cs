using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entity.Pickups
{
    class Speed : Pickup
    {
        //--------------------------------------
		//  Constructor
		//--------------------------------------
		public Speed()
		{
			description = "Speed";
			chance = 100;
			DetermineEffect(-5, 10);
		}
        override public void Init() 
        {
            SetTexture("Assets/Pickups/Speed");
        }
		override protected void Effect()
		{
            player.acceleration.X += effect;
            player.acceleration.Y += effect;
		}
    }
}
