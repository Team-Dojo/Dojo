using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entity.Pickups
{
    class Damage:Collectables
    {
        //--------------------------------------
		//  Constructor
		//--------------------------------------
		public Damage()
		{
			description = "Damage";
			chance = 100;
            visible = true;
			DetermineEffect();
		}
        public void init() 
        {
            SetTexture("Assets/Shuriken");
        }
		override protected void Effect()
		{
            player.damage += effect;
		}
    }
}
