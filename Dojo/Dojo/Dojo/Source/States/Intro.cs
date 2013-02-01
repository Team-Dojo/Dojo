using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Dojo.Source.Display;
using Dojo.Source.States;
using Dojo.Source.Resources;

namespace Dojo.Source.States
{
    class Intro : State
    {
        private Texture2D videoTexture;
        private Video video;
        private VideoPlayer videoPlayer;
        private bool active;

        public Intro()
        {
            videoPlayer = new VideoPlayer();
            active = true;
        }

        public override void Unload()
        {
            video = null;
            videoPlayer = null;
            videoTexture = null;
            //base.Unload();
        }

        public override void Update()
        {
            if (Pressed(Buttons.A))
            {
                active = false;
            }

            if(active)
            {
                if (videoPlayer.State == MediaState.Stopped)
                {
                    videoPlayer.Play(video);
                }
            }

            if ((videoPlayer.PlayPosition.Seconds == video.Duration.Seconds) || (!active))
            {
                active = false;
                videoPlayer.Stop();
                Unload();
                GameManager.SwitchState(StateID.SPLASH);
            }

            base.Update();
        }

        public override void Draw()
        {
            if (videoPlayer.State == MediaState.Playing)
            {
                videoTexture = videoPlayer.GetTexture();
            }

            if (videoTexture != null)
            {
                GameManager.spriteBatch.Draw(videoTexture, new Vector2(0,0), Color.White);
            }
        }

        public override void Load()
        {
            video = GameManager.contentManager.Load<Video>("Videos/Intro");
        }
    }
}
