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
    public class KeyboardObserver : ObjectObserver
    {
        KeyboardState _oldState = new KeyboardState();

        public override void Update(GameTime gt)
        {
            KeyboardState _newState = Keyboard.GetState();

            for (int i = 0; i < Observers.Count; ++i)
            {
                foreach (Keys key in _oldState.GetPressedKeys())
                {
                    if (_newState.IsKeyUp(key))
                        Observers[i].KeyUp(this, key);                    
                }
            }
            _oldState = _newState;
        }
    }
}
