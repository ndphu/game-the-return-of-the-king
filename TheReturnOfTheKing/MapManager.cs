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
using System.Xml;
using System.IO;

namespace TheReturnOfTheKing
{
    public class MapManager : GameObjectManager
    {
        public override VisibleGameObject CreateObject(int idx)
        {
            return base.CreateObject(idx);
        }
        public override bool InitPrototypes(ContentManager content, string fileName)
        {
            try
            {
                _nprototype = 1;
                _prototype = new Map[_nprototype];
                _prototype[0] = new Map();
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                int _cols = int.Parse(doc.SelectSingleNode("//Width").InnerText);
                int _rows = int.Parse(doc.SelectSingleNode("//Height").InnerText);
                int _pieceWidth = int.Parse(doc.SelectSingleNode("//PieceWidth").InnerText);
                int _pieceHeight = int.Parse(doc.SelectSingleNode("//PieceHeight").InnerText);
                int _npiece = _prototype[0]._nsprite = _cols * _rows;
                _prototype[0]._sprite = new GameSprite[_npiece];
                string contentName = doc.SelectSingleNode("//ContentName").InnerText;
                for (int i = 0; i < _npiece; ++i)
                {
                    _prototype[0]._sprite[i] = new GameSprite(content.Load<Texture2D>(contentName + i.ToString("0000")),(i % _cols) * _pieceWidth,(i / _cols) * _pieceHeight);                
                }
                ((Map)_prototype[0]).Width = _prototype[0]._sprite[0].Texture2D[0].Width * _cols;
                ((Map)_prototype[0]).Height = _prototype[0]._sprite[0].Texture2D[0].Height * _rows;
                ((Map)_prototype[0]).Cols = _cols;
                ((Map)_prototype[0]).Rows = _rows;
                ((Map)_prototype[0]).RpF = GlobalVariables.ScreenHeight / _pieceHeight + 1;
                ((Map)_prototype[0]).CpF = GlobalVariables.ScreenWidth / _pieceWidth + 1;
                ((Map)_prototype[0]).StartPointX = int.Parse(doc.SelectSingleNode("//StartPointX").InnerText);
                ((Map)_prototype[0]).StartPointY = int.Parse(doc.SelectSingleNode("//StartPointY").InnerText);
             
                string collisionName = doc.SelectSingleNode("//Collision").InnerText;
                List<List<bool>> matrix = new List<List<bool>>();
                int collisionUnitDim = int.Parse(doc.SelectSingleNode("//CollisionUnitDim").InnerText);
                int collisionMatrixWith = (int)((Map)_prototype[0]).Width / collisionUnitDim;
                int collisionMatrixHeight = (int)((Map)_prototype[0]).Height / collisionUnitDim;                
                FileStream f = File.OpenRead(collisionName);
                
                List<bool> temp = new List<bool>();
                while (true)
                {
                    int i = f.ReadByte();
                    if (i == -1)
                        break;
                    if (i == '\r' || i == ' ' || i == '\n')
                        continue;
                    if (i == '1')
                        temp.Add(true);
                    else
                        temp.Add(false);
                    if (temp.Count == collisionMatrixWith)
                    {
                        matrix.Add(temp);
                        temp = new List<bool>();
                    }
                }                
                ((Map)_prototype[0]).Matrix = matrix;
                ((Map)_prototype[0]).CollisionDim = collisionUnitDim;

                XmlNodeList Monsters = doc.SelectNodes(@"//Monster");
                ((Map)_prototype[0]).LstMonster = new List<Monster>();
                for (int i = 0; i < Monsters.Count; ++i)
                {
                    Monster mst = (Monster)GlobalVariables.MonsterManager.CreateObject(int.Parse(Monsters[i].SelectSingleNode(@"Type").InnerText));                    
                    ((Map)_prototype[0]).LstMonster.Add(mst);
                    ((Map)_prototype[0]).LstMonster[i].X = int.Parse(Monsters[i].SelectSingleNode(@"X").InnerText) * collisionUnitDim;
                    ((Map)_prototype[0]).LstMonster[i].Y = int.Parse(Monsters[i].SelectSingleNode(@"Y").InnerText) * collisionUnitDim;
                    ((Map)_prototype[0]).LstMonster[i].DestPoint = new Point((int)mst.X, (int)mst.Y);
                    ((Map)_prototype[0]).LstMonster[i].CellToMove = new List<Point>();
                    ((Map)_prototype[0]).LstMonster[i].IsMoving = false;
                    ((Map)_prototype[0]).LstMonster[i].SetMap((Map)_prototype[0]);
                }

                 return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
