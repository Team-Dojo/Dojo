using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Dojo.Source.Entity;

namespace Dojo.Source
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static int TEAM_ONE = 1;
        public static int TEAM_TWO = 2;        
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static Vector2 baseScreenSize = new Vector2(1280, 720); // Important game elements have to be rendered in 80% - 90% of the fullscreen size.
        Player player;
        Player player2;
        Texture2D BackGround;
        Vector2 BackGroundPosition = Vector2.Zero;
        Texture2D Wall;
        Vector2 WallPosition = Vector2.Zero;
        Texture2D Player1HealthBar;
        Vector2 Player1HealthBarPosition = Vector2.Zero;
        Texture2D Player2HealthBar;
        Vector2 Player2HealthBarPosition = Vector2.Zero;
        SpriteFont font;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Notes the actual screen width / height.
            //System.Console.WriteLine(device.PresentationParameters.BackBufferHeight);
            //System.Console.WriteLine(device.PresentationParameters.BackBufferWidth);

            // Configure display.
            graphics.PreferredBackBufferWidth = (int)baseScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)baseScreenSize.Y;
            graphics.IsFullScreen = false; // Use false for debug and true for release.
            graphics.ApplyChanges();

            // Configure players.

            player = new Player(PlayerIndex.One, TEAM_ONE, (int)Player.Orientation.RIGHT, Content, 50, 380);
            player2 = new Player(PlayerIndex.Two, TEAM_TWO,(int)Player.Orientation.LEFT, Content, 1200, 380);

            BackGround = Content.Load<Texture2D>("Areana");
            BackGroundPosition = new Vector2(0, 0);

            Wall = Content.Load<Texture2D>("Wall");
            WallPosition = new Vector2(633, 97);

            Player1HealthBar = Content.Load<Texture2D>("Player1Health");
            Player1HealthBarPosition = new Vector2(950, 50);

            Player2HealthBar = Content.Load<Texture2D>("Player2Health");
            Player2HealthBarPosition = new Vector2(20, 50); 
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("DojoFont");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit.
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                this.Exit();
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed) 
            {
               WallPosition.X -= 2;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed) 
            {
               WallPosition.X += 2;
            }
            player.Update();
            player2.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(BackGround, BackGroundPosition, Color.White);
            spriteBatch.Draw(Wall, WallPosition, Color.White);
            spriteBatch.Draw(Player1HealthBar, Player1HealthBarPosition, Color.White);
            spriteBatch.Draw(Player2HealthBar, Player2HealthBarPosition, Color.White);
            spriteBatch.DrawString(font, "Player 1", new Vector2(20, 5), Color.Red);
            spriteBatch.DrawString(font, "Player 2", new Vector2(1100, 5), Color.Blue);
            player.Draw();
            player.DrawProj();
            player2.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
