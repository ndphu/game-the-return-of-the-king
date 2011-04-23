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
        /// Vị trí khung hình tấn công
        /// </summary>
        int _hitFrame;

        public int HitFrame
        {
            get { return _hitFrame; }
            set { _hitFrame = value; }
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
                CollisionRect = new Rectangle((int)X, (int)Y, (int)GlobalVariables.MapCollisionDim, (int)GlobalVariables.MapCollisionDim);
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
                CollisionRect = new Rectangle((int)X, (int)Y, (int)GlobalVariables.MapCollisionDim, (int)GlobalVariables.MapCollisionDim);
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
        public virtual void SetMap(Map map)
        {
            _map = map;
            cellToMove = new List<Point>();
            IsMoving = false;
            _destPoint = new Point((int)X, (int)Y);            
        }
        /// <summary>
        /// Mục tiêu tấn công
        /// </summary>
        Character _target;

        public Character Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            _sprite[_dir].Draw(gameTime, sb);
        }

        public override void Update(GameTime gameTime)
        {            
            if (CellToMove.Count != 0 && !IsMoving)
            {
                UpdateDirection(CellToMove[CellToMove.Count - 1].X * Map.CollisionDim, CellToMove[CellToMove.Count - 1].Y * Map.CollisionDim);
                CellToMove.RemoveAt(CellToMove.Count - 1);
            }            
            _sprite[Dir].Update(gameTime);
            if (IsAttacking)
            {
                Dir = Dir % 8 + 16;
                return;
            }
            if (!IsMoving)
            {
                return;
            }
            else
            {
                UpdateDirection(DestPoint.X, DestPoint.Y);
            }
           
            if (this.Y == DestPoint.Y && this.X < DestPoint.X)
            {
                this.X += Speed;             
            }
            else
                if (this.Y > DestPoint.Y && this.X < DestPoint.X)
                {
                    this.X += (float)(Speed / Math.Sqrt(2));
                    this.Y -= (float)(Speed / Math.Sqrt(2));                    
                }
                else
                    if (this.Y > DestPoint.Y && this.X == DestPoint.X)
                    {
                        this.Y -= Speed;
                        
                    }
                    else
                        if (this.Y > DestPoint.Y && this.X > DestPoint.X)
                        {
                            this.X -= (float)(Speed / Math.Sqrt(2));
                            this.Y -= (float)(Speed / Math.Sqrt(2));
                            
                        }
                        else
                            if (this.Y == DestPoint.Y && this.X > DestPoint.X)
                            {
                                this.X -= Speed;
                            }
                            else
                                if (this.Y < DestPoint.Y && this.X > DestPoint.X)
                                {
                                    this.X -= (float)(Speed / Math.Sqrt(2));
                                    this.Y += (float)(Speed / Math.Sqrt(2));
                             
                                }
                                else
                                    if (this.Y < DestPoint.Y && this.X == DestPoint.X)
                                    {
                                        this.Y += Speed;
                                    }
                                    else
                                        if (this.Y < DestPoint.Y && this.X < DestPoint.X)
                                        {
                                            this.X += (float)(Speed / Math.Sqrt(2));
                                            this.Y += (float)(Speed / Math.Sqrt(2));
                             
                                        }
            if (Math.Abs(this.X - DestPoint.X) < Speed / Math.Sqrt(2) && Math.Abs(this.Y - DestPoint.Y) < Speed / Math.Sqrt(2) && IsMoving)
            {
                IsMoving = false;                
                this.X = DestPoint.X;
                this.Y = DestPoint.Y;
            }           
        }
       
        public void UpdateDirection(double x, double y)
        {
            if (!IsMoving)
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
                Dir = Dir % 8 + 8;
            }
        }

        public bool IsCollisionWith(Character other)
        {
            if (Math.Abs(other.X - this.X) < GlobalVariables.MapCollisionDim * 1 && Math.Abs(other.Y - this.Y) < GlobalVariables.MapCollisionDim * 1)
                return true;    
            return false;              
        }

        public virtual void BeginAttack(Character _char)
        {
            if (IsMoving)
                IsMoving = false;
            if (IsAttacking == false)
            {
                IsAttacking = true;
                _sprite[Dir].Itexture2D = 0;
                CellToMove = new List<Point>();
                float x = _char.X;
                float y = _char.Y;
                if (this.Y == y && this.X < x)
                    Dir = 0;
                if (this.Y > y && this.X < x)
                    Dir = 1;
                if (this.Y > y && this.X == x)
                    Dir = 2;
                if (this.Y > y && this.X > x)
                    Dir = 3;
                if (this.Y == y && this.X > x)
                    Dir = 4;
                if (this.Y < y && this.X > x)
                    Dir = 5;
                if (this.Y < y && this.X == x)
                    Dir = 6;
                if (this.Y < y && this.X < x)
                    Dir = 7;
                Dir = Dir % 8 + 16;
            }
            Target = _char;    
        }

        public virtual void EndAttack(Character _char)
        {
            if (IsAttacking)
            {
                IsAttacking = false;
                _sprite[Dir].Itexture2D = 0;
            }
        }

        public virtual void Hit()
        {
            Random r = new Random();
            if (r.Next(0, 100) < this.CriticalRate)
                Target.BeHit(this.Attack * 2);
            else
                Target.BeHit(this.Attack);
        }

        public virtual void BeHit(int damage)
        {
            Hp -= (damage - this.Defense);
        }
    }
}
