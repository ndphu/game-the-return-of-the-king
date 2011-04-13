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
                doc.Load(@"Data\map03.xml");
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
                
                ((Map)_prototype[0]).MapWidth = _prototype[0]._sprite[0].Texture2D[0].Width * _cols;
                ((Map)_prototype[0]).MapHeight = _prototype[0]._sprite[0].Texture2D[0].Height * _rows;
                ((Map)_prototype[0]).Cols = _cols;
                ((Map)_prototype[0]).Rows = _rows;
                ((Map)_prototype[0]).RpF = GlobalVariables.ScreenHeight / _pieceHeight + 1;
                ((Map)_prototype[0]).CpF = GlobalVariables.ScreenWidth / _pieceWidth + 1;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
