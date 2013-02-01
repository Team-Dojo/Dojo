using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.Display;
using Dojo.Source.Entities;
using Dojo.Source.Resources;

namespace Dojo.Source.UI
{
    class HUD
    {
        public const int HEIGHT = 120;
        public const int WIDTH = 1280;
        private const int NUM_BARS = 2;
        private int teamSize;
        private Sprite[] staminaBar = new Sprite[NUM_BARS];
        private float blueWidth;

        public HUD(int _teamSize)
        {
            teamSize = _teamSize;

            staminaBar[Global.RED_TEAM] = new Sprite((int)Sprite.Orientation.RIGHT);
            staminaBar[Global.RED_TEAM].position = new Vector2(20, 60);
            staminaBar[Global.BLUE_TEAM] = new Sprite((int)Sprite.Orientation.LEFT);
            staminaBar[Global.BLUE_TEAM].position = new Vector2(950, 60);

            
        }

        public void Init()
        {
            staminaBar[Global.RED_TEAM].SetTexture("Assets/UI/RedBar");
            staminaBar[Global.BLUE_TEAM].SetTexture("Assets/UI/BlueBar");

            blueWidth = staminaBar[Global.BLUE_TEAM].width;
        }

        public void Draw()
        {
            for (int i = 0; i < NUM_BARS; i++)
            {
                GameManager.spriteBatch.Draw(staminaBar[i].texture, staminaBar[i].position, null, Color.White, 0.0f, Vector2.Zero, staminaBar[i].scale, SpriteEffects.None, 0);
            }
        }

        public void Update(float redTeam, float blueTeam)
        {
            if (teamSize == Global.MIN_TEAM_SIZE)
            {
                staminaBar[Global.RED_TEAM].scale.X = redTeam;
                staminaBar[Global.BLUE_TEAM].scale.X = blueTeam;
            }
            else if (teamSize == Global.MAX_TEAM_SIZE)
            {
                staminaBar[Global.RED_TEAM].scale.X = redTeam;
                staminaBar[Global.BLUE_TEAM].scale.X = blueTeam;
            }
            else
            {
                System.Console.WriteLine("ERROR: NUM_PLAYERS is not even (must be set to 2 or 4 in order to play)");
            }

            float curWidth = (staminaBar[Global.BLUE_TEAM].width) * (staminaBar[Global.BLUE_TEAM].scale.X);
            staminaBar[Global.BLUE_TEAM].position.X = 950 + (blueWidth - curWidth);
        }
    }
}
