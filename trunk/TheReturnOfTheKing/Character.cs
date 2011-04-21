using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Character : VisibleGameEntity
    {
        /// <summary>
        /// Máu
        /// </summary>
        int _hp;

        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }
        /// <summary>
        /// Mana
        /// </summary>
        int _mp;

        public int Mp
        {
            get { return _mp; }
            set { _mp = value; }
        }
       
        /// <summary>
        /// Bán kính tấn công
        /// </summary>
        int _range;

        public int Range
        {
            get { return _range; }
            set { _range = value; }
        }
        /// <summary>
        /// Tốc độ đánh
        /// Tính từ lúc bắt đầu nhận lệnh tấn công cho đến lúc thực sự gây ra sát thương
        /// </summary>
        int _attackSpeed;

        public int AttackSpeed
        {
            get { return _attackSpeed; }
            set { _attackSpeed = value; }
        }
        /// <summary>
        /// Lực sát thương
        /// </summary>
        int _attack;

        public int Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }
        /// <summary>
        /// Phòng thủ
        /// </summary>
        int _defense;

        public int Defense
        {
            get { return _defense; }
            set { _defense = value; }
        }
        /// <summary>
        /// Tỉ lệ tấn công chí mạng (tính theo %)
        /// </summary>
        int _criticalRate;

        public int CriticalRate
        {
            get { return _criticalRate; }
            set { _criticalRate = value; }
        }

        /// <summary>
        /// Hình chữ nhật để xét va chạm
        /// </summary>
        Rectangle _collisionRect;

        public Rectangle CollisionRect
        {
            get { return _collisionRect; }
            set { _collisionRect = value; }
        }
        /// <summary>
        /// Bản đồ nhân vật ở trên đó
        /// </summary>
        Map _map;

        public Map Map
        {
            get { return _map; }
            set { _map = value; }
        }

        /// <summary>
        /// X
        /// </summary>
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
                CollisionRect = new Rectangle((int)X, (int)Y, (int)this._sprite[0].Texture2D[0].Width, _sprite[0].Texture2D[0].Height);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
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
                CollisionRect = new Rectangle((int)X, (int)Y, (int)this._sprite[0].Texture2D[0].Width, _sprite[0].Texture2D[0].Height);
            }
        }
        /// <summary>
        /// Danh sách các ô còn phải di chuyển tiếp theo
        /// </summary>
        List<Point> cellToMove;

        public List<Point> CellToMove
        {
            get { return cellToMove; }
            set { cellToMove = value; }
        }
        /// <summary>
        /// Đang trong trạng thái di chuyển
        /// </summary>
        bool _isMoving;

        public bool IsMoving
        {
            get { return _isMoving; }
            set { _isMoving = value; }
        }
        /// <summary>
        /// Đang trong trạng thái tấn công
        /// </summary>
        bool _isAttacking;

        public bool IsAttacking
        {
            get { return _isAttacking; }
            set { _isAttacking = value; }
        }
        /// <summary>
        /// Tốc độ di chuyển
        /// </summary>
        int _speed;

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        /// <summary>
        /// Hướng hiện tại
        /// </summary>
        int _dir = 0;

        public int Dir
        {
            get { return _dir; }
            set { _dir = value; }
        }
        /// <summary>
        /// Đích đến
        /// </summary>
        Point _destPoint;

        public Point DestPoint
        {
            get { return _destPoint; }
            set { _destPoint = value; }
        }
        public Character()
        {
        }
        /// <summary>
        /// Gán nhân vật với bản đồ
        /// </summary>
        /// <param name="map">Bản đồ cần gán</param>
        public void SetMap(Map map)
        {
            X = map.StartPointX * map.CollisionDim;
            Y = map.StartPointY * map.CollisionDim;
            GlobalVariables.dX = Math.Min(-X + GlobalVariables.ScreenWidth / 2, 0);
            GlobalVariables.dY = Math.Min(-Y + GlobalVariables.ScreenHeight / 2, 0);
            _destPoint = new Point((int)X, (int)Y);            
            _map = map;
            cellToMove = new List<Point>();
            IsMoving = false;
        }
        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            _sprite[_dir].Draw(gameTime, sb);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
       
        public void UpdateDirection(double x, double y)
        {
            IsMoving = true;
            _destPoint = new Point((int)x, (int)y);
            if (this.Y == y && this.X < x)
                _dir = 0;
            if (this.Y > y && this.X < x)
                _dir = 1;
            if (this.Y > y && this.X == x)
                _dir = 2;
            if (this.Y > y && this.X > x)
                _dir = 3;
            if (this.Y == y && this.X > x)
                _dir = 4;
            if (this.Y < y && this.X > x)
                _dir = 5;
            if (this.Y < y && this.X == x)
                _dir = 6;
            if (this.Y < y && this.X < x)
                _dir = 7;
            if (_dir <= 7)
                _dir += 8;
        }

        /*public override VisibleGameObject Clone()
        {
            return new Character
            {
                CellToMove = this.CellToMove,
                CollisionRect = this.CollisionRect,
                DestPoint = this.DestPoint,
                Height = this.Height,
                Width = this.Width,
                X = this.X,
                Y = this.Y,
                Speed = this.Speed,
                IsMoving = this.IsMoving,
                IsMouseHover = this.IsMouseHover,
                _sprite = this._sprite,
                _nsprite = this._nsprite,
                
            };
        }*/

        public bool IsCollisionWith(Character other)
        {
            return other.CollisionRect.Intersects(this.CollisionRect);                
        }

        public void BeginAttack(Character _char)
        {
            if (_dir < 16)
                _dir += 8;
        }

        public void EndAttack(Character _char)
        {
            if (_dir >= 16)
                _dir -= 8;
        }
    }
}
