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
    public abstract class VisibleGameObject
    {
        public GameSprite[] _sprite;
        public int _nsprite;

        private bool _isMouseHover = false;

        public bool IsMouseHover
        {
            get { return _isMouseHover; }
            set { _isMouseHover = value; }
        }

        protected float _x;

        public virtual float X
        {
            get { return _x; }
            set 
            {
                _x = value;                
                /*for (int i = 0; i < _nsprite; ++i)
                    _sprite[i].X = _x;*/
            }
        }

        protected float _y;

        public virtual float Y
        {
            get { return _y; }
            set
            {
                _y = value;

                /*for (int i = 0; i < _nsprite; ++i)
                    _sprite[i].Y = _y;*/
            }
        }

        protected float _width;

        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        protected float _height;

        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        protected Rectangle _rect;

        public Rectangle Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }

        public virtual void Init(ContentManager content)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb)
        {
            for (int i = 0; i < _nsprite; ++i)
                _sprite[i].Draw(gameTime, sb);
        }

        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < _nsprite; ++i)
                _sprite[i].Update(gameTime);
        }

        public virtual VisibleGameObject Clone()
        {
            return null;
        }

        public virtual void MouseDownHandler(MouseObserver mo)
        {
            
        }

        public virtual void MouseUpHandler(MouseObserver mo)
        {

        }

        public virtual void MouseEnter(MouseObserver mo)
        {

        }

        public virtual void MouseLeave(MouseObserver mo)
        {

        }

        public virtual void MouseClick(MouseObserver mo)
        {

        }

        public virtual void ChildNotify(VisibleGameObject child)
        {

        }

        public virtual void KeyDown(KeyboardObserver ko, Keys key)
        { 

        }

        public virtual void KeyUp(KeyboardObserver ko, Keys key)
        { 

        }
    }
}
