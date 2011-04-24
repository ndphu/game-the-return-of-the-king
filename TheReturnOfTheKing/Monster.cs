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
    public class Monster : Character
    {   
        public override VisibleGameObject Clone()
        {
            GameSprite[] _spriteTemp = new GameSprite[_nsprite];
            for (int i = 0; i < _nsprite; ++i)
                _spriteTemp[i] = _sprite[i].Clone();            
            return new Monster
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
                Y = this.Y,                
                HitFrame = this.HitFrame,
                Sight = this.Sight
                Y = this.Y,                
                HitFrame = this.HitFrame
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Target != null && Math.Sqrt(Math.Pow(this.X - Target.X, 2) - Math.Pow(this.Y - Target.Y, 2)) > this.Sight)
                Target = null;
            if (Target != null && IsCollisionWith(Target))
            if (CellToMove.Count == 0)
            {
                CellToMove = new List<Point>();
                UpdateDirection(Target.X, Target.Y);
            }
            if (CellToMove.Count == 0 && Target == null)
            {
                /*if (Target == null)
                {*/
                /*if (Target == null)
                {*/
                    Random r = new Random();
                    if (r.Next(0, 100) < 20)
                    {
                        Point curentPosition = Map.PointToCell(new Point((int)X, (int)Y));
                        Point newPosition = new Point(r.Next(curentPosition.X - 2, curentPosition.X + 2), r.Next(curentPosition.Y - 2, curentPosition.Y + 2));
                        CellToMove = Utility.FindPath(Map.Matrix, curentPosition, newPosition);
                    }
                /*}
                else*/
                {   /*
                    if (this.IsCollisionWith(Target))
                    {
                        CellToMove = new List<Point>();
                        DestPoint = new Point((int)this.X, (int)this.Y);                       
                    }
                    else
                        if (IsMoving == false)
                            CellToMove = Utility.FindPath(Map.Matrix, Map.PointToCell(new Point((int)X, (int)Y)), Map.PointToCell(new Point((int)Target.X, (int)Target.Y)));
                
                     */
                }                
            }            
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

    }
}
