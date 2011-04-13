using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace TheReturnOfTheKing
{
    public class MouseObserver : ObjectObserver
    {
        MouseState _oldState = new MouseState();

        public override void Update(GameTime gt)
        {
            MouseState _newState = Mouse.GetState();

            for (int i = 0; i < Observers.Count; ++i)
            {
                if (Observers[i].Rect.Contains(new Point(_newState.X, _newState.Y)))
                {
                    Observers[i].MouseEnter(this);
                    if (_oldState.LeftButton == ButtonState.Pressed && _newState.LeftButton == ButtonState.Released)
                        Observers[i].MouseClick(this);
                }
                else
                    Observers[i].MouseLeave(this);                
            }

            _oldState = _newState;
        }
    }
}
