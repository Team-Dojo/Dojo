using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entity.Pickups
{
    class Stamina : Pickup
    {
        //--------------------------------------
        //  Constructor
        //--------------------------------------
        public Stamina()
        {
            description = "Stamina";
            chance = 100;
            DetermineEffect(-20, 20);
        }
        override public void Init()
        {
            SetTexture("Assets/Pickups/Stamina");
        }
        override protected void Effect()
        {
            player.stamina += effect;
        }
    }
}