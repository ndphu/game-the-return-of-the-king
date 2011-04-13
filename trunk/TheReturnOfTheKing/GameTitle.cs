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
    public class GameTitle : Misc
    {
        int _delayTime;

        public int DelayTime
        {
            get { return _delayTime; }
            set { _delayTime = value; }
        }

        public override void  Init(ContentManager content)
        {
            _nsprite = 2;
            XMLUtility xml = new XMLUtility(@".\Data\XML\gametitle.xml");
            int _numofframe = int.Parse(xml.GetElementValue(@"NumOfFrames"));
            string contentname = xml.GetElementValue(@"ContentName");
            _sprite = new GameSprite[_nsprite];
            Texture2D[] _textures = new Texture2D[_numofframe];
            for (int i = 0; i < _numofframe; ++i)
            {
                _textures[i] = content.Load<Texture2D>(contentname + i.ToString("00"));
            }
            _sprite[0] = new GameSprite(_textures, int.Parse(xml.GetElementValue(@"X")),int.Parse(xml.GetElementValue(@"Y")));
            DelayTime = 100;

            xml = new XMLUtility(@".\Data\XML\gametitleeffect.xml");
            _nsprite = 1;
            _numofframe = int.Parse(xml.GetElementValue(@"NumOfFrames"));
            contentname = xml.GetElementValue(@"ContentName");            
            _textures = new Texture2D[_numofframe];
            for (int i = 0; i < _numofframe; ++i)
            {
                _textures[i] = content.Load<Texture2D>(contentname + i.ToString("00"));
            }
            _sprite[1] = new GameSprite(_textures, int.Parse(xml.GetElementValue(@"X")), int.Parse(xml.GetElementValue(@"Y")));
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            _sprite[1].Draw(gameTime, sb);
            if (_delayTime <= 0)
                _sprite[0].Draw(gameTime, sb);
        }

        public override void Update(GameTime gameTime)
        {
            _sprite[1].Itexture2D = (_sprite[1].Itexture2D + 1) % _sprite[1].Ntexture2D;
            if (_delayTime > 0)
            {
                _delayTime -= 1;
                return;
            }            
            if (_sprite[0].Itexture2D < _sprite[0].Ntexture2D - 1)
                _sprite[0].Itexture2D += 1; 
        }
    }
}
