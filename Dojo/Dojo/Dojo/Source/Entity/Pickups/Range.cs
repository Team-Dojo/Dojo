﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entity.Pickups
{
    class Range : Pickup
    {
        //--------------------------------------
		//  Constructor
		//--------------------------------------
        public Range()
		{
			description = "Range";
			chance = 100;
			DetermineEffect(-50, 300);
		}
        override public void Init() 
        {
            SetTexture("Assets/Pickups/Range");
        }
		override protected void Effect()
		{
            if (player.projectile != null)
            {
                if (player.projectile.range + effect > 100)
                {
                    player.projectile.range += effect;
                }
                else
                {
                    player.projectile.range = 100;
                }
            }
		}
    }
}