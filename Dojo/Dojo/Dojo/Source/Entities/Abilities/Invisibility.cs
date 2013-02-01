using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Data;

namespace Dojo.Source.Entities.Abilities
{
    class Invisibility : Ability
    {
        public Invisibility()
        {
        }

        override protected void Effect()
        {
            player.visible = false;
        }

        override protected void Destroy()
        {
            player.visible = true;
            player.TimedText("Invisibility Expired");
        }
    }
}
