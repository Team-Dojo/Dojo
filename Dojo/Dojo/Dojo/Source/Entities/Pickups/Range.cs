using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class Range : Pickup
    {
        public Range()
		{
			description = "Range";
			chance = 100;
			DetermineEffect();
		}

        override public void Init() 
        {
            SetTexture("Assets/Pickups/Range");
        }

		override protected void Effect()
		{
            player.range.Update(effect);
        }
    }
}
