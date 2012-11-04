using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Dojo.Source.Framework.Entity;
using Dojo.Source.Framework.Display;
using Dojo.Source.Entity;

namespace Dojo.Source.States
{
    class Play : State
    {
        public static int TEAM_ONE = 1;
        public static int TEAM_TWO = 2;
        private Player player;

        public Play()
        {
            
        }

        public override void Init()
        {
           
        }

        public override void Draw()
        {
            player.Draw();
        }

        public override void Load()
        {
            player = new Player(PlayerIndex.One, TEAM_ONE, Static.RIGHT, GameManager.contentManager);
        }

        public override void Update()
        {
            player.Update();
        }
    }
}
