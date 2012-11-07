using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Dojo.Source.Framework.Display;
using Dojo.Source.Entity;
using Dojo.Source.UI;
using Dojo.Source.Resources;
using Dojo.Source.Entity.Abstract;

namespace Dojo.Source.States
{
    class Play : State
    {
        public static bool running;
        public static Static wall;
        public static HUD hud;
        public static PickupManager pickupManager;
        public static List<Sprite> collisionArray;
        public static int numPlayers;
        public static Player[] player;

        private int victor = -1;
        private int teamSize;
        private Court redCourt;
        private Court blueCourt;
        private Sprite redVictorySprite;
        private Sprite blueVictorySprite;
        private int speed;
        private MessageBox pauseMessage;

        public Play()
        {
            speed = 1;
            pickupManager = new PickupManager();
            pauseMessage = null;
            running = false;
            teamSize = 1;

            redCourt = new Court((int)Court.Type.RED);
            blueCourt = new Court((int)Court.Type.BLUE);
            wall = new Static(true, (int)Static.Orientation.NONE);
            wall.name = "wall";
            redVictorySprite = new Sprite((int)Sprite.Orientation.NONE);
            blueVictorySprite = new Sprite((int)Sprite.Orientation.NONE);

            hud = new HUD(teamSize);
            collisionArray = new List<Sprite>();
        }

        public override void Unload()
        {
            running = false;
            MediaPlayer.Stop();
            redVictorySprite = null;
            blueVictorySprite = null;
            collisionArray = null;
            player = null;

            base.Unload();
        }

        public override void Init()
        {
            running = true;

            numPlayers = (teamSize * 2);
            player = new Player[numPlayers];

            // Instantiate players
            if (teamSize == Ref.MIN_TEAM_SIZE)
            {
                player[0] = new Player(PlayerIndex.One, Ref.TEAM_ONE, (int)Player.Orientation.RIGHT, GameManager.contentManager, 500, 380);
                player[1] = new Player(PlayerIndex.Two, Ref.TEAM_TWO, (int)Player.Orientation.LEFT, GameManager.contentManager, 740, 380);
            }
            else if (teamSize == Ref.MAX_TEAM_SIZE)
            {
                player[0] = new Player(PlayerIndex.One, Ref.TEAM_ONE, (int)Player.Orientation.RIGHT, GameManager.contentManager, 500, 340);
                player[1] = new Player(PlayerIndex.Two, Ref.TEAM_ONE, (int)Player.Orientation.RIGHT, GameManager.contentManager, 500, 400);
                player[2] = new Player(PlayerIndex.Three, Ref.TEAM_TWO, (int)Player.Orientation.LEFT, GameManager.contentManager, 740, 340);
                player[3] = new Player(PlayerIndex.Four, Ref.TEAM_TWO, (int)Player.Orientation.LEFT, GameManager.contentManager, 740, 400);
            }
            else
            {
                System.Console.WriteLine("ERROR: numPlayers is not even (must be set to 2 or 4 in order to play)");
            }

            redVictorySprite.position.X = blueVictorySprite.position.X = (Program.SCREEN_WIDTH / 2) - 250;
            redVictorySprite.position.Y = blueVictorySprite.position.Y = (Program.SCREEN_HEIGHT / 2) - 350;

            wall.position = new Vector2(((Program.SCREEN_WIDTH / 2) - (wall.width / 2)), 120);

            // Initialise game components
            redCourt.Init();
            blueCourt.Init();
            hud.Init();

            // Update collision array
            collisionArray.Add(wall);

            base.Init();
        }

        public override void Load()
        {
            redVictorySprite.SetTexture("Assets/RedVictory");
            blueVictorySprite.SetTexture("Assets/BlueVictory");

            // Position sprites and game objects
            wall.SetTexture("Assets/Bar");

            base.Load();
        }

        public override void Update()
        {
            // Pause
            if (Pressed(Buttons.Start))
            {
                Pause();
            }

            // Reset
            for (int i = 0; i < numPlayers; i++)
            {
                if (controller[i].IsButtonDown(Buttons.A) &&
                    controller[i].IsButtonDown(Buttons.B) &&
                    controller[i].IsButtonDown(Buttons.X) &&
                    controller[i].IsButtonDown(Buttons.Y))
                {
                    Reset();
                }
            }

            if(running) 
            {
                if (player[Ref.PLAYER_ONE].stamina <= 0)
                {
                    victor = Ref.TEAM_TWO;
                    wall.position.X -= speed;
                    player[Ref.PLAYER_ONE].Disable();
                    if (speed < 30)
                    {
                        speed++;
                    }
                }
                else if (player[Ref.PLAYER_TWO].stamina <= 0)
                {
                    victor = Ref.TEAM_ONE;
                    wall.position.X += speed;
                    player[Ref.PLAYER_TWO].Disable();
                    if (speed < 30)
                    {
                        speed++;
                    }
                }

                // Updates hud based on player array
                hud.Update(player);

                // Updates players
                for (int i = 0; i < numPlayers; i++)
                {
                    player[i].Update();
                }

                redCourt.Update();
                blueCourt.Update();

                // Update pickups
                pickupManager.Update();

                // Moves wall (X/B buttons)
                if (wall.position.X <= 0 - (wall.width / 2))
                {
                    wall.position.X = 0 - (wall.width / 2);
                    victor = Ref.TEAM_TWO;
                    speed = 0;
                }
                if (wall.position.X + (wall.width / 2) >= Program.SCREEN_WIDTH)
                {
                    wall.position.X = Program.SCREEN_WIDTH - (wall.width / 2);
                    victor = Ref.TEAM_ONE;
                    speed = 0;
                }

                // Handles player collision detection
                for (int i = 0; i < numPlayers; i++)
                {
                    for (int j = 0; j < collisionArray.Count; j++)
                    {
                        if (player[i].HitTestObject(collisionArray[j]))
                        {
                            if (!(collisionArray[j] is Projectile) && !(collisionArray[j] is Pickup))
                            {

                                if (player[i].orientation == (int)Player.Orientation.LEFT)
                                {
                                    player[i].position.X = (collisionArray[j].position.X + collisionArray[j].width);
                                }
                                else
                                {
                                    player[i].position.X = (collisionArray[j].position.X - player[i].width);
                                }
                            }
                        }
                    }
                }

                
            }
            base.Update();
        }

        public void Reset()
        {
            // Instantiate players
            if (teamSize == Ref.MIN_TEAM_SIZE)
            {
                player[0] = new Player(PlayerIndex.One, Ref.TEAM_ONE, (int)Player.Orientation.RIGHT, GameManager.contentManager, 500, 380);
                player[1] = new Player(PlayerIndex.Two, Ref.TEAM_TWO, (int)Player.Orientation.LEFT, GameManager.contentManager, 740, 380);
            }
            else if (teamSize == Ref.MAX_TEAM_SIZE)
            {
                player[0] = new Player(PlayerIndex.One, Ref.TEAM_ONE, (int)Player.Orientation.RIGHT, GameManager.contentManager, 500, 340);
                player[1] = new Player(PlayerIndex.Two, Ref.TEAM_ONE, (int)Player.Orientation.RIGHT, GameManager.contentManager, 500, 400);
                player[2] = new Player(PlayerIndex.Three, Ref.TEAM_TWO, (int)Player.Orientation.LEFT, GameManager.contentManager, 740, 340);
                player[3] = new Player(PlayerIndex.Four, Ref.TEAM_TWO, (int)Player.Orientation.LEFT, GameManager.contentManager, 740, 400);
            }
            else
            {
                System.Console.WriteLine("ERROR: numPlayers is not even (must be set to 2 or 4 in order to play)");
            }

            victor = -1;

            wall.position = new Vector2(((Program.SCREEN_WIDTH / 2) - (wall.width / 2)), 120);
        }

        public void Pause()
        {
            if (running)
            {
                running = false;
                pauseMessage = new MessageBox("GAME PAUSED", Formats.arialLarge);
            }
            else
            {
                running = true;
                pauseMessage = null;
            }
        }

        public override void Draw()
        {
            redCourt.Draw();
            blueCourt.Draw();

            // Draw pickups
            pickupManager.Draw();

            GameManager.spriteBatch.DrawString(Formats.arial, "Player 1", new Vector2(20, 15), Color.Red);
            GameManager.spriteBatch.DrawString(Formats.arial, "Player 2", new Vector2(1115, 15), Color.Blue);

            for (int i = 0; i < numPlayers; i++)
            {
                player[i].DrawProj();
                player[i].Draw();
            }

            GameManager.spriteBatch.Draw(wall.texture, wall.position, Color.White);

            hud.Draw();

            if (victor == Ref.TEAM_ONE)
            {
                redVictorySprite.Draw();
            }
            else if (victor == Ref.TEAM_TWO)
            {
                blueVictorySprite.Draw();
            }

            if (pauseMessage != null)
            {
                pauseMessage.Draw();
            }

            base.Draw();
        }
    }
}
