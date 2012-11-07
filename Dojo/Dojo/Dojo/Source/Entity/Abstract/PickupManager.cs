using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Entity.Pickups;
using Dojo.Source.States;

namespace Dojo.Source.Entity.Abstract
{
    class PickupManager
    {
        private static int MAX_PICKUPS = 10;
        private Pickup[] pickups;
        public int pickupCount;
        private int timer;
        private int spawnTime;
        private bool collisionDetected;

        public PickupManager()
        {
            timer = 0;
            pickupCount = 0;
            spawnTime = 40;
            collisionDetected = false;
            pickups = new Pickup[MAX_PICKUPS];
        }

        public Pickup ReturnPickup()
        {
            //Pickup[] PICKUPS = new Pickup[] { new FireRate()};
            Pickup[] PICKUPS = new Pickup[] { new Damage(), new Speed(), new FireRate(), new Range(), new Stamina() };
            Random rand = new Random();
            int pickupNum = rand.Next(0, (PICKUPS.Length - 1));

            PICKUPS[pickupNum].Init();

            return PICKUPS[pickupNum];
        }

        public void RemovePickup()
        {
            int curPickupIndex = -1;
            int lastPickup = pickupCount - 1;

            for (int i = 0; i < pickupCount; i++)
            {
                if (!pickups[i].active)
                {
                    curPickupIndex = i;
                    break;
                }
            }

            if (curPickupIndex != -1)
            {
                if (pickups[lastPickup] != null)
                {
                    pickups[curPickupIndex] = pickups[lastPickup];
                }
            }

            pickupCount--;
        }

        public void Update()
        {
            if (timer == spawnTime)
            {
                if (pickupCount != MAX_PICKUPS)
                {
                    pickupCount++;
                    int i = (pickupCount - 1);
                    Random rand = new Random();

                    int x;
                    int y;

                    pickups[i] = ReturnPickup();

                    do
                    {
                        collisionDetected = false;

                        x = rand.Next(0, Program.SCREEN_WIDTH);
                        y = rand.Next(120, Program.SCREEN_HEIGHT);

                        pickups[i].position.X = x;
                        pickups[i].position.Y = y;

                        for (int j = 0; j < pickupCount; j++)
                        {
                            if (j != i)
                            {
                                for (int z = 0; z < Play.numPlayers; z++)
                                {
                                    if (pickups[i].HitTestObject(Play.player[z]))
                                    {
                                        collisionDetected = true;
                                    }
                                }

                                if (pickups[j].HitTestObject(pickups[i]))
                                {
                                    collisionDetected = true;
                                }

                                if ((pickups[i].position.X + pickups[i].width) >= Program.SCREEN_WIDTH)
                                {
                                    collisionDetected = true;
                                }

                                if ((pickups[i].position.Y + pickups[i].height) >= Program.SCREEN_HEIGHT)
                                {
                                    collisionDetected = true;
                                }
                            }
                        }

                    } while (collisionDetected);

                    Play.collisionArray.Add(pickups[i]);
                }
                timer = 0;
            }
            timer++;
        }

        public void Draw()
        {
            for (int i = 0; i < pickupCount; i++)
            {
                pickups[i].Draw();
            }
        }
    }
}
