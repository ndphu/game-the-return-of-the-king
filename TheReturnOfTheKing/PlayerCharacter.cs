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

        public override void SetMap(Map map)
        {
            base.SetMap(map);
            X = map.StartPointX * map.CollisionDim;
            Y = map.StartPointY * map.CollisionDim;
            GlobalVariables.dX = Math.Min(-X + GlobalVariables.ScreenWidth / 2, 0);
            GlobalVariables.dY = Math.Min(-Y + GlobalVariables.ScreenHeight / 2, 0);
            DestPoint = new Point((int)X, (int)Y);   
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
                HitFrame = this.HitFrame,
            };
        }
        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
            if (IsAttacking)
            {
                if (_sprite[Dir].Itexture2D == HitFrame && _sprite[Dir].Check == 0)
                    this.Hit();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsAttacking != true)
            {
                if (this.Y == DestPoint.Y && this.X < DestPoint.X)
                {
                    if (this.X > GlobalVariables.ScreenWidth / 2 && GlobalVariables.dX > GlobalVariables.ScreenWidth - Map.Width)
                        GlobalVariables.dX -= Speed;
                }
                else
                    if (this.Y > DestPoint.Y && this.X < DestPoint.X)
                    {
                        if (this.X > GlobalVariables.ScreenWidth / 2 && GlobalVariables.dX > GlobalVariables.ScreenWidth - Map.Width)
                            GlobalVariables.dX -= (float)(Speed / Math.Sqrt(2));
                        if (GlobalVariables.dY < 0 && this.Y < Map.Height - GlobalVariables.ScreenHeight / 2)
                            GlobalVariables.dY += (float)(Speed / Math.Sqrt(2));
                    }
                    else
                        if (this.Y > DestPoint.Y && this.X == DestPoint.X)
                        {
                            if (GlobalVariables.dY < 0 && this.Y < Map.Height - GlobalVariables.ScreenHeight / 2)
                                GlobalVariables.dY += Speed;
                        }
                        else
                            if (this.Y > DestPoint.Y && this.X > DestPoint.X)
                            {
                                if (GlobalVariables.dX < 0 && this.X < Map.Width - GlobalVariables.ScreenWidth / 2)
                                    GlobalVariables.dX += (float)(Speed / Math.Sqrt(2));
                                if (GlobalVariables.dY < 0 && this.Y < Map.Height - GlobalVariables.ScreenHeight / 2)
                                    GlobalVariables.dY += (float)(Speed / Math.Sqrt(2));
                            }
                            else
                                if (this.Y == DestPoint.Y && this.X > DestPoint.X)
                                {
                                    if (GlobalVariables.dX < 0 && this.X < Map.Width - GlobalVariables.ScreenWidth / 2)
                                        GlobalVariables.dX += Speed;
                                }
                                else
                                    if (this.Y < DestPoint.Y && this.X > DestPoint.X)
                                    {
                                        if (GlobalVariables.dX < 0 && this.X < Map.Width - GlobalVariables.ScreenWidth / 2)
                                            GlobalVariables.dX += (float)(Speed / Math.Sqrt(2));
                                        if (this.Y > GlobalVariables.ScreenHeight / 2 && GlobalVariables.dY > GlobalVariables.ScreenHeight - Map.Height)
                                            GlobalVariables.dY -= (float)(Speed / Math.Sqrt(2));
                                    }
                                    else
                                        if (this.Y < DestPoint.Y && this.X == DestPoint.X)
                                        {
                                            if (this.Y > GlobalVariables.ScreenHeight / 2 && GlobalVariables.dY > GlobalVariables.ScreenHeight - Map.Height)
                                                GlobalVariables.dY -= Speed;
                                        }
                                        else
                                            if (this.Y < DestPoint.Y && this.X < DestPoint.X)
                                            {
                                                if (this.X > GlobalVariables.ScreenWidth / 2 && GlobalVariables.dX > GlobalVariables.ScreenWidth - Map.Width)
                                                    GlobalVariables.dX -= (float)(Speed / Math.Sqrt(2));
                                                if (this.Y > GlobalVariables.ScreenHeight / 2 && GlobalVariables.dY > GlobalVariables.ScreenHeight - Map.Height)
                                                    GlobalVariables.dY -= (float)(Speed / Math.Sqrt(2));
                                            }
            }

            if (Target != null && (Target.IsDying || Target.IsDyed))
            {
                Target = null;
                CellToMove = new List<Point>();
                DestPoint = new Point((int)this.X,(int)this.Y);
                _sprite[Dir].Itexture2D = 0;
                IsStanding = true;
            }
        }
        
    }
}
