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
    
    public class StateMenu : GameState
    {
        MenuBackground _menubg = new MenuBackground();
        List<Button> _buttonArray = new List<Button>();
        GameTitle _gametitle = new GameTitle();
        Menu _menu = new Menu();

        public override void InitState(ContentManager content, MainGame owner)
        {
            base.InitState(content, owner);
            _menubg.Init(content);
            _gametitle.Init(content);
            _menu = (Menu)GlobalVariables.Mnm.CreateObject(0);
            _menu.StateOwner = this;
            _menu.Child[0].Mouse_Click += new Button.OnMouseClickHandler(StateMenu_Mouse_Click_NewGame);
            _menu.Child[1].Mouse_Click += new Button.OnMouseClickHandler(StateMenu_Mouse_Click_Load);
            _menu.Child[2].Mouse_Click += new Button.OnMouseClickHandler(StateMenu_Mouse_Click_About);
            _menu.Child[3].Mouse_Click += new Button.OnMouseClickHandler(StateMenu_Mouse_Click_Help);
            _menu.Child[4].Mouse_Click += new Button.OnMouseClickHandler(StateMenu_Mouse_Click_Option);
            _menu.Child[5].Mouse_Click += new Button.OnMouseClickHandler(StateMenu_Mouse_Click_Quit);

            //for (int i = 0; i < _menu.Child.Count; ++i)
            //    GlobalVariables.MouseObserver.RegisterObserver(_menu.Child[i]);
        }

        /// <summary>
        /// Hàm xử lý cho sự kiện click lên nút NewGame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StateMenu_Mouse_Click_NewGame(object sender, EventArgs e)
        {
            Button _sender = (Button)sender;
            //BUtton dừng lại rồi, mới được click
            if (!_sender._motionInfo.IsStanding)
                return;
            Owner.GameState.ExitState();
            Owner.GameState = new StateMainGame();
            Owner.GameState.InitState(Owner.Content, Owner);
            Owner.GameState.EnterState();
        }

        /// <summary>
        /// Hàm xử lý cho sự kiện click lên nút Quit
        /// Dự kiến xử lý: Tắt game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StateMenu_Mouse_Click_Quit(object sender, EventArgs e)
        {
            Owner.Exit();
        }
        /// <summary>
        /// Hàm xử lý cho sự kiện click lên nút option
        /// Dự kiến xử lý: chuyển sang StateOption
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StateMenu_Mouse_Click_Option(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Hàm xử lý cho sự kiện click lên nút Help
        /// Dự kiến xử lý: Chuyển sang trạng thái HelpState
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StateMenu_Mouse_Click_Help(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Hàm xử lý cho sự kiện click lên nút About
        /// Dự kiến xử lý: chuyển sang trạng thái AboutState: giới thiệu nhóm, email, support này nọ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StateMenu_Mouse_Click_About(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        ///  Hàm xử lý cho sự kiện lick lên nút Load
        ///  Dự kiến xử lý: chuyển sang trạng thái LoadState (khác loadingstate nhá)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StateMenu_Mouse_Click_Load(object sender, EventArgs e)
        {
            
        }
        
        public override void EnterState()
        {
            for (int i = 0; i < _menu.Child.Count; ++i)
                GlobalVariables.MouseObserver.RegisterObserver(_menu.Child[i]);
            //GlobalVariables.MouseObserver.RegisterObserver(_menu);
            //GlobalVariables.KeyboardObserver.RegisterObserver(_menu);
        }

        public override void DrawState(GameTime gameTime, SpriteBatch sb)
        {
            _menubg.Draw(gameTime, sb);
            _gametitle.Draw(gameTime, sb);
            _menu.Draw(gameTime, sb);
        }

        public override void UpdateState(GameTime gameTime)
        {
            _menubg.Update(gameTime);
            _gametitle.Update(gameTime);            
            _menu.Update(gameTime);
        }

        public override void ExitState()
        {
            for (int i = 0; i < _menu.Child.Count; ++i)
                GlobalVariables.MouseObserver.UnregisterObserver(_menu.Child[i]);
            //GlobalVariables.KeyboardObserver.UnregisterObserver(_menu);
        }
    }
}
