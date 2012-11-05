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
        private static Damage DAMAGE = new Damage();

        public static void Init()
        {
            DAMAGE.init();
        }

        //--------------------------------------
        //  Properties
        //--------------------------------------
        //private static const BOUNCE:Bounce = new Bounce();
        //private static const RANGE:Range = new Range();
        //private static const HEART:Heart = new Heart();
        //private static const SPEED:Speed = new Speed();
        //private static const SIZE:Size = new Size();
        //private static const HEALTH:Health = new Health();
        //private static const DAMAGE:Damage = new Damage();
        //private static const FIRE_RATE:FireRate = new FireRate();
        //private static const SHOT_SPEED:ShotSpeed = new ShotSpeed();
        public static Collectables[] PICKUPS = new Collectables[] { DAMAGE };
    }
}
