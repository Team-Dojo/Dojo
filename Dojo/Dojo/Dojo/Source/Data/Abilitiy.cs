using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Entities;

namespace Dojo.Source.Data
{
    abstract class Ability
    {
        protected int timer;
        protected Player player;
        protected enum State { ACTIVE, IDLE };
        protected State state;
        public bool draw { get; protected set; }

        public Ability()
        {
            state = State.IDLE;
            timer = 0;
            draw = false;
        }

        virtual protected void Init()
        {
        }

        virtual public void Draw()
        {
        }

        abstract protected void Effect();

        abstract protected void Destroy();

        public void Trigger(Player target, int duration)
        {
            state = State.ACTIVE;
            player = target;
            timer = duration;

            Init();
        }

        public void Update()
        {
            if (state == State.ACTIVE)
            {
                if (timer != 0)
                {
                    Effect();
                    timer--;
                }
                else
                {
                    Destroy();
                    state = State.IDLE;
                }
            }
        }
    }
}
