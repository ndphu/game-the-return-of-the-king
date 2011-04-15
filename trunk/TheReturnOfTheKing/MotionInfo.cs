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
    public class MotionInfo
    {
        //Ai là người có thông tin này?
        private VisibleGameObject _owner;

        public VisibleGameObject Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        //Hướng di chuyển đầu tiên của vật thể
        private string _firstDection;

        public string FirstDection
        {
            get { return _firstDection; }
            set {
                _firstDection = value;
                _nowDirection = value;
            }
        }

        //Hướng di chuyển hiện tại
        private string _nowDirection;

        public string NowDirection
        {
            get { return _nowDirection; }
            set { _nowDirection = value; }
        }

        //Tọa độ mặt đất đứng yên.
        //Phụ thuộc thuộc vào hướng di chuyển
        //  -Nếu là chuyển động ngang thì đây là tọa độ X của màn hình
        //  -Nếu là chuyển động đứng thì đây là tọa độ Y của màn hình
        //Nếu không có standingGround (không có tưng tưng) thì giá trị này = float.MinValue
        private float _standingGround;

        public float StandingGround
        {
            get { return _standingGround; }
            set { _standingGround = value; }
        }

        //Vector vận tốc
        private Vector2 _vel;

        public Vector2 Vel
        {
            get { return _vel; }
            set { _vel = value; }
        }

        //Vector gia tốc
        private Vector2 _accel;

        public Vector2 Accel
        {
            get { return _accel; }
            set { _accel = value; }
        }

        //Tỉ lệ giảm tốc
        //Nếu Standing thì = 0;
        private float _decelerationRate;

        public float DecelerationRate
        {
            get { return _decelerationRate; }
            set { _decelerationRate = value; }
        }

        //Có đang đứng yên hay chuyển động.
        private bool _isStanding;

        public bool IsStanding
        {
            get { return _isStanding; }
            set { _isStanding = value; }
        }

        //Hàm chuyển động, nhận vào vị trí trước đó và trả về vị trí cần cập nhật..
        public Vector2 Move(Vector2 _prePosition)
        {
            Vector2 _newPosition = _prePosition;

            if (_standingGround != float.MinValue)
            {
                switch (_firstDection)
                {
                    case "Right":
                        {
                            #region
                            if (_nowDirection == "Right")
                            {
                                _vel -= _accel;
                                _newPosition += _vel;
                                if (_vel.X <= 0)
                                {
                                    _nowDirection = "Left";
                                    _vel.X = 0;
                                }
                            }
                            else
                            {
                                _vel += _accel;
                                _newPosition -= _vel;
                                if (_newPosition.X <= _standingGround)
                                {
                                    if (_vel.X <= _accel.X)
                                    {
                                        _isStanding = true;
                                        _newPosition.X = _standingGround;
                                    }
                                    _newPosition.X = _standingGround;
                                    _nowDirection = "Right";
                                    _vel *= _decelerationRate;
                                }
                            }
                            #endregion
                            break;
                        }
                    case "Left":
                        {
                            break;
                        }
                    case "Up":
                        {
                            break;
                        }
                    case "Down":
                        {
                            #region
                            if (_nowDirection == "Down")
                            {
                                _vel -= _accel;
                                _newPosition += _vel;
                                if (_vel.Y <= 0)
                                {
                                    _nowDirection = "Up";
                                    _vel.Y = 0;
                                }
                            }
                            else
                            {
                                _vel += _accel;
                                _newPosition -= _vel;
                                if (_newPosition.Y <= _standingGround)
                                {
                                    if (_vel.Y <= _accel.Y)
                                    {
                                        _isStanding = true;
                                        _newPosition.Y = _standingGround;
                                    }
                                    _newPosition.Y = _standingGround;
                                    _nowDirection = "Down";
                                    _vel *= _decelerationRate;
                                }
                            }
                            #endregion
                            break;
                        }
                }
            }
            else
            {
                switch (_firstDection)
                {
                    case "Right":
                        {
                            #region
                            if (_nowDirection == "Right")
                            {
                                _vel -= _accel;
                                _newPosition += _vel;
                                if (_vel.X <= 0)
                                {
                                    _nowDirection = "Left";
                                    _vel = new Vector2(30, 0);

                                }
                            }
                            else
                            {
                                _vel += _accel;
                                _newPosition -= _vel;
                                if (_newPosition.X < _owner.Width * -1.5)
                                {
                                    _isStanding = true;
                                }
                            }
                            #endregion
                            break;
                        }
                    case "Left":
                        {
                            break;
                        }
                    case "Up":
                        {
                            break;
                        }
                    case "Down":
                        {
                            break;
                        }
                }
            }

            //Trước khi return thì cập nhật lại Rec
            _owner.Rect = new Rectangle((int)_newPosition.X,
                (int)_newPosition.Y,
                (int)_owner.Width,
                (int)_owner.Height);
            return _newPosition;
        }
    }
}
