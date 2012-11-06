using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Framework.Display;
namespace Dojo.Source.Entity
{
    class Pickup : Static
    {
        public bool active { get; protected set; }
        public string description { get; protected set; }
        public int chance { get; protected set; }
        protected Player player;
        protected Player opposingPlayer;
        protected int effect;

        public Pickup()
            : base(true, (int)Orientation.NONE)
        {
            active = true;
            effect = -20;
            description = "description undefined";
            visible = false;
        }

        public virtual void Init()
        {

        }

        public void Reset()
        {
            visible = true;
            active = true;
        }

        public void Activate(Player players)
        {
            player = players;
            opposingPlayer = players;

            visible = false;
            active = false;

            Effect();
            Destroy();
        }

        public void Destroy()
        {
            // Nullify variables.
            player = null;
            opposingPlayer = null;
            active = false;
            description = "";
            chance = 0;
        }

        protected virtual void Effect()
        {
            // Effect of pickup should be carried out here.
        }

        protected void DetermineEffect()
        {
            /*
            do {
                effect = Maths.RandomNumber(-3, 3);
            } while (effect == 0);

            if (effect > 0) {
                _description += " Up";
            } else {
                _description += " Down";
            }
             */
        }
    }
}
