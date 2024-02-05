using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UndyingWill2D.Controllers;

namespace UndyingWill2D.Managers
{
    internal class LevelManager
    {
        private List<EntityController> _controllers;
        List<TileController> _tiles;

        PlayerController _player;
        TileController _floor;
        SpriteBatch _spriteBatch;
        ContentManager _contentManager;

        public LevelManager(SpriteBatch spriteBatch, ContentManager contentManager) { this._spriteBatch = spriteBatch; this._contentManager = contentManager; }

        public void Initialise()
        {
            FloorTile = Content.Load<Texture2D>("FloorTile");
            _tiles = new List<TileController>();
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    int scale = 50;
                    float currentXPosition = j * scale;
                    float currentYPosition = i * scale;
                    _floor = new TileController(FloorTile, scale, new Vector2(currentXPosition, currentYPosition), Content);
                    _tiles.Add(_floor);
                }
            }
            PlayerAnimation = Content.Load<Texture2D>("PlayerAnimation");
            _player = new PlayerController(PlayerAnimation, 90, new Vector2(_screenWidth / 2, _screenHeight / 2), Content);

        }
        public void LoadContent()
        {
            _player.LoadContent();
        }
        public void Update() 
        {
            _player.Update();
            //for (int item = 0; item < _objects.Count; item++) 
            //{
            //EntityController currentObject = _objects[item];
            //currentObject.Update();
            //}
        }
        public void Draw()
        {
            for (int i = 0; i < _tiles.Count; i++)
            {
                TileController floor = _tiles[i];
                floor.Draw(_spriteBatch);
            }
            _player.Draw(_spriteBatch);
        }
    }
}
