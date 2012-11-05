﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dojo.Source.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dojo.Source.UI
{
    class ErrorBox
    {
        private string msg;
        private int msgLength;
        private SpriteFont style;
        private Vector2 size;
        private Vector2 position;

        public ErrorBox(string _msg, SpriteFont _style)
        {
            msg = _msg;
            style = _style;
            size = new Vector2(style.MeasureString(msg).X, style.MeasureString(msg).Y);
            position = new Vector2((Program.baseScreenSize.X / 2) - (size.X / 2), (Program.baseScreenSize.Y / 2) - (size.Y / 2));
        }

        public void Draw()
        {
            GameManager.spriteBatch.DrawString(style, msg, position, Color.White);
        }
    }
}
