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
    public abstract class GameState
    {
        /// <summary>
        /// Đối tượng đang nắm giữ state (tức là Game chính)
        /// </summary>
        MainGame _owner;

        public MainGame Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        /// <summary>
        /// Khởi tạo state.
        /// Khởi tạo các biến của state ở đây
        /// </summary>
        /// <param name="content">Content</param>
        /// <param name="owner">Cha</param>
        public virtual void InitState(ContentManager content, MainGame owner)
        {
            Owner = owner;
        }
        /// <summary>
        /// Set lại các biến của state nếu cần thiết
        /// </summary>
        public virtual void EnterState()
        {

        }
        /// <summary>
        /// Hàm update cho state
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void UpdateState(GameTime gameTime)
        {

        }
        /// <summary>
        /// Chổ này sẽ giải phóng thuộc tính nếu cần thiết
        /// </summary>
        public virtual void ExitState()
        { 

        }
        /// <summary>
        /// Vẽ
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sb"></param>
        public virtual void DrawState(GameTime gameTime, SpriteBatch sb)
        { 

        }
    }
}
