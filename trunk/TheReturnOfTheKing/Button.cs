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
    public class Button : Dialog
    {
        bool _isClicked = false;
        float _clickedAnimateOffsetX;

        public float ClickedAnimateOffsetX
        {
            get { return _clickedAnimateOffsetX; }
            set { _clickedAnimateOffsetX = value; }
        }
        float _clickedAnimateOffsetY;

        public float ClickedAnimateOffsetY
        {
            get { return _clickedAnimateOffsetY; }
            set { _clickedAnimateOffsetY = value; }
        }

        public override VisibleGameObject Clone()
        {
            return new Button
            {
                _sprite = this._sprite,
                _nsprite = this._nsprite,
                IsMouseHover = this.IsMouseHover,
                Width = this.Width,
                Height = this.Height,
                Rect = this.Rect,
                _clickedAnimateOffsetX = this._clickedAnimateOffsetX,
                _clickedAnimateOffsetY = this._clickedAnimateOffsetY
            };
        }

        public override float X
        {
            get
            {
                return base.X;
            }
            set
            {
                _x = value;
                for (int i = 0; i < base._nsprite - 1; ++i)
                    _sprite[i].X = value;
                _sprite[_nsprite - 1].X = value - _clickedAnimateOffsetX;
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
                _y = value;
                for (int i = 0; i < base._nsprite - 1; ++i)
                    _sprite[i].Y = value;
                _sprite[_nsprite - 1].Y = value - _clickedAnimateOffsetY;
            }
        }

        public override void Update(GameTime gameTime)
        {
            _sprite[1].Itexture2D = (_sprite[1].Itexture2D + 1) % _sprite[1].Ntexture2D;
            _sprite[2].Itexture2D = (_sprite[2].Itexture2D + 1) % _sprite[2].Ntexture2D;
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            _sprite[0].Draw(gameTime, sb);
            if (IsMouseHover == true)
                _sprite[1].Draw(gameTime, sb);
            if (_isClicked)
                _sprite[2].Draw(gameTime, sb);
        }

        public override void MouseEnter(MouseObserver mo)
        {
            IsMouseHover = true;
            if (_owner != null)
                _owner.ChildNotify(this);
        }

        public override void MouseLeave(MouseObserver mo)
        {
            IsMouseHover = false;
        }

        public override void MouseDownHandler(MouseObserver mo)
        {
            
        }

        public override void MouseUpHandler(MouseObserver mo)
        {
            
        }

        public override void MouseClick(MouseObserver mo)
        {
            if (_isClicked)
                _isClicked = false;
            else
                _isClicked = true;
            OnMouse_Click(this,null);
        }

        public delegate void OnMouseClickHandler(object sender, EventArgs e);

        public event OnMouseClickHandler Mouse_Click;

        public void OnMouse_Click(Object sender, EventArgs e)
        {
            if (Mouse_Click != null)
            {
                Mouse_Click(sender, e);
            }
        }

    }
}
