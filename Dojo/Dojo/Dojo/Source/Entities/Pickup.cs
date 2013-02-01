using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Display;

namespace Dojo.Source.Entities
{
    class Pickup : GameObject
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
            description = "Description Undefined";
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
        }

        public void Destroy()
        {
            // Nullify variables
            player = null;
            opposingPlayer = null;
            active = false;
            visible = false;
            description = "";
            chance = 0;
        }

        protected virtual void Effect()
        {
            // Effect of pickup should be carried out here
        }

        protected void BoolEffect(bool state)
        {
            if (state == true)
                description += " On";
            else
                description += " Off";
        }

        protected void DetermineEffect()
        {
            Random rand = new Random();

            do
            {
                effect = rand.Next(-1, 2);
            } 
            while (effect == 0);

            if (effect > 0) 
                description += " Up";
            else
                description += " Down";
        }
    }
}
