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
    public class ButtonManger : GameObjectManager
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
                XmlNodeList list = doc.SelectNodes(@"//Button");
                _nprototype = list.Count;
                _prototype = new Button[_nprototype];
                for (int i = 0; i < _nprototype; ++i)
                {
                    _prototype[i] = new Button();
                    _prototype[i]._nsprite = 3;
                    _prototype[i]._sprite = new GameSprite[3];

                    _prototype[i]._sprite[0] = new GameSprite(content.Load<Texture2D>(list[i].SelectSingleNode(@"Normal").InnerText),
                        (int)_prototype[i].X,
                        (int)_prototype[i].Y);                    
                    
                    int _numofframe = int.Parse(list[i].SelectSingleNode(@"MouseHover").SelectSingleNode(@"NumOfFrames").InnerText);
                    string contentname = list[i].SelectSingleNode(@"MouseHover").SelectSingleNode(@"ContentName").InnerText;
                    Texture2D[] _textures = new Texture2D[_numofframe];
                    for (int j = 0; j < _numofframe; ++j)
                    {
                        _textures[j] = content.Load<Texture2D>(contentname + j.ToString("00"));
                    }

                    _prototype[i]._sprite[1] = new GameSprite(_textures,
                        0,
                        0);

                    _numofframe = int.Parse(list[i].SelectSingleNode(@"MouseClicked").SelectSingleNode(@"NumOfFrames").InnerText);
                    contentname = list[i].SelectSingleNode(@"MouseClicked").SelectSingleNode(@"ContentName").InnerText;
                    _textures = new Texture2D[_numofframe];
                    for (int j = 0; j < _numofframe; ++j)
                    {
                        _textures[j] = content.Load<Texture2D>(contentname + j.ToString("00"));
                    }

                    _prototype[i]._sprite[2] = new GameSprite(_textures, 0,0);
                    ((Button)_prototype[i]).ClickedAnimateOffsetX = int.Parse(list[i].SelectSingleNode(@"XOFFSET").InnerText);
                    ((Button)_prototype[i]).ClickedAnimateOffsetY = int.Parse(list[i].SelectSingleNode(@"YOFFSET").InnerText);
                    
                    _prototype[i].Height = int.Parse(list[i].SelectSingleNode(@"Height").InnerText);
                    _prototype[i].Width = int.Parse(list[i].SelectSingleNode(@"Width").InnerText);                    
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
