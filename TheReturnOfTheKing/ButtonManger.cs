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

                //Vi button nao cũng xài chung 1 mảng FireSprite chung nên load trước lun
                XmlNode fire = doc.SelectSingleNode(@"//Fire");
                int _numofframe = int.Parse(fire.SelectSingleNode(@"NumOfFrames").InnerText);
                string contentname = fire.SelectSingleNode(@"ContentName").InnerText;
                Texture2D[] _textures = new Texture2D[_numofframe];
                for (int i = 0; i < _numofframe; ++i)
                {
                    _textures[i] = content.Load<Texture2D>(contentname + i.ToString("00"));
                }
                float fireX = float.Parse (fire.SelectSingleNode(@"X").InnerText);

                for (int i = 0; i < _nprototype; ++i)
                {
                    _prototype[i] = new Button();
                    _prototype[i]._nsprite = 3;
                    _prototype[i]._sprite = new GameSprite[3];
               
                    _prototype[i]._sprite[0] = new GameSprite(content.Load<Texture2D>(list[i].SelectSingleNode(@"Idle/ContentName").InnerText),
                        0,
                        0);

                    _prototype[i]._sprite[1] = new GameSprite(content.Load<Texture2D>(list[i].SelectSingleNode(@"MouseHover/ContentName").InnerText),
                        0,
                        0);

                    _prototype[i]._sprite[2] = new GameSprite (_textures, 
                        fireX,
                        0);

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
