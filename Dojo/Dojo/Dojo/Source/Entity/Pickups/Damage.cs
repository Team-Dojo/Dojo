using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entity.Pickups
{
    class Damage : Pickup
    {
		public Damage()
		{
			description = "Damage";
			chance = 100;
			DetermineEffect(-2, 10);
		}

        override public void Init() 
        {
            SetTexture("Assets/Pickups/Damage");
        }

		override protected void Effect()
		{
            if (player.damage + effect > 5)
            {
                player.damage += effect;
            }
            else
            {
                player.damage = 5;
            }
		}
    }
}
