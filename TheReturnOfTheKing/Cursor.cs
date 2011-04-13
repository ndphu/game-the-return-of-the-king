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
    public class Cursor : Misc
    {
        public override void Init(ContentManager content)
        {
            _nsprite = 1;
            _sprite = new GameSprite[1];
            _sprite[0] = new GameSprite(content.Load<Texture2D>("img/misc/cursor"), 0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            _sprite[0].X = ms.X;
            _sprite[0].Y = ms.Y;
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }
    }
}
