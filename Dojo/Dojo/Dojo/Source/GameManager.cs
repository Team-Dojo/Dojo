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
using Dojo.Source.Entities;
using Dojo.Source.Display;
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
        private static int currentStateID;
        private static State currentState;

        // Audio
        private static Song music;
        private static Song introMusic;
        private static Song playMusic1;
        private static Song playMusic2;
        private static Song playMusic3;
        private static bool songChanged;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = Content;

            currentState = null;
            currentStateID = StateID.INTRO;
        }

        protected override void Initialize()
        {
            // Configure display
            graphics.PreferredBackBufferWidth = Program.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = Program.SCREEN_HEIGHT;
            graphics.IsFullScreen = true; // false = debug, true = release
            graphics.ApplyChanges();

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
            playMusic1 = Content.Load<Song>("Audio/Music/Play1");
            playMusic2 = Content.Load<Song>("Audio/Music/Play2");
            playMusic3 = Content.Load<Song>("Audio/Music/Play3");
            music = introMusic;
        }

        protected override void UnloadContent()
        {
            //currentState.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if ((Keyboard.GetState().IsKeyDown(Keys.Escape)) ||
                (currentState.exit))
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
            GameManager.spriteBatch.DrawString(Formats.arialTiny, Program.BUILD, new Vector2(Program.SCREEN_WIDTH - 100, Program.SCREEN_HEIGHT - 20), Color.DarkGray);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void SwitchState(int newState)
        {
            int args = 1;
            
            if (currentState != null)
            {
                args = currentState.args;
            }

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
                    currentState = new Intro();
                    break;

                case StateID.SPLASH:
                    currentState = new Splash();
                    break;

                case StateID.PLAY:
                    Random rand = new Random();

                    int song = rand.Next(0, 3);

                    switch(song)
                    {
                        case 0:  music = playMusic1; break;
                        case 1:  music = playMusic2; break;
                        case 2:  music = playMusic3; break;
                    }

                    songChanged = true;
                    currentState = new Play(args);
                    break;

                case StateID.MAIN_MENU:
                    currentState = new Menu();
                    break;

                case StateID.CREDITS:
                    currentState = new Credits();
                    break;

                case StateID.SELECT:
                    currentState = new Select();
                    break;
            }

            currentState.Init();
            currentState.Load();
        }
    }
}
