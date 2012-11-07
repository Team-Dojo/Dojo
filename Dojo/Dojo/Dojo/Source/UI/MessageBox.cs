using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dojo.Source.UI
{
    class MessageBox
    {
        private string msg;
        private SpriteFont style;
        private Vector2 size;
        private Vector2 position;

        public MessageBox(string _msg = "", SpriteFont _style = null)
        {
            msg = _msg;
            SetStyle(_style);
        }

        public void SetStyle(SpriteFont _style)
        {
            style = _style;
            size = new Vector2(style.MeasureString(msg).X, style.MeasureString(msg).Y);
            position = new Vector2((Program.SCREEN_WIDTH / 2) - (size.X / 2), (HUD.HEIGHT/2) + (Program.SCREEN_HEIGHT / 2) - (size.Y / 2));
        }

        public void SetMessage(string _msg)
        {
            msg = _msg;
        }

        public void Draw()
        {
            GameManager.spriteBatch.DrawString(style, msg, new Vector2(position.X + 5, position.Y + 5), Color.Black);
            GameManager.spriteBatch.DrawString(style, msg, position, Color.White);
        }
    }
}
