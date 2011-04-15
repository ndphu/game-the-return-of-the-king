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
    public class Cursor : Misc
    {
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
        private bool _isIdle = true;

        public bool IsIdle
        {
            get { return _isIdle; }
            set { 
                _isIdle = value;
                if (value == true)
                {
                    _isHover = false;
                    _isAttack = false;
                }
            }
        }

        private bool _isHover = false;

        public bool IsHover
        {
            get { return _isHover; }
            set { 
                _isHover = value;
                if (value == true)
                {
                    _isIdle = false;
                    _isAttack = false;
                }
            }
        }

        private bool _isAttack = false;

        public bool IsAttack
        {
            get { return _isAttack; }
            set { 
                _isAttack = value;
                if (value == true)
                {
                    _isHover = false;
                    _isIdle = false;
                }
            }
        }

        public override void Init(ContentManager content)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@".\Data\XML\gamecursor.xml");

                _nsprite = doc.DocumentElement.ChildNodes.Count;
                _sprite = new GameSprite[_nsprite];

                for (int i = 0; i < _nsprite; i++)
                {
                    string contentName = doc.DocumentElement.ChildNodes[i].InnerText;
                    _sprite[i] = new GameSprite(content.Load<Texture2D>(contentName),
                        0,
                        0);
                }
            }
            catch (Exception e)
            {
                return;
            }
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();

            for (int i = 0; i < _nsprite; i++)
            {
                X = ms.X - GlobalVariables.dX;
                Y = ms.Y - GlobalVariables.dY;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (_isIdle)
            {
                _sprite[0].Draw(gameTime, sb);
            }
            else if (_isHover)
            {
                _sprite[1].Draw(gameTime, sb);
            }
            else
            {
                _sprite[2].Draw(gameTime, sb);
            }
        }
    }
}
