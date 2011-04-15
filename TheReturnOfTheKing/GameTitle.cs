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
    public class GameTitle : Misc
    {
        bool _appeared = false;

        int _delayTime;

        public int DelayTime
        {
            get { return _delayTime; }
            set { _delayTime = value; }
        }

        int _iDelayTime = 0;

        public int IDelayTime
        {
            get { return _iDelayTime; }
            set { _iDelayTime = value; }
        }

        public override void  Init(ContentManager content)
        {
            _nsprite = 2;

            //XMLUtility xml = new XMLUtility(@".\Data\XML\gametitle.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(@".\Data\XML\gametitle.xml");

            /*int _numofframe = int.Parse(xml.GetElementValue(@"NumOfFrames"));
            string contentname = xml.GetElementValue(@"ContentName");

            _sprite = new GameSprite[_nsprite];
            Texture2D[] _textures = new Texture2D[_numofframe];

            for (int i = 0; i < _numofframe; ++i)
            {
                _textures[i] = content.Load<Texture2D>(contentname + i.ToString("00"));
            }*/
            _sprite = new GameSprite[_nsprite];
            XmlNodeList list = doc.DocumentElement.SelectNodes(@"title");

            for (int i = 0; i < list.Count; i++)
            {
                int _numofframe = int.Parse(list[i].SelectSingleNode(@"NumOfFrames").InnerText);
                string contentname = list[i].SelectSingleNode(@"ContentName").InnerText;

                Texture2D[] _textures = new Texture2D[_numofframe];

                for (int j = 0; j < _numofframe; ++j)
                {
                    _textures[j] = content.Load<Texture2D>(contentname + j.ToString("00"));
                }
                _sprite[i] = new GameSprite(_textures, 0, 0);
            }

            X = float.Parse(doc.DocumentElement.SelectSingleNode(@"X").InnerText);
            Y = float.Parse(doc.DocumentElement.SelectSingleNode(@"Y").InnerText);
            _delayTime = int.Parse((doc.DocumentElement.SelectSingleNode(@"DelayTime").InnerText));
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (_iDelayTime == _delayTime)
            {
                if (!_appeared)
                    _sprite[0].Draw(gameTime, sb);
                else
                    _sprite[1].Draw(gameTime, sb);     
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_iDelayTime == _delayTime)
            {
                if (!_appeared)
                {
                    _sprite[0].Itexture2D++;
                    if (_sprite[0].Itexture2D == _sprite[0].Ntexture2D - 1)
                        _appeared = true;
                }
                else
                {
                    _sprite[1].Itexture2D = (_sprite[1].Itexture2D + 1) % _sprite[1].Ntexture2D;
                }
            }
            else
                _iDelayTime++;
        }
    }
}
