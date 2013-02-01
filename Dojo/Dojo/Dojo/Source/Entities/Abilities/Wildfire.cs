using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Data;

namespace Dojo.Source.Entities.Abilities
{
    class Wildfire : Ability
    {
        private int prevLevel;
        private int newLevel;

        public Wildfire()
        {
        }

        protected override void Init()
        {
            prevLevel = player.fireRate.level;
            player.fireRate.SetLevel(1); // Fastest
            base.Init();
        }

        override protected void Effect()
        {
            Random rand = new Random();
            player.shotDir = (float)rand.NextDouble() * rand.Next(-5, 5);
        }

        override protected void Destroy()
        {
            player.shotDir = 0;
            player.fireRate.SetLevel(prevLevel);
            player.TimedText("Wildfire Expired");
        }
    }
}
