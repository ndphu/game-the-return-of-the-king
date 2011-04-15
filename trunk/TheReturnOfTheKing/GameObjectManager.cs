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
    public abstract class GameObjectManager
    {
        protected VisibleGameObject[] _prototype;
        protected int _nprototype;

        public virtual bool InitPrototypes(ContentManager content, string fileName)
        {
            _nprototype = 0;
            return true;
        }

        public virtual VisibleGameObject CreateObject(int idx)
        {
            if (0 <= idx && idx < _nprototype)
                return _prototype[idx].Clone();
            return null;
        }
    }
}
