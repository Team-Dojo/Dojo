using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Data;
using Dojo.Source.Display;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Dojo.Source.Resources;

namespace Dojo.Source.Entities.Abilities
{
    class Illusion : Ability
    {
        private int numIllusions;
        private List<GameObject> illusions;
        private List<Vector2> offsets;
        private const int MAX_TIME = 50;

        private int prevLevel;

        public Illusion()
        {
            illusions = new List<GameObject>();
            offsets = new List<Vector2>();
        }

        protected override void Init()
        {
            Random rand = new Random();

            if (offsets.Count > 0)
            {
                offsets.Clear();
            }

            numIllusions = rand.Next(2, 5);

            for (int i = 0; i < numIllusions; i++)
            {
                GameObject obj = new GameObject(false, player.orientation);

                if (player.team == Global.RED_TEAM)
                    obj.SetTexture("Assets/RedPlayer");
                else if (player.team == Global.BLUE_TEAM)
                    obj.SetTexture("Assets/BluePlayer");
                else
                    System.Console.WriteLine("LOADING PLAYER IMAGE FAILED: NO TEAM ASSIGNED!");

                illusions.Add(obj);
            }

            draw = true;

            for (int i = 0; i < numIllusions; i++)
            {
                float x = (float)((i + 1) * 50) + rand.Next(-100, 100);
                float y = (float)((i + 1) * 50) + rand.Next(-100, 100);
                
                Vector2 pos = new Vector2(x, y);

                offsets.Add(pos);

                illusions.ElementAt(i).position.X = player.position.X + offsets[i].X;
                illusions.ElementAt(i).position.Y = player.position.Y + offsets[i].Y;
            }

            prevLevel = player.pushSpeed.level;
            player.pushSpeed.SetLevel(prevLevel + numIllusions);

            base.Init();
        }

        override protected void Effect()
        {
            for (int i = 0; i < numIllusions; i++)
            {
                illusions.ElementAt(i).position.X = player.position.X + offsets[i].X;
                illusions.ElementAt(i).position.Y = player.position.Y + offsets[i].Y;
            }

           
        }

        override protected void Destroy()
        {
            player.TimedText("Illusion Expired");
            draw = false;
            player.pushSpeed.SetLevel(prevLevel);
        }

        public override void Draw()
        {
            for (int i = 0; i < numIllusions; i++)
            {
                illusions.ElementAt(i).Draw();
            }

            base.Draw();
        }
    }
}
