using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Entity;
using Dojo.Source.Entity.Pickups;

namespace Dojo.Source.Resources
{
    static class PickUps
    {
        public static Pickup ReturnPickup()
        {
            Pickup[] PICKUPS = new Pickup[] { new Damage() };
            Random rand = new Random();
            int pickupNum = rand.Next(0, (PICKUPS.Length-1));

            PICKUPS[pickupNum].Init();

            return PICKUPS[pickupNum];
        }
    }
}
