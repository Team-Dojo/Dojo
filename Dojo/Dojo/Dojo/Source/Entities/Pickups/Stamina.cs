using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class Stamina : Pickup
    {
        public Stamina()
        {
            description = "Stamina";
            chance = 100;
            DetermineEffect();
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/Stamina");
        }

        override protected void Effect()
        {
            //player.maxStamina.Update(effect);
        }
    }
}