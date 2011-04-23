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
using System.Xml;

namespace TheReturnOfTheKing
{
    public class GameSprite
    {
        private int _nDelay = 5;

        public int NDelay
        {
            get { return _nDelay; }
            set { _nDelay = value; }
        }

        int _check;

        public int Check
        {
            get { return _check; }
            set { _check = value; }
        }

        Texture2D[] _texture2D;

        public Texture2D[] Texture2D
        {
            get { return _texture2D; }
            set { _texture2D = value; }
        }
               

        float _x;

        public virtual float X
        {
            get { return _x; }
            set { _x = value; }
        }


        float _y;

        public virtual float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        int _xoffset;

        public int Xoffset
        {
            get { return _xoffset; }
            set { _xoffset = value; }
        }

        int _yoffset;

        public int Yoffset
        {
            get { return _yoffset; }
            set { _yoffset = value; }
        }

        int _itexture2D;

        public int Itexture2D
        {
            get { return _itexture2D; }
            set { _itexture2D = value; }
        }

        int _ntexture2D;

        public int Ntexture2D
        {
            get { return _ntexture2D; }
            set { _ntexture2D = value; }
        }

        public GameSprite(Texture2D[] _inputtexture2D, float _inputx, float _inputy)
        {
            _texture2D = _inputtexture2D;
            _ntexture2D = _texture2D.Length;
            _x = _inputx;
            _y = _inputy;
            _itexture2D = 0;
            _check = NDelay;
        }
        public GameSprite(Texture2D _inputtexture2D, int _inputx, int _inputy)
        {
            _ntexture2D = 1;
            _texture2D = new Texture2D[_ntexture2D];            
            _texture2D[0] = _inputtexture2D;
            _x = _inputx;
            _y = _inputy;
            _itexture2D = 0;
            _check = NDelay;
        }

        public void Update(GameTime gameTime)
        {
            if (_check == 0)
            {
                _itexture2D = (_itexture2D + 1) % _ntexture2D;
                _check = NDelay;
            }
            else
            {
                _check -= 1;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(_texture2D[_itexture2D], new Vector2(_x + GlobalVariables.dX + Xoffset, _y + GlobalVariables.dY + Yoffset), Color.White);
        }

        public GameSprite Clone()
        {
            return new GameSprite(this.Texture2D,this.X,this.Y)
            {
                _check = this._check,
                Itexture2D = this.Itexture2D,
                NDelay = this.NDelay,
                Ntexture2D = this.Ntexture2D,
                Texture2D = this.Texture2D,
                X = this.X,
                Y = this.Y,
                Xoffset = this.Xoffset,
                Yoffset = this.Yoffset,                
            };
        }
    }
}
