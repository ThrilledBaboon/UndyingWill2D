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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private LevelManager _level;
        int _screenWidth;
        int _screenHeight;
        // Public Textures
        public Texture2D Player;
        public Texture2D PlayerAnimation;
        public Texture2D Enemy;
        public Texture2D EnemyAnimation;
        public Texture2D FloorTile;

        //PlayerController _player;
        //MobController _enemy;
        TileController _floor;

        List<TileController> _tiles;

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

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            LevelManager _level = new LevelManager(_spriteBatch, Content);
            _level.Initialise();
            //PlayerAnimation = Content.Load<Texture2D>("PlayerAnimation");
            //EnemyAnimation = Content.Load<Texture2D>("SkeletonAnimation");
            //FloorTile = Content.Load<Texture2D>("FloorTile");
            //_tiles = new List<TileController>();
            //for (int i = 0; i < 30; i++) 
            //{
            //    for (int j = 0; j < 40; j++)
            //    {
            //        int scale = 50;
            //        float currentXPosition = j * scale;
            //        float currentYPosition = i * scale;
            //        _floor = new TileController(FloorTile, scale, new Vector2(currentXPosition, currentYPosition), Content);
            //        _tiles.Add(_floor);
            //    }
            //}
            //_enemy = new MobController(EnemyAnimation, 90, new Vector2(_screenWidth / 4, _screenHeight / 4), Content);
            //_player = new PlayerController(PlayerAnimation,90, new Vector2(_screenWidth / 2, _screenHeight / 2), Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            PlayerAnimation = Content.Load<Texture2D>("PlayerAnimation");
            EnemyAnimation = Content.Load<Texture2D>("SkeletonAnimation");
            _level.LoadContent();
            //_player.LoadContent();
            //_enemy.LoadContent();
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _level.Update();
            //_player.Update();
            //_enemy.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            //for (int i = 0; i < _tiles.Count; i++)
            //{
            //    TileController floor = _tiles[i];
            //    floor.Draw(_spriteBatch);
            //}
            _level.Draw();
            //_player.Draw(_spriteBatch);
            //_enemy.Draw(_spriteBatch);
            _spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
