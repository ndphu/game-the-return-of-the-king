using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
               
        //Cursor _cursor = new Cursor();

        GameState _gameState;

        public GameState GameState
        {
            get { return _gameState; }
            set { _gameState = value; }
        }
        
        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";                     
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            //IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.graphics.IsFullScreen = true;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            GlobalVariables.ScreenHeight = this.Window.ClientBounds.Height;
            GlobalVariables.ScreenWidth = this.Window.ClientBounds.Width;
            GlobalVariables.MouseObserver = new MouseObserver();
            //GlobalVariables.KeyboardObserver = new KeyboardObserver();
            GlobalVariables.Btm = new ButtonManger();
            GlobalVariables.Btm.InitPrototypes(Content, @"./Data/XML/buttonmanager.xml");
            GlobalVariables.Mnm = new MenuManager();
            GlobalVariables.Mnm.InitPrototypes(Content, @"./Data/XML/menumanager.xml");

            //_cursor.Init(Content);
            GlobalVariables.GameCursor = new Cursor();
            GlobalVariables.GameCursor.Init(Content);

            _gameState = new StateMainGame();
            _gameState.InitState(Content, this);
            _gameState.EnterState();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            GlobalVariables.MouseObserver.Update(gameTime);
            //GlobalVariables.KeyboardObserver.Update(gameTime);
            _gameState.UpdateState(gameTime);            
            GlobalVariables.GameCursor.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            _gameState.DrawState(gameTime, spriteBatch);
            //_cursor.Draw(gameTime, spriteBatch);
            GlobalVariables.GameCursor.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
