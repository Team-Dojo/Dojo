using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private Player player2;
        private Texture2D BackGround;
        private Vector2 BackGroundPosition = Vector2.Zero;
        private Static Wall = new Static(true, (int)Static.Orientation.NONE);
        private Texture2D Player1HealthBar;
        private Vector2 Player1HealthBarPosition = Vector2.Zero;
        private Texture2D Player2HealthBar;
        private Vector2 Player2HealthBarPosition = Vector2.Zero;
        private SpriteFont font;
        public static float health = 1;
        public static int ARENAHEIGHT = 102;

        public Play()
        {
            
        }

        public override void Init()
        {
            player = new Player(PlayerIndex.One, TEAM_ONE, (int)Player.Orientation.RIGHT, GameManager.contentManager, 50, 380);
            player2 = new Player(PlayerIndex.Two, TEAM_TWO, (int)Player.Orientation.LEFT, GameManager.contentManager, 1200, 380);

            BackGround = GameManager.contentManager.Load<Texture2D>("Areana");
            BackGroundPosition = new Vector2(0, 0);

            Wall.SetTexture("Wall");
            Wall.position = new Vector2(633, 97);

            Player2HealthBar = GameManager.contentManager.Load<Texture2D>("Player2Health");
            Player2HealthBarPosition = new Vector2(950, 50);

            Player1HealthBar = GameManager.contentManager.Load<Texture2D>("Player1Health");
            Player1HealthBarPosition = new Vector2(20, 50); 
        }

        public override void Draw()
        {
            GameManager.spriteBatch.Draw(BackGround, BackGroundPosition, Color.White);
            GameManager.spriteBatch.Draw(Wall.texture, Wall.position, Color.White);
            GameManager.spriteBatch.DrawString(font, "Player 1", new Vector2(20, 5), Color.Red);
            GameManager.spriteBatch.DrawString(font, "Player 2", new Vector2(1100, 5), Color.Blue);

            Vector2 vec = new Vector2((float)health, 1);
            GameManager.spriteBatch.Draw(Player1HealthBar, Player1HealthBarPosition, null, Color.White, 0.0f, Vector2.Zero, vec, SpriteEffects.None, 0);

            Vector2 vect = new Vector2((float)health, (float)1);
            GameManager.spriteBatch.Draw(Player2HealthBar, Player2HealthBarPosition, null, Color.White, 0.0f, Vector2.Zero, vect, SpriteEffects.None, 0);

            GameManager.spriteBatch.DrawString(font, "Player 1: " + player.stamina + "health " + health, new Vector2(20, 5), Color.Red);
            GameManager.spriteBatch.DrawString(font, "Player 2", new Vector2(1100, 5), Color.Blue);

            player.Draw();
            player.DrawProj();
            player2.Draw();
            player2.DrawProj();
        }

        public override void Load()
        {
            font = GameManager.contentManager.Load<SpriteFont>("DojoFont");
        }

        public override void Update()
        {
            player.Update();
            player2.Update();


            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                Wall.position.X -= 2;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
            {
                Wall.position.X += 2;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
            {
                health += 0.02f;
                Player2HealthBarPosition.X -= 6f;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                health -= 0.02f;
                Player2HealthBarPosition.X += 6f;
            }
            if (player.isAlive())
            {
                health = player.stamina / 100;
                Player2HealthBarPosition.X = 1250 - (player.stamina * 3f);
                //player.projectiles
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
            {
                //health = player.stamina / 100;
                // Player2HealthBarPosition.X += 7.5f;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
            {
                health -= player.stamina / 4000;
                Player2HealthBarPosition.X += 7.5f;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
            {
                health += player.stamina / 4000;
                Player2HealthBarPosition.X -= 7.5f;
            }

            // PLAYER 1 COLLISION DETECTION START
            if (player.HitTestObject(Wall))
            {
                player.position.X = Wall.position.X - player.width;
                if (GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed)
                {
                    Wall.position.X += 2;
                }
            }
            /*
            if ((player.position.X + PLAYERWIDTH) >= WallPosition.X)
            {
                player.position.X = WallPosition.X - PLAYERWIDTH;
                if (GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed)
                {
                    WallPosition.X += 2;
                }
            }*/
            if (player.position.X <= 0)
            {
                player.position.X = 0;
            }
            if (player.position.Y <= ARENAHEIGHT)
            {
                player.position.Y = ARENAHEIGHT;
            }
            if ((player.position.Y + player.height) >= Program.baseScreenSize.Y)
            {
                player.position.Y = Program.baseScreenSize.Y - player.height;
            }
            // PLAYER 1 COLLISION DETECTION END
            //**********************************************************//
            // PLAYER 2 COLLISION DETECTION START
           /*
            if (player2.position.X <= (WallPosition.X + Wall.Width))
            {
                player2.position.X = WallPosition.X + Wall.Width;
                if (GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed)
                {
                    WallPosition.X -= 2;
                }
            }*/
            if (player2.position.X + player.width >= Program.baseScreenSize.X)
            {
                player2.position.X = Program.baseScreenSize.X - player.width;
            }
            if (player2.position.Y <= ARENAHEIGHT)
            {
                player2.position.Y = ARENAHEIGHT;
            }
            if ((player2.position.Y + player.height) >= Program.baseScreenSize.Y)
            {
                player2.position.Y = Program.baseScreenSize.Y - player.height;
            }

            
        }
    }
}
