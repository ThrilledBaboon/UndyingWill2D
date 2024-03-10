using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using UndyingWill2D.Controllers;
using UndyingWill2D.Managers;

namespace UndyingWill2D
{
    public class Main : Game
    {
        //Fields
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private LevelManager _level;
        //Screen Fields
        public int _screenWidth;
        public int _screenHeight;
        //Constructors
        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //set the GraphicsDeviceManager's fullscreen property
            _screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.PreferredBackBufferWidth = _screenWidth; 
            _graphics.PreferredBackBufferHeight = _screenHeight; 
        }
        //Core Methods
        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _level = new LevelManager(Content, _screenWidth, _screenHeight);
            _level.Initialise();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _level.LoadContent();
            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _level.Update();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _level.Draw(_spriteBatch);
            _spriteBatch.End(); 
            base.Draw(gameTime);
        }
    }
}
