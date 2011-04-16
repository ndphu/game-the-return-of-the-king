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
    public class CharacterManager : GameObjectManager
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
                _prototype = new VisibleGameObject[_nprototype];
                XmlDocument doc = new XmlDocument();
                doc.Load(@"data\character\Kunkka.xml");
                _prototype[0] = new Character();
                _prototype[0]._nsprite = 24;
                _prototype[0]._sprite = new GameSprite[_prototype[0]._nsprite];
                
                for (int i = 0; i < _nprototype; ++i)
                {
                    XmlNode node = doc.SelectSingleNode(@"Character/Stand");
                    GameSprite[] temp = LoadSprites(node, content);
                    for (int j = 0; j < 8; ++j)
                        _prototype[0]._sprite[j] = temp[j];
                    node = doc.SelectSingleNode(@"Character/Move");
                    temp = LoadSprites(node, content);
                    for (int j = 8; j < 16; ++j)
                        _prototype[0]._sprite[j] = temp[j - 8];
                    node = doc.SelectSingleNode(@"Character/Attack");
                    temp = LoadSprites(node, content);
                    for (int j = 16; j < 24; ++j)
                        _prototype[0]._sprite[j] = temp[j - 16];
                    ((Character)_prototype[0]).CellToMove = new List<Point>();
                    ((Character)_prototype[0]).DestPoint = new Point();
                    ((Character)_prototype[0]).IsMoving = false;
                    ((Character)_prototype[0]).Map = null;
                    ((Character)_prototype[0]).Speed = int.Parse(doc.SelectSingleNode(@"Character/Speed").InnerText);
                    ((Character)_prototype[0]).X = 0;
                    ((Character)_prototype[0]).Y = 0;
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        GameSprite[] LoadSprites(XmlNode node, ContentManager content)
        {
            try
            {
                int xoffset = int.Parse(node.SelectSingleNode(@"XOffset").InnerText);
                int yoffset = int.Parse(node.SelectSingleNode(@"YOffset").InnerText);
                int numofframe = int.Parse(node.SelectSingleNode(@"NumOfFrames").InnerText);
                string contentName = node.SelectSingleNode("ContentName").InnerText;
                GameSprite[] sprite = new GameSprite[8];
                for (int i = 0; i < 8; ++i)
                {
                    Texture2D[] textures = new Texture2D[numofframe];
                    for (int j = 0; j < numofframe; ++j)
                    {
                        textures[j] = content.Load<Texture2D>(contentName + i.ToString("00") + "-" + j.ToString("00"));
                    }
                    sprite[i] = new GameSprite(textures, 0, 0);
                    sprite[i].Xoffset = xoffset;
                    sprite[i].Yoffset = yoffset;
                    sprite[i].NDelay = 3;
                }
                return sprite;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
