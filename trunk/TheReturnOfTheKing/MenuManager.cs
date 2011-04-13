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
    public class MenuManager : GameObjectManager
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
                XmlNodeList list = doc.SelectNodes(@"//Menu");
                _nprototype = list.Count;
                _prototype = new Menu[_nprototype];
                for (int i = 0; i < _nprototype; ++i)
                {
                    _prototype[i] = new Menu();
                    _prototype[i]._nsprite = 1;
                    _prototype[i]._sprite = new GameSprite[1];
                    
                    _prototype[i]._sprite[0] = new GameSprite(content.Load<Texture2D>(list[i].SelectSingleNode(@"Background").InnerText),
                        (int)_prototype[i].X,
                        (int)_prototype[i].Y);
                    _prototype[i].Height = int.Parse(list[i].SelectSingleNode(@"Height").InnerText);
                    _prototype[i].Width = int.Parse(list[i].SelectSingleNode(@"Width").InnerText);
                    _prototype[i].X = int.Parse(list[i].SelectSingleNode(@"X").InnerText);
                    _prototype[i].Y = int.Parse(list[i].SelectSingleNode(@"Y").InnerText);
                    _prototype[i].Rect = new Rectangle((int)_prototype[i].X, (int)_prototype[i].Y, (int)_prototype[i].Width, (int)_prototype[i].Height);
                    
                    for (int j = 0; j < list[i].ChildNodes.Count; ++j)
                    {
                        if (list[i].ChildNodes[j].Name.CompareTo("Button") == 0)
                        {
                            XmlNode _node = list[i].ChildNodes[j];
                            Button _b = (Button)GlobalVariables.Btm.CreateObject(int.Parse(_node.SelectSingleNode("Type").InnerText));
                            //_b.X = _prototype[i].X + int.Parse(_node.SelectSingleNode("X").InnerText);
                            _b.X = _prototype[i].X + (_prototype[i].Width - _b.Width) / 2;
                            _b.Y = _prototype[i].Y + int.Parse(_node.SelectSingleNode("Y").InnerText);
                            _b.Rect = new Rectangle((int)_b.X, (int)_b.Y, (int)_b.Width, (int)_b.Height);
                            ((Menu)_prototype[i]).Child.Add(_b);
                        }
                    }
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
