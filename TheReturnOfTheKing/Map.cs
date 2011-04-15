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
    public class Map : VisibleGameEntity
    {
        int _width;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        int _height;

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        int _cols;

        public int Cols
        {
            get { return _cols; }
            set { _cols = value; }
        }

        int _rows;

        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        int _collisionDim;

        public int CollisionDim
        {
            get { return _collisionDim; }
            set { _collisionDim = value; }
        }

        int _startPointX;

        public int StartPointX
        {
            get { return _startPointX; }
            set { _startPointX = value; }
        }

        int _startPointY;

        public int StartPointY
        {
            get { return _startPointY; }
            set { _startPointY = value; }
        }

        int _curCol;
        int _curRow;
        bool _nextCol; // co ve them manh tiep theo ben phai hok
        bool _nextRow; // co ve them manh tiep theo o duoi hok
        int _rpF;

        public int RpF
        {
            get { return _rpF; }
            set { _rpF = value; }
        }

        int _cpF;

        public int CpF
        {
            get { return _cpF; }
            set { _cpF = value; }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //base.Draw(gameTime, sb);
            _sprite[_curRow * _cols + _curCol].Draw(gameTime, sb);            
            for (int i = 0; i <= _cpF; ++i)
                for (int j = 0; j <= _rpF; ++j)
                {
                    if ((_curRow + j)* _cols + _curCol + i < _nsprite)
                        _sprite[(_curRow + j) * _cols + _curCol + i].Draw(gameTime, sb);               
                }
            
        }

        public override void Init(ContentManager content)
        {
            base.Init(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _curCol = (int)Math.Abs(GlobalVariables.dX) / _sprite[0].Texture2D[0].Width;
            _curRow = (int)Math.Abs(GlobalVariables.dY) / _sprite[0].Texture2D[0].Height;
            _nextCol = (Math.Abs(GlobalVariables.dX) % _sprite[0].Texture2D[0].Width) != 0;
            _nextRow = (Math.Abs(GlobalVariables.dY) % _sprite[0].Texture2D[0].Height) != 0;

        }

        public override VisibleGameObject Clone()
        {
            return new Map
            {
                _nsprite = this._nsprite,
                _sprite = this._sprite,
                X = this.X,
                Y = this.Y,
                _height = this.Height,
                _width = this.Width,
                _cols = this._cols,
                _rows = this._rows,
                _curCol = this._curCol,
                _curRow = this._curRow,
                _nextCol = this._nextCol,
                _nextRow = this._nextRow,
                _rpF = this._rpF,
                _cpF = this._cpF,
                _matrix = this._matrix,
                _collisionDim = this._collisionDim,
                _startPointX = this._startPointX,
                _startPointY = this._startPointY
            };
        }
        List<List<bool>> _matrix = new List<List<bool>>();

        public List<List<bool>> Matrix
        {
            get { return _matrix; }
            set { _matrix = value; }
        }

        public bool IsCollision(Rectangle rect)
        {
            /*
            int start_X = rect.X;
            int start_Y = rect.Y;
            int end_X = Math.Min(start_X + rect.Width, GlobalVariables.ScreenWidth);
            int end_Y = Math.Min(start_Y + rect.Height, GlobalVariables.ScreenHeight);

            for (int i = start_X; i < end_X; ++i)
                for (int j = start_Y; j < end_Y; ++j)
                {
                    if (_matrix[j][i] == true)
                        return true;
                }
            */
            return false;
        }
        public Point PointToCell(Point p)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= _width || p.Y >= Height)
                return new Point(0, 0);
            int x = p.X / _collisionDim;
            int y = p.Y / _collisionDim;
            return new Point(x, y);
        }

        public Point CellToPoint(Point p)
        {
            return new Point(p.X * _collisionDim, p.Y * _collisionDim);
        }
    }
}
