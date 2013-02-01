using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Data;

namespace Dojo.Source.Entities.Abilities
{
    class Invincibility : Ability
    {
        public Invincibility()
        {
        }

        override protected void Effect()
        {
            player.invincible = true;
        }

        override protected void Destroy()
        {
            player.invincible = false;
            player.TimedText("Invincibility Expired");
        }
    }
}
