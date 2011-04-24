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
    public class StateMainGame : GameState
    {
        Map _map;
        MapManager _mapManager = new MapManager();
        PlayerCharacterManager _charManager = new PlayerCharacterManager();        
        List<Monster> _listMonsters = new List<Monster>();        
        PlayerCharacter _char;
        Frog _frog;
        public override void InitState(ContentManager content, MainGame owner)
        {
            base.InitState(content, owner);
            GlobalVariables.MonsterManager = new MonsterManager();
            GlobalVariables.MonsterManager.InitPrototypes(content, @"Data\monster\monster.xml");
            _charManager.InitPrototypes(content, @"Data\character\character.xml");
            _mapManager.InitPrototypes(content, @"Data\Map\map01.xml");
            _map = (Map)_mapManager.CreateObject(0);
            GlobalVariables.MapCollisionDim = _map.CollisionDim;
            _char = (PlayerCharacter)_charManager.CreateObject(0);            
            _char.SetMap(_map);
            _listMonsters = _map.InitMonsterList();

            _frog = new Frog();
            _frog.Init(content);
            _frog.SetCharacter(_char);

        }

        public override void EnterState()
        {
            base.EnterState();
            
        }

        public override void UpdateState(GameTime gameTime)
        {
            base.UpdateState(gameTime);
            _map.Update(gameTime);            
            MouseState ms = Mouse.GetState();

            int _checkMonster = -1; // Kiem tra chuot co dang chi len quai vat hay khong
            for (int i = 0; i < _listMonsters.Count; ++i)
            {
                _listMonsters[i].Update(gameTime);
                if (_listMonsters[i].IsCollisionWith(_char))
                    _listMonsters[i].Target = _char;
                   
                if (_listMonsters[i].CollisionRect.Contains(new Point((int)GlobalVariables.GameCursor.X, (int)GlobalVariables.GameCursor.Y)))
                    _checkMonster = i;
            }
            if (_checkMonster != -1)
                GlobalVariables.GameCursor.IsAttack = true;
            else
                GlobalVariables.GameCursor.IsIdle = true;

            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (ms.X < GlobalVariables.ScreenWidth && ms.Y < GlobalVariables.ScreenHeight && ms.X >= 0 && ms.Y >= 0)
                {
                    if (!GlobalVariables.GameCursor.IsAttack)
                    {
                        Point newCell = _map.PointToCell(new Point((int)GlobalVariables.GameCursor.X, (int)GlobalVariables.GameCursor.Y));
                        if (_map.Matrix[newCell.Y][newCell.X] == true)
                            _char.CellToMove = Utility.FindPath(_map.Matrix, _map.PointToCell(new Point((int)_char.X, (int)_char.Y)), newCell);
                        _char.Target = null;                        
                    }
                    else
                    {
                        _char.Target = _listMonsters[_checkMonster];                        
                    }
                }
            }
            
            GlobalVariables.GameCursor.Update(gameTime);
   
            _char.Update(gameTime);
            _frog.Update(gameTime);
            
        }

        public override void DrawState(GameTime gameTime, SpriteBatch sb)
        {
            base.DrawState(gameTime, sb);
            _map.Draw(gameTime, sb);
            for (int i = 0; i < _listMonsters.Count; ++i)
            {
                if (_listMonsters[i].Y < _char.Y)
                    _listMonsters[i].Draw(gameTime, sb);
            }
            _char.Draw(gameTime, sb);
            for (int i = 0; i < _listMonsters.Count; ++i)
            {
                if (_listMonsters[i].Y >= _char.Y)
                    _listMonsters[i].Draw(gameTime, sb);
            }
            _frog.Draw(gameTime, sb);

            GlobalVariables.GameCursor.Draw(gameTime, sb);

        }

        public override void ExitState()
        {
            base.ExitState();
        }

        
    }
}
