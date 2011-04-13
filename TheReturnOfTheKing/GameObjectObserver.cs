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
    public abstract class ObjectObserver
    {
        private List<VisibleGameObject> _observers = new List<VisibleGameObject>();

        public List<VisibleGameObject> Observers
        {
            get { return _observers; }
            set { _observers = value; }
        }

        public void RegisterObserver(VisibleGameObject observer)
        {
            _observers.Add(observer);
        }

        public void UnregisterObserver(VisibleGameObject observer)
        {
            _observers.Remove(observer);
        }

        public virtual void Update(GameTime gt)
        {
            
        }
    }
}
