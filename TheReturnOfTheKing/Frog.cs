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
    public class Frog : Misc
    {
        Character _character;

        public Character Character
        {
            get { return _character; }
            set { _character = value; }
        }
        public override float X
        {
            get
            {
                return base.X;
            }
            set
            {
                base.X = value;
                for (int i = 0; i < _nsprite; ++i)
                    _sprite[i].X = value;
            }
        }

        public override float Y
        {
            get
            {
                return base.Y;
            }
            set
            {
                base.Y = value;
                for (int i = 0; i < _nsprite; ++i)
                    _sprite[i].Y = value;
            }
        }

        public override void Init(ContentManager content)
        {
            _nsprite = 1;
            _sprite = new GameSprite[1];
            _sprite[0] = new GameSprite(content.Load<Texture2D>("img/misc/frog/frog"), 0, 0);            
        }

        public void SetCharacter(Character _char)
        {
            _character = _char;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            X = _character.X - GlobalVariables.ScreenWidth;
            Y = _character.Y - GlobalVariables.ScreenHeight;
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //base.Draw(gameTime, sb);
            sb.Draw(_sprite[0].Texture2D[0], new Vector2(X + GlobalVariables.dX, Y + GlobalVariables.dY), Color.White);
        }
    }
}
