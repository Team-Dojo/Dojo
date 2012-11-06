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

namespace Dojo.Source.States
{
    class Play : State
    {
        private int team_size = 1;
        private int num_players;
        private Player[] player;
        public static Static wall = new Static(true, (int)Static.Orientation.NONE);
        public static HUD hud;
        private Court redCourt = new Court((int)Court.Type.RED);
        private Court blueCourt = new Court((int)Court.Type.BLUE);
        public static List<Sprite> collisionArray;
        public static bool running = true;
        private Sprite redVictorySprite = new Sprite((int)Sprite.Orientation.NONE);
        private Sprite blueVictorySprite = new Sprite((int)Sprite.Orientation.NONE);
        public int victor = -1;
        private Song music;


        public Play()
        {
            num_players = team_size * 2;
            hud = new HUD(team_size);
        }

        public override void Init()
        {
            music = GameManager.contentManager.Load<Song>("Audio/Music/Song");

            collisionArray = new List<Sprite>();
            redVictorySprite.SetTexture("Assets/RedVictory");
            blueVictorySprite.SetTexture("Assets/BlueVictory");
            redVictorySprite.position.Y = 300;
            blueVictorySprite.position.Y = 300;
            blueVictorySprite.position.X = 700;

            player = new Player[num_players];

            if (team_size == Ref.MIN_TEAM_SIZE)
            {
                player[0] = new Player(PlayerIndex.One, Ref.TEAM_ONE, (int)Player.Orientation.RIGHT, GameManager.contentManager, 500, 380);
                player[1] = new Player(PlayerIndex.Two, Ref.TEAM_TWO, (int)Player.Orientation.LEFT, GameManager.contentManager, 740, 380);
            }
            else if (team_size == Ref.MIN_TEAM_SIZE)
            {
                player[0] = new Player(PlayerIndex.One, Ref.TEAM_ONE, (int)Player.Orientation.RIGHT, GameManager.contentManager, 500, 340);
                player[1] = new Player(PlayerIndex.Two, Ref.TEAM_ONE, (int)Player.Orientation.RIGHT, GameManager.contentManager, 500, 400);
                player[2] = new Player(PlayerIndex.Three, Ref.TEAM_TWO, (int)Player.Orientation.LEFT, GameManager.contentManager, 740, 340);
                player[3] = new Player(PlayerIndex.Four, Ref.TEAM_TWO, (int)Player.Orientation.LEFT, GameManager.contentManager, 740, 400);
            }
            else
            {
                System.Console.WriteLine("ERROR: NUM_PLAYERS is not even (must be set to 2 or 4 in order to play)");
            }

            redCourt.Init();
            blueCourt.Init();

            hud.Init();

            wall.SetTexture("Assets/Bar");
            wall.position = new Vector2(((Program.baseScreenSize.X/2) - (wall.width/2)), 120);

            collisionArray.Add(wall);

            base.Init();
        }

        public override void Update()
        {
            if(running) 
            {
                if (player[Ref.PLAYER_ONE].stamina <= 0)
                {
                    victor = Ref.TEAM_TWO;
                }
                else if (player[Ref.PLAYER_TWO].stamina <= 0)
                {
                    victor = Ref.TEAM_ONE;
                }

                // Updates hud based on player array
                hud.Update(player);

                // Updates players
                for (int i = 0; i < num_players; i++)
                {
                    player[i].Update();
                }

                redCourt.Update();
                blueCourt.Update();

                // Moves wall (X/B buttons)
                for (int z = 0; z < num_players; z++)
                {
                    if (controller[z].Buttons.X == ButtonState.Pressed)
                    {
                        wall.position.X -= 2;
                    }
                    if (controller[z].Buttons.B == ButtonState.Pressed)
                    {
                        wall.position.X += 2;
                    }
                }

                // Handles player collision detection
                for (int i = 0; i < num_players; i++)
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
                            /*
                            if (controller[i].Buttons.RightShoulder == ButtonState.Pressed)
                            {
                                wall.position.X += 2;
                            }
                             */
                        }
                    }
                }

                base.Update();
            }

        }

        public override void Draw()
        {
            if (MediaPlayer.State != MediaState.Playing)
            {
                //MediaPlayer.Play(music);
            } 

            redCourt.Draw();
            blueCourt.Draw();

            GameManager.spriteBatch.DrawString(Formats.arial, "Player 1: " + player[0].stamina, new Vector2(20, 5), Color.Red);
            GameManager.spriteBatch.DrawString(Formats.arial, "Player 2: " + player[1].stamina, new Vector2(600, 5), Color.Blue);

            for (int i = 0; i < num_players; i++)
            {
                player[i].Draw();
                player[i].DrawProj();
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

            
            base.Draw();
        }
    }
}
