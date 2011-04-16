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
    public class Character : VisibleGameObject
    {
        Rectangle _collisionRect;

        public Rectangle CollisionRect
        {
            get { return _collisionRect; }
            set { _collisionRect = value; }
        }

        Map _map;

        public Map Map
        {
            get { return _map; }
            set { _map = value; }
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

        List<Point> cellToMove;

        public List<Point> CellToMove
        {
            get { return cellToMove; }
            set { cellToMove = value; }
        }

        bool _isMoving;

        public bool IsMoving
        {
            get { return _isMoving; }
            set { _isMoving = value; }
        }

        int _speed;

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        int _dir = 0;
        Vector2 _transVector = new Vector2(0,0);
        Point _destPoint;

        public Point DestPoint
        {
            get { return _destPoint; }
            set { _destPoint = value; }
        }
        public Character()
        {
        }
        public void SetMap(Map map)
        {
            X = map.StartPointX * map.CollisionDim;
            Y = map.StartPointY * map.CollisionDim;
            GlobalVariables.dX = Math.Min(-X + GlobalVariables.ScreenWidth / 2, 0);
            GlobalVariables.dY = Math.Min(-Y + GlobalVariables.ScreenHeight / 2, 0);
            _destPoint = new Point((int)X, (int)Y);
            _speed = 4;
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
            if (cellToMove.Count != 0 && !IsMoving)
            {
                UpdateDirection(cellToMove[cellToMove.Count - 1].X * _map.CollisionDim, cellToMove[cellToMove.Count - 1].Y * _map.CollisionDim);
                cellToMove.RemoveAt(cellToMove.Count - 1);
            }

            _sprite[_dir].Update(gameTime);

            if (this.Y == _destPoint.Y && this.X < _destPoint.X)
            {
                this.X += Speed;
                if (this.X > GlobalVariables.ScreenWidth / 2 && GlobalVariables.dX > GlobalVariables.ScreenWidth - _map.Width)
                    GlobalVariables.dX -= Speed;
            }
            else
                if (this.Y > _destPoint.Y && this.X < _destPoint.X)
                {
                    this.X += (float)(Speed / Math.Sqrt(2));                    
                    this.Y -= (float)(Speed / Math.Sqrt(2));
                    if (this.X > GlobalVariables.ScreenWidth / 2 && GlobalVariables.dX > GlobalVariables.ScreenWidth - _map.Width)
                        GlobalVariables.dX -= (float)(Speed / Math.Sqrt(2));
                    if (GlobalVariables.dY < 0 && this.Y < _map.Height - GlobalVariables.ScreenHeight / 2)
                        GlobalVariables.dY += (float)(Speed / Math.Sqrt(2));                    
                }
                else
                    if (this.Y > _destPoint.Y && this.X == _destPoint.X)
                    {
                        this.Y -= Speed;
                        if (GlobalVariables.dY < 0 && this.Y < _map.Height - GlobalVariables.ScreenHeight / 2)
                            GlobalVariables.dY += Speed;
                    }
                    else
                        if (this.Y > _destPoint.Y && this.X > _destPoint.X)
                        {
                            this.X -= (float)(Speed / Math.Sqrt(2));                            
                            this.Y -= (float)(Speed / Math.Sqrt(2));
                            if (GlobalVariables.dX < 0 && this.X < _map.Width - GlobalVariables.ScreenWidth / 2)
                                GlobalVariables.dX += (float)(Speed / Math.Sqrt(2));
                            if (GlobalVariables.dY < 0 && this.Y < _map.Height - GlobalVariables.ScreenHeight / 2)
                                GlobalVariables.dY += (float)(Speed / Math.Sqrt(2));
                        }
                        else
                            if (this.Y == _destPoint.Y && this.X > _destPoint.X)
                            {
                                this.X -= Speed;
                                if (GlobalVariables.dX < 0 && this.X < _map.Width - GlobalVariables.ScreenWidth / 2)
                                    GlobalVariables.dX += Speed;
                            }
                            else
                                if (this.Y < _destPoint.Y && this.X > _destPoint.X)
                                {
                                    this.X -= (float)(Speed / Math.Sqrt(2));
                                    this.Y += (float)(Speed / Math.Sqrt(2));
                                    if (GlobalVariables.dX < 0 && this.X < _map.Width - GlobalVariables.ScreenWidth / 2)
                                        GlobalVariables.dX += (float)(Speed / Math.Sqrt(2));
                                    if (this.Y > GlobalVariables.ScreenHeight / 2 && GlobalVariables.dY > GlobalVariables.ScreenHeight - _map.Height)
                                        GlobalVariables.dY -= (float)(Speed / Math.Sqrt(2));
                                }
                                else
                                    if (this.Y < _destPoint.Y && this.X == _destPoint.X)
                                    {
                                        this.Y += Speed;
                                        if (this.Y > GlobalVariables.ScreenHeight / 2 && GlobalVariables.dY > GlobalVariables.ScreenHeight - _map.Height)
                                            GlobalVariables.dY -= Speed;
                                    }
                                    else
                                        if (this.Y < _destPoint.Y && this.X < _destPoint.X)
                                        {
                                            this.X += (float)(Speed / Math.Sqrt(2));
                                            this.Y += (float)(Speed / Math.Sqrt(2));
                                            if (this.X > GlobalVariables.ScreenWidth / 2 && GlobalVariables.dX > GlobalVariables.ScreenWidth - _map.Width)
                                                GlobalVariables.dX -= (float)(Speed / Math.Sqrt(2));
                                            if (this.Y > GlobalVariables.ScreenHeight / 2 && GlobalVariables.dY > GlobalVariables.ScreenHeight - _map.Height)
                                                GlobalVariables.dY -= (float)(Speed / Math.Sqrt(2));
                                        }
            if (Math.Abs(this.X - _destPoint.X) < Speed / Math.Sqrt(2) && Math.Abs(this.Y - _destPoint.Y) < Speed / Math.Sqrt(2) && IsMoving)
            {
                IsMoving = false;
                if (_dir > 7 && cellToMove.Count == 0)
                    _dir -= 8;
                this.X = _destPoint.X;
                this.Y = _destPoint.Y;                
            }
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

        public override VisibleGameObject Clone()
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
        }
    }
}
