using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Entities.Pickups
{
    class ArcScale : Pickup
    {
        public ArcScale()
        {
            description = "Arc Scale";
            chance = 100;
            DetermineEffect();
        }

        override public void Init()
        {
            SetTexture("Assets/Pickups/FireRate");
        }

        override protected void Effect()
        {
            player.arcScale.Update(effect);
        }
    }
}

