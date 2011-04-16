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
        CharacterManager _charManager = new CharacterManager();
        Character _char;
        Frog _frog;
        public override void InitState(ContentManager content, MainGame owner)
        {
            base.InitState(content, owner);
            _mapManager.InitPrototypes(content, @"Data\Map\map01.xml");
            _map = (Map)_mapManager.CreateObject(0);

            _charManager.InitPrototypes(content, @"data\character\character.xml");

            _char = (Character)_charManager.CreateObject(1);
            _char.SetMap(_map);
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
            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (ms.X < GlobalVariables.ScreenWidth && ms.Y < GlobalVariables.ScreenHeight && ms.X >= 0 && ms.Y >= 0)
                {
                    Point newCell = _map.PointToCell(new Point((int)GlobalVariables.GameCursor.X, (int)GlobalVariables.GameCursor.Y));
                    if (_map.Matrix[newCell.Y][newCell.X] == true)
                        _char.CellToMove = Utility.FindPath(_map.Matrix, _map.PointToCell(new Point((int)_char.X, (int)_char.Y)), newCell);
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
            _char.Draw(gameTime, sb);
            _frog.Draw(gameTime, sb);
            GlobalVariables.GameCursor.Draw(gameTime, sb);
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        
    }
}
