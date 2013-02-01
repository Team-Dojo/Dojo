using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Dojo.Source.Display;
using Dojo.Source.Entities;
using Dojo.Source.UI;
using Dojo.Source.Resources;
using Dojo.Source;

namespace Dojo.Source.States
{
    class Play : State
    {
        public static bool paused;
        public static GameObject wall;
        public static HUD hud;
        public static PickupManager pickupManager;
        public static List<GameObject> collisionArray;
        public static int numPlayers;
        public static Player[] player;

        private int victoryCooldown = 250;

        private int victor = -1;
        private int teamSize;
        private Court redCourt;
        private Court blueCourt;
        private Sprite redVictorySprite;
        private Sprite blueVictorySprite;

        private const int RESUME = 0;
        private const int QUIT = 1;
        private Sprite resume;
        private Sprite quit;
        private int pauseSelect;
        private Vector2 pauseMenuPos;

        private Sprite aContinue;

        public float rtStamina;
        public float btStamina;
        public float percentRTStamina;
        public float percentBTStamina;
        public float MAX_STAMINA;

        public Play(int teamSize)
        {
            pickupManager = new PickupManager();
            paused = false;
            this.teamSize = teamSize;

            pauseSelect = RESUME;

            pauseMenuPos = new Vector2(440, 295);
            resume = new Sprite();
            quit = new Sprite();
            aContinue = new Sprite();

            if (teamSize == Global.MIN_TEAM_SIZE)
            {
                MAX_STAMINA = 100.0f;
            }
            else if (teamSize == Global.MAX_TEAM_SIZE)
            {
                MAX_STAMINA = 200.0f;
            }

            rtStamina = btStamina = MAX_STAMINA;

            redCourt = new Court((int)Court.Type.RED);
            blueCourt = new Court((int)Court.Type.BLUE);

            wall = new GameObject(true, (int)GameObject.Orientation.NONE);
            wall.name = "wall";
            wall.speed.X = 1;

            redVictorySprite = new Sprite((int)Sprite.Orientation.NONE);
            blueVictorySprite = new Sprite((int)Sprite.Orientation.NONE);

            hud = new HUD(teamSize);
            collisionArray = new List<GameObject>();
        }

        public override void Unload()
        {
            paused = true;
            MediaPlayer.Stop();
            redVictorySprite = null;
            blueVictorySprite = null;
            collisionArray = null;
            player = null;

            base.Unload();
        }

        public override void Init()
        {
            numPlayers = (teamSize * 2);
            player = new Player[numPlayers];

            // Instantiate players
            if (teamSize == Global.MIN_TEAM_SIZE)
            {
                player[0] = new Player(PlayerIndex.One, Global.RED_TEAM, (int)Player.Orientation.RIGHT, new Vector2(300, 380));
                player[1] = new Player(PlayerIndex.Two, Global.BLUE_TEAM, (int)Player.Orientation.LEFT, new Vector2(1040, 380));
            }
            else if (teamSize == Global.MAX_TEAM_SIZE)
            {
                player[0] = new Player(PlayerIndex.One, Global.RED_TEAM, (int)Player.Orientation.RIGHT, new Vector2(300, 240));
                player[1] = new Player(PlayerIndex.Two, Global.RED_TEAM, (int)Player.Orientation.RIGHT, new Vector2(300, 500));
                player[2] = new Player(PlayerIndex.Three, Global.BLUE_TEAM, (int)Player.Orientation.LEFT, new Vector2(1040, 240));
                player[3] = new Player(PlayerIndex.Four, Global.BLUE_TEAM, (int)Player.Orientation.LEFT, new Vector2(1040, 500));
            }
            else
            {
                System.Console.WriteLine("ERROR: numPlayers is not even (must be set to 2 or 4 in order to play)");
            }

            for (int i = 0; i < numPlayers; i++)
            {
                collisionArray.Add(player[i]);
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
            // Load player textures
            for (int i = 0; i < numPlayers; i++)
            {
                player[i].Load();
            }

            redVictorySprite.SetTexture("Assets/RedVictory");
            blueVictorySprite.SetTexture("Assets/BlueVictory");
            aContinue.SetTexture("Assets/States/SplashPlay");
            resume.SetTexture("Assets/States/PauseResume");
            quit.SetTexture("Assets/States/PauseQuit");

            aContinue.position = new Vector2(320, 120);

            // Position sprites and game objects
            wall.SetTexture("Assets/Bar");

            base.Load();
        }

        public override void Update()
        {
            // Pause
            if (Pressed(Buttons.Start))
            {
                paused = !paused;
            }

            if (!paused)
            {
                percentRTStamina = rtStamina / MAX_STAMINA;
                percentBTStamina = btStamina / MAX_STAMINA;

                if (rtStamina <= 0)
                {
                    victor = Global.BLUE_TEAM;
                    wall.position.X -= wall.speed.X;
                    player[Global.PLAYER_ONE].Disable();
                    if (wall.speed.X < 30)
                    {
                        wall.speed.X++;
                    }
                    victoryCooldown--;
                }
                else if (btStamina <= 0)
                {
                    victor = Global.RED_TEAM;
                    wall.position.X += wall.speed.X;
                    player[Global.PLAYER_TWO].Disable();
                    if (wall.speed.X < 30)
                    {
                        wall.speed.X++;
                    }
                    victoryCooldown--;
                }

                if (victoryCooldown <= 0)
                {
                    if (Pressed(Buttons.A))
                        GameManager.SwitchState(StateID.SPLASH);
                }

                // Updates hud based on player array
                hud.Update(percentRTStamina, percentBTStamina);

                // Updates players
                for (int i = 0; i < numPlayers; i++)
                {
                    player[i].Update();
                }

                if (victor == -1)
                {
                    if (teamSize == Global.MAX_TEAM_SIZE)
                    {
                        player[0].AlterStamina(ref rtStamina);
                        player[1].AlterStamina(ref rtStamina);
                        player[2].AlterStamina(ref btStamina);
                        player[3].AlterStamina(ref btStamina);
                    }
                    else
                    {
                        player[0].AlterStamina(ref rtStamina);
                        player[1].AlterStamina(ref btStamina);
                    }
                }

                if (rtStamina > MAX_STAMINA)
                {
                    rtStamina = MAX_STAMINA;
                }
                else if (rtStamina < 0)
                {
                    rtStamina = 0;
                }

                if (btStamina > MAX_STAMINA)
                {
                    btStamina = MAX_STAMINA;
                }
                else if (btStamina < 0)
                {
                    btStamina = 0;
                }

                redCourt.Update();
                blueCourt.Update();

                // Update pickups
                pickupManager.Update();

                // Prevents wall from getting outwith bounds
                if (wall.position.X <= 0 - (wall.width / 2))
                {
                    wall.position.X = 0 - (wall.width / 2);
                    victor = Global.BLUE_TEAM;
                    wall.speed.X = 0;
                    victoryCooldown--;
                }
                if (wall.position.X + (wall.width / 2) >= Program.SCREEN_WIDTH)
                {
                    wall.position.X = Program.SCREEN_WIDTH - (wall.width / 2);
                    victor = Global.RED_TEAM;
                    wall.speed.X = 0;
                    victoryCooldown--;
                }
            }
            else
            {
                Pause();
            }

            base.Update();
        }

        public void Pause()
        {
            if (Pressed(Buttons.DPadDown) || Pressed(Buttons.LeftThumbstickDown))
            {
                pauseSelect++;
            }

            if (Pressed(Buttons.DPadUp) || Pressed(Buttons.LeftThumbstickUp))
            {
                pauseSelect--;
            }

            if (pauseSelect < RESUME)
            {
                pauseSelect = QUIT;
            }

            if (pauseSelect > QUIT)
            {
                pauseSelect = RESUME;
            }

            if (Pressed(Buttons.A))
            {
                switch (pauseSelect)
                {
                    case RESUME: paused = false; break;
                    case QUIT: GameManager.SwitchState(StateID.MAIN_MENU); break;
                }
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

            GameManager.spriteBatch.Draw(wall.texture, wall.position, Color.White);

            for (int i = 0; i < numPlayers; i++)
            {
                player[i].DrawProj();
            }
            for (int i = 0; i < numPlayers; i++)
            {
                player[i].Draw();
            }

            hud.Draw();

            if (victor == Global.RED_TEAM)
            {
                redVictorySprite.Draw();
            }
            else if (victor == Global.BLUE_TEAM)
            {
                blueVictorySprite.Draw();
            }

            if (paused)
            {
                switch (pauseSelect)
                {
                    case RESUME: GameManager.spriteBatch.Draw(resume.texture, pauseMenuPos, Color.White); break;
                    case QUIT: GameManager.spriteBatch.Draw(quit.texture, pauseMenuPos, Color.White); break;
                }
            }

            if (victoryCooldown <= 0)
            {
                aContinue.Draw();
            }

            base.Draw();
        }
    }
}
