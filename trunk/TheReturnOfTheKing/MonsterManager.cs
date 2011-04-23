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
    public class MonsterManager : GameObjectManager
    {
        public override VisibleGameObject CreateObject(int idx)
        {
            return base.CreateObject(idx);
        }

        public override bool InitPrototypes(ContentManager content, string fileName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNodeList nodelist = doc.SelectNodes(@"//Monster");
                _nprototype = nodelist.Count;
                _prototype = new VisibleGameObject[_nprototype];
                for (int i = 0; i < nodelist.Count; ++i)
                {
                    _prototype[i] = new Monster();
                    _prototype[i]._nsprite = 24;
                    _prototype[i]._sprite = new GameSprite[_prototype[i]._nsprite];
                    XmlNode node = nodelist[i].SelectSingleNode(@"Stand");
                    GameSprite[] temp = Utility.LoadSprites(node, content);
                    for (int j = 0; j < 8; ++j)
                        _prototype[i]._sprite[j] = temp[j];
                    node = nodelist[i].SelectSingleNode(@"Move");
                    temp = Utility.LoadSprites(node, content);
                    for (int j = 8; j < 16; ++j)
                        _prototype[i]._sprite[j] = temp[j - 8];
                    node = nodelist[i].SelectSingleNode(@"Attack");
                    temp = Utility.LoadSprites(node, content);
                    for (int j = 16; j < 24; ++j)
                        _prototype[i]._sprite[j] = temp[j - 16];
                    ((Monster)_prototype[i]).CellToMove = new List<Point>();
                    ((Monster)_prototype[i]).DestPoint = new Point();
                    ((Monster)_prototype[i]).IsMoving = false;
                    ((Monster)_prototype[i]).Map = null;
                    ((Monster)_prototype[i]).Speed = int.Parse(nodelist[i].SelectSingleNode(@"Speed").InnerText);
                    ((Monster)_prototype[i]).Hp = int.Parse(nodelist[i].SelectSingleNode(@"Hp").InnerText);
                    ((Monster)_prototype[i]).Mp = int.Parse(nodelist[i].SelectSingleNode(@"Mp").InnerText);
                    ((Monster)_prototype[i]).CriticalRate = int.Parse(nodelist[i].SelectSingleNode(@"CriticalRate").InnerText);
                    ((Monster)_prototype[i]).Attack = int.Parse(nodelist[i].SelectSingleNode(@"Damage").InnerText);
                    ((Monster)_prototype[i]).Defense = int.Parse(nodelist[i].SelectSingleNode(@"Defense").InnerText);
                    ((Monster)_prototype[i]).AttackSpeed = int.Parse(nodelist[i].SelectSingleNode(@"AttackSpeed").InnerText);
                    ((Monster)_prototype[i]).Range = int.Parse(nodelist[i].SelectSingleNode(@"Range").InnerText);
                    ((Monster)_prototype[i]).X = 0;
                    ((Monster)_prototype[i]).Y = 0;
                    ((Monster)_prototype[i]).HitFrame = int.Parse(nodelist[i].SelectSingleNode(@"HitFrame").InnerText);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
