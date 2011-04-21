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
    public class MenuBackground : Misc
    {
        public override void Init(ContentManager content)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Data\XML\menubg.xml");
            //_nsprite = 1;  
            _nsprite = int.Parse(doc.SelectSingleNode(@"//NumOfFrames").InnerText);
            string contentname = doc.SelectSingleNode(@"//ContentName").InnerText;
            _sprite = new GameSprite[_nsprite];
            _sprite[0] = new GameSprite(content.Load<Texture2D>(contentname), int.Parse(doc.SelectSingleNode(@"//X").InnerText), int.Parse(doc.SelectSingleNode(@"//Y").InnerText));
            
        }

       

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
