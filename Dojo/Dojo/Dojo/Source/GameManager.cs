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
using Dojo.Source.Framework.Entity;
using Dojo.Source.States;
using Dojo.Source.Framework.Display;

namespace Dojo.Source
{
    public class GameManager : Microsoft.Xna.Framework.Game
    {
        //-----------------------------
        // Properties
        //-----------------------------
        // Display
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static ContentManager contentManager;
        // States
        private static int currentStateID = StateID.SPLASH;
        private static State currentState = null;
        private static Splash splash = new Splash();
        private static Play play = new Play();

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = Content;
            SwitchState(currentStateID);
        }

        protected override void Initialize()
        {
            // Configure display.
            graphics.PreferredBackBufferWidth = (int)Program.baseScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)Program.baseScreenSize.Y;
            graphics.IsFullScreen = false; // Use false for debug and true for release.
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            splash.Load();
            play.Load();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit.
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                this.Exit();
            }

            currentState.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
                currentState.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void SwitchState(int newState)
        {
            if (newState != currentStateID)
            {
                currentStateID = newState;
            }

            switch (currentStateID)
            {
                case StateID.SPLASH:
                    currentState = splash;
                    break;

                case StateID.PLAY:
                    currentState = play;
                    break;
            }
        }
    }
}
