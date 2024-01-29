﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Net.Security;
using UndyingWill2D.Controllers;
using UndyingWill2D.Managers;

namespace UndyingWill2D
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private LevelManager _level;
        // Public Textures
        public Texture2D Player;
        public Texture2D PlayerAnimation;
        public Texture2D Enemy;

        PlayerController _player;
        AnimationManager _animationManager;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //set the GraphicsDeviceManager's fullscreen property
            int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.PreferredBackBufferWidth = width; 
            _graphics.PreferredBackBufferHeight = height; 
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            LevelManager _level = new LevelManager();
            //_level.Initialise();
            _player = new PlayerController(PlayerAnimation, new Vector2(0, 0), Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _player.LoadContent();
            _animationManager = new(2, 2, new Vector2(32, 32));
            _animationManager.GetFrame();
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            //_spriteBatch.Draw(PlayerAnimation, new Rectangle(0, 0, 75, 75), new Rectangle(0, 0, 32, 32), Color.White); 
            _spriteBatch.Draw(_player.texture, _player.Rectangle, Color.White);
            _spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
