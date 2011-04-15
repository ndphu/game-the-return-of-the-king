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

        public Character(Map map,ContentManager content)
        {
            X = map.StartPointX * map.CollisionDim;
            Y = map.StartPointY * map.CollisionDim;
            GlobalVariables.dX = Math.Min(-X + 400,0);
            GlobalVariables.dY = Math.Min(-Y + 300,0);
            _destPoint = new Point((int)X, (int)Y);
            _speed = 4;
            _nsprite = 16;
            _sprite = new GameSprite[_nsprite];
            Texture2D[] _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/stand/D06-(07)-F00" + i.ToString());
            }
            _sprite[0] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[0].Xoffset = -63;
            _sprite[0].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/stand/D05-(02)-F00" + i.ToString());
            }
            _sprite[1] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[1].Xoffset = -63;
            _sprite[1].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/stand/D04-(06)-F00" + i.ToString());
            }
            _sprite[2] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[2].Xoffset = -63;
            _sprite[2].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/stand/D03-(01)-F00" + i.ToString());
            }
            _sprite[3] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[3].Xoffset = -63;
            _sprite[3].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/stand/D02-(05)-F00" + i.ToString());
            }
            _sprite[4] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[4].Xoffset = -63;
            _sprite[4].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/stand/D01-(00)-F00" + i.ToString());
            }
            _sprite[5] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[5].Xoffset = -63;
            _sprite[5].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/stand/D00-(04)-F00" + i.ToString());
            }
            _sprite[6] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[6].Xoffset = -63;
            _sprite[6].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/stand/D07-(03)-F00" + i.ToString());
            }
            _sprite[7] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[7].Xoffset = -63;
            _sprite[7].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/move/D06-(07)-F00" + i.ToString());
            }
            _sprite[8] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[8].Xoffset = -60;
            _sprite[8].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/move/D05-(02)-F00" + i.ToString());
            }
            _sprite[9] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[9].Xoffset = -60;
            _sprite[9].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/move/D04-(06)-F00" + i.ToString());
            }
            _sprite[10] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[10].Xoffset = -60;
            _sprite[10].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/move/D03-(01)-F00" + i.ToString());
            }
            _sprite[11] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[11].Xoffset = -60;
            _sprite[11].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/move/D02-(05)-F00" + i.ToString());
            }
            _sprite[12] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[12].Xoffset = -60;
            _sprite[12].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/move/D01-(00)-F00" + i.ToString());
            }
            _sprite[13] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[13].Xoffset = -60;
            _sprite[13].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/move/D00-(04)-F00" + i.ToString());
            }
            _sprite[14] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[14].Xoffset = -60;
            _sprite[14].Yoffset = -87;
            _texture = new Texture2D[8];
            for (int i = 0; i < 8; ++i)
            {
                _texture[i] = content.Load<Texture2D>("img/character/move/D07-(03)-F00" + i.ToString());
            }
            _sprite[15] = new GameSprite(_texture, (int)X, (int)Y);
            _sprite[15].Xoffset = -60;
            _sprite[15].Yoffset = -87;
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
    }
}
