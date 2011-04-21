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
    public class PlayerCharacter : Character
    {
        /// <summary>
        /// Điểm kinh nghiệm
        /// </summary>
        int _xp;

        public int Xp
        {
            get { return _xp; }
            set { _xp = value; }
        }

        public override VisibleGameObject Clone()
        {
            GameSprite[] _spriteTemp = new GameSprite[_nsprite];
            for (int i = 0; i < _nsprite; ++i)
                _spriteTemp[i] = _sprite[i].Clone();
            return new PlayerCharacter
            {
                _nsprite = this._nsprite,
                _sprite = _spriteTemp,
                Attack = this.Attack,
                AttackSpeed = this.AttackSpeed,
                CellToMove = this.CellToMove,
                CollisionRect = this.CollisionRect,
                CriticalRate = this.CriticalRate,
                Defense = this.Defense,
                DestPoint = this.DestPoint,
                Dir = this.Dir,
                Height = this.Height,
                Hp = this.Hp,
                IsAttacking = this.IsAttacking,
                IsMouseHover = this.IsMouseHover,
                IsMoving = this.IsMoving,
                Map = this.Map,
                Mp = this.Mp,
                Range = this.Range,
                Rect = this.Rect,
                Speed = this.Speed,
                Width = this.Width,
                X = this.X,
                Xp = this.Xp,
                Y = this.Y,
            };
        }

        public override void Update(GameTime gameTime)
        {
            if (CellToMove.Count != 0 && !IsMoving)
            {
                UpdateDirection(CellToMove[CellToMove.Count - 1].X * Map.CollisionDim, CellToMove[CellToMove.Count - 1].Y * Map.CollisionDim);
                CellToMove.RemoveAt(CellToMove.Count - 1);
            }

            _sprite[Dir].Update(gameTime);

            if (this.Y == DestPoint.Y && this.X < DestPoint.X)
            {
                this.X += Speed;
                if (this.X > GlobalVariables.ScreenWidth / 2 && GlobalVariables.dX > GlobalVariables.ScreenWidth - Map.Width)
                    GlobalVariables.dX -= Speed;
            }
            else
                if (this.Y > DestPoint.Y && this.X < DestPoint.X)
                {
                    this.X += (float)(Speed / Math.Sqrt(2));
                    this.Y -= (float)(Speed / Math.Sqrt(2));
                    if (this.X > GlobalVariables.ScreenWidth / 2 && GlobalVariables.dX > GlobalVariables.ScreenWidth - Map.Width)
                        GlobalVariables.dX -= (float)(Speed / Math.Sqrt(2));
                    if (GlobalVariables.dY < 0 && this.Y < Map.Height - GlobalVariables.ScreenHeight / 2)
                        GlobalVariables.dY += (float)(Speed / Math.Sqrt(2));
                }
                else
                    if (this.Y > DestPoint.Y && this.X == DestPoint.X)
                    {
                        this.Y -= Speed;
                        if (GlobalVariables.dY < 0 && this.Y < Map.Height - GlobalVariables.ScreenHeight / 2)
                            GlobalVariables.dY += Speed;
                    }
                    else
                        if (this.Y > DestPoint.Y && this.X > DestPoint.X)
                        {
                            this.X -= (float)(Speed / Math.Sqrt(2));
                            this.Y -= (float)(Speed / Math.Sqrt(2));
                            if (GlobalVariables.dX < 0 && this.X < Map.Width - GlobalVariables.ScreenWidth / 2)
                                GlobalVariables.dX += (float)(Speed / Math.Sqrt(2));
                            if (GlobalVariables.dY < 0 && this.Y < Map.Height - GlobalVariables.ScreenHeight / 2)
                                GlobalVariables.dY += (float)(Speed / Math.Sqrt(2));
                        }
                        else
                            if (this.Y == DestPoint.Y && this.X > DestPoint.X)
                            {
                                this.X -= Speed;
                                if (GlobalVariables.dX < 0 && this.X < Map.Width - GlobalVariables.ScreenWidth / 2)
                                    GlobalVariables.dX += Speed;
                            }
                            else
                                if (this.Y < DestPoint.Y && this.X > DestPoint.X)
                                {
                                    this.X -= (float)(Speed / Math.Sqrt(2));
                                    this.Y += (float)(Speed / Math.Sqrt(2));
                                    if (GlobalVariables.dX < 0 && this.X < Map.Width - GlobalVariables.ScreenWidth / 2)
                                        GlobalVariables.dX += (float)(Speed / Math.Sqrt(2));
                                    if (this.Y > GlobalVariables.ScreenHeight / 2 && GlobalVariables.dY > GlobalVariables.ScreenHeight - Map.Height)
                                        GlobalVariables.dY -= (float)(Speed / Math.Sqrt(2));
                                }
                                else
                                    if (this.Y < DestPoint.Y && this.X == DestPoint.X)
                                    {
                                        this.Y += Speed;
                                        if (this.Y > GlobalVariables.ScreenHeight / 2 && GlobalVariables.dY > GlobalVariables.ScreenHeight - Map.Height)
                                            GlobalVariables.dY -= Speed;
                                    }
                                    else
                                        if (this.Y < DestPoint.Y && this.X < DestPoint.X)
                                        {
                                            this.X += (float)(Speed / Math.Sqrt(2));
                                            this.Y += (float)(Speed / Math.Sqrt(2));
                                            if (this.X > GlobalVariables.ScreenWidth / 2 && GlobalVariables.dX > GlobalVariables.ScreenWidth - Map.Width)
                                                GlobalVariables.dX -= (float)(Speed / Math.Sqrt(2));
                                            if (this.Y > GlobalVariables.ScreenHeight / 2 && GlobalVariables.dY > GlobalVariables.ScreenHeight - Map.Height)
                                                GlobalVariables.dY -= (float)(Speed / Math.Sqrt(2));
                                        }
            if (Math.Abs(this.X - DestPoint.X) < Speed / Math.Sqrt(2) && Math.Abs(this.Y - DestPoint.Y) < Speed / Math.Sqrt(2) && IsMoving)
            {
                IsMoving = false;
                if (Dir > 7 && CellToMove.Count == 0)
                    Dir -= 8;
                this.X = DestPoint.X;
                this.Y = DestPoint.Y;
            }
        }
    }
}
