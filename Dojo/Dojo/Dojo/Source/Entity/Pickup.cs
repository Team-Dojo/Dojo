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
            description = "description undefined";
            active = true;
            visible = true;
        }

        public virtual void Init()
        {
            // Initialise texture
        }

        public void Activate(Player players)
        {
            player = players;
            opposingPlayer = players;

            Effect();
            Destroy();
        }

        public void Destroy()
        {
            // Nullify variables.
            player = null;
            opposingPlayer = null;
            active = false;
            visible = false;
            description = "";
            chance = 0;
        }

        protected virtual void Effect()
        {
            // Effect of pickup should be carried out here.
        }

        protected void DetermineEffect(int min, int max)
        {
            Random rand = new Random();
            do {
                effect = rand.Next(min, max);
            } while (effect == 0);

            if (effect > 0) {
                description += " Up";
            } else {
                description += " Down";
            }
        }
    }
}
