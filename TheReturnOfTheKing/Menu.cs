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
    public class Menu : Dialog
    {
        GameState _stateOwner;

        public GameState StateOwner
        {
            get { return _stateOwner; }
            set { _stateOwner = value; }
        }

        List<Button> _child = new List<Button>();

        public List<Button> Child
        {
            get { return _child; }
            set { _child = value; }
        }

        public MotionInfo _motionInfo;

        private int _delayTime = 0;

        public int DelayTime
        {
            get { return _delayTime; }
            set { _delayTime = value; }
        }

        private int _iDelayTime = 0;

        public int IDelayTime
        {
            get { return _iDelayTime; }
            set { _iDelayTime = value; }
        }

        /*int _menuIndex = 0;

        public int MenuIndex
        {
            get { return _menuIndex; }
            set { _menuIndex = value; }
        }*/

        //Color _color = new Color(160,160,160);
        //int _iSign = 2;

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (_iDelayTime == _delayTime)
            {
                //sb.Draw(_sprite[0].Texture2D[0], new Vector2(_sprite[0].X, _sprite[0].Y), _color);
                base.Draw(gameTime, sb);
                for (int i = 0; i < _child.Count; ++i)
                    _child[i].Draw(gameTime, sb);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_iDelayTime == _delayTime)
            {
                //Update vị trí cho menu.
                if (!_motionInfo.IsStanding)
                {
                    Vector2 newPos = _motionInfo.Move(new Vector2(X, Y));
                    X = newPos.X;
                    Y = newPos.Y;
                }
            }
            else
                _iDelayTime++;

            //Update vị trí các button
            for (int i = 0; i < _child.Count; ++i)
                _child[i].Update(gameTime);

        }

        public override void Init(ContentManager content)
        {   
            
        }

        public override void MouseEnter(MouseObserver mo)
        {
            /*if (!IsMouseHover)
            {
                IsMouseHover = true;
                for (int i = 0; i < _child.Count; ++i)
                    mo.RegisterObserver(_child[i]);
            }*/
        }

        public override void MouseLeave(MouseObserver mo)
        {
            /*if (IsMouseHover)
            {
                IsMouseHover = false;
                for (int i = 0; i < _child.Count; ++i)
                {
                    _child[i].MouseLeave(mo);
                    mo.UnregisterObserver(_child[i]);
                }
                _color = new Color(160, 160, 160);
                _iSign = 2;
            }*/
        }
        public override VisibleGameObject Clone()
        {
            Menu res = new Menu
            {
                _child = this._child,
                _nsprite = this._nsprite,
                _sprite = this._sprite,
                Width = this.Width,
                Height = this.Height,
                Rect = this.Rect,
                X = this.X,
                Y = this.Y,            
                _motionInfo = this._motionInfo,
                _delayTime = this._delayTime
            };
            for (int i = 0; i < res.Child.Count; ++i)
            {
                res.Child[i].Owner = res;
            }
            return res;
        }

        public override void ChildNotify(VisibleGameObject child)
        {
            //_menuIndex = _child.IndexOf((Button)child);
        }

        public override void KeyUp(KeyboardObserver ko, Keys key)
        {
            /*switch (key)
            {
                case Keys.Up:
                    _menuIndex -= 1;
                    if (_menuIndex < 0)
                        _menuIndex = _child.Count - 1;
                    Mouse.SetPosition((int)(_child[_menuIndex].X + _child[_menuIndex].Width / 2), (int)(_child[_menuIndex].Y + _child[_menuIndex].Height / 2));
                    break;
                case Keys.Down:
                    _menuIndex += 1;
                    if (_menuIndex > _child.Count - 1)
                        _menuIndex = 0;
                    Mouse.SetPosition((int)(_child[_menuIndex].X + _child[_menuIndex].Width / 2), (int)(_child[_menuIndex].Y + _child[_menuIndex].Height / 2));
                    break;
                case Keys.Enter:
                    _child[_menuIndex].MouseClick(null);
                    break;
            }*/
        }
    }
}
