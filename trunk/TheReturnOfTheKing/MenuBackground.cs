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
    public class MenuBackground : Misc
    {
        public override void Init(ContentManager content)
        {
            XMLUtility xml = new XMLUtility(@".\Data\XML\menubg.xml");
            _nsprite = 1;            
            string contentname = xml.GetElementValue(@"ContentName");
            _sprite = new GameSprite[_nsprite];
            _sprite[0] = new GameSprite(content.Load<Texture2D>(contentname), int.Parse(xml.GetElementValue(@"X")), int.Parse(xml.GetElementValue(@"Y")));
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }

        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
            
        }

    }
}
