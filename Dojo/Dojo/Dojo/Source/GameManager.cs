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
using Dojo.Source.Framework.Display;
using Dojo.Source.States;
using Dojo.Source.Resources;

namespace Dojo.Source
{
    public class GameManager : Microsoft.Xna.Framework.Game
    {
        // Display
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static ContentManager contentManager;
        // States
        private static int currentStateID = StateID.INTRO;
        private static State currentState = null;
        private static Intro intro;
        private static Splash splash;
        private static Play play;
        // Audio
        private static Song music;
        private static Song introMusic;
        private static Song playMusic;
        private static bool songChanged;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = Content;

            intro = new Intro();
            splash = new Splash();
            play = new Play();
        }

        protected override void Initialize()
        {
            // Configure display
            graphics.PreferredBackBufferWidth = Program.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = Program.SCREEN_HEIGHT;
            graphics.IsFullScreen = false; // false = debug, true = release
            graphics.ApplyChanges();

            // Initialise states
            play.Init();
            splash.Init();
            intro.Init();

            SwitchState(currentStateID);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load fonts
            Formats.Init();

            // Load music
            introMusic = Content.Load<Song>("Audio/Music/Intro");
            playMusic = Content.Load<Song>("Audio/Music/Play");
            music = introMusic;

            // Load state content
            splash.Load();
            play.Load();
            intro.Load();
        }

        protected override void UnloadContent()
        {
            //currentState.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                UnloadContent();
                this.Exit();
            }
            if ((GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed))
            {
                UnloadContent();
                this.Exit();
            }

            currentState.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            if (songChanged)
            {
                MediaPlayer.Stop();
                songChanged = false;
                System.Console.WriteLine("song changed");
            }
            if ((MediaPlayer.State != MediaState.Playing))
            {
                MediaPlayer.Play(music);
                
                System.Console.WriteLine(music);
            }
            currentState.Draw();
            GameManager.spriteBatch.DrawString(Formats.arialTiny, Program.BUILD, new Vector2(Program.SCREEN_WIDTH - 90, Program.SCREEN_HEIGHT - 20), Color.DarkGray);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void SwitchState(int newState)
        {
            // If the new ID is not the current ID, then update the value
            if (newState != currentStateID)
            {
                currentStateID = newState;
            }

            // Change current state based on the ID
            switch (currentStateID)
            {
                case StateID.INTRO:
                    music = introMusic;
                    songChanged = true;
                    currentState = intro;
                    break;

                case StateID.SPLASH:
                    currentState = splash;
                    break;

                case StateID.PLAY:
                    music = playMusic;
                    songChanged = true;
                    currentState = play;
                    break;
            }
        }
    }
}
