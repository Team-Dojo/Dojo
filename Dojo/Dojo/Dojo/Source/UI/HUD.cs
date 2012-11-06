using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dojo.Source.Framework.Display;
using Dojo.Source.Entity;
using Dojo.Source.Resources;

namespace Dojo.Source.UI
{
    class HUD
    {
        private const int NUM_BARS = 2;
        private int team_size;
        private Sprite[] staminaBar = new Sprite[NUM_BARS];
        public const int HEIGHT = 120;
        public const int WIDTH = 1280;

        public HUD(int _team_size)
        {
            team_size = _team_size;

            staminaBar[0] = new Sprite((int)Sprite.Orientation.RIGHT);
            staminaBar[0].position = new Vector2(20, 50);
            staminaBar[1] = new Sprite((int)Sprite.Orientation.LEFT);
            staminaBar[1].position = new Vector2(950, 50);
        }

        public void Init()
        {
            staminaBar[0].SetTexture("Assets/UI/RedBar");
            staminaBar[1].SetTexture("Assets/UI/BlueBar");
        }

        public void Draw()
        {
            for (int i = 0; i < NUM_BARS; i++)
            {
                GameManager.spriteBatch.Draw(staminaBar[i].texture, staminaBar[i].position, null, Color.White, 0.0f, Vector2.Zero, staminaBar[i].scale, SpriteEffects.None, 0);
            }
        }

        public void Update(Player[] player)
        {
            if (team_size == 1)
            {
                staminaBar[Ref.TEAM_ONE].scale.X = player[0].percentStamina;
                staminaBar[Ref.TEAM_TWO].scale.X = player[1].percentStamina;
                staminaBar[Ref.TEAM_TWO].position.X = 1250 - (3f * player[1].stamina);
            }
            else if (team_size == 2)
            {
                staminaBar[Ref.TEAM_ONE].scale.X = 1;
                staminaBar[Ref.TEAM_TWO].scale.X = 1;
            }
            else
            {
                System.Console.WriteLine("ERROR: NUM_PLAYERS is not even (must be set to 2 or 4 in order to play)");
            }
        }
    }
}
