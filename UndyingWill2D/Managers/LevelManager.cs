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
using UndyingWill2D;

namespace UndyingWill2D.Managers
{
    public class LevelManager
    {
        private List<EntityController> _controllers;
        //List<TileController> _tiles;

        PlayerController _player;
        ContentManager _contentManager;
        RoomManager _roomManager;

        int _screenWidth;
        int _screenHeight;

        //Texture2D FloorTile;
        Texture2D PlayerAnimation;

        public LevelManager(ContentManager contentManager, int screenWidth, int screenHeight) 
        { 
            this._contentManager = contentManager;
            this._screenWidth = screenWidth; 
            this._screenHeight = screenHeight;
        }

        public void Initialise()
        {
            _roomManager = new RoomManager(_contentManager);
            _roomManager.Initialise();
            PlayerAnimation = _contentManager.Load<Texture2D>("PlayerAnimation");
            _player = new PlayerController(PlayerAnimation, 90, new Vector2(_screenWidth / 2, _screenHeight / 2), _contentManager);

        }
        public void LoadContent()
        {
            _player.LoadContent();
        }
        public void Update() 
        {
            _roomManager.Update();
            _player.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _roomManager.Draw(spriteBatch);
            _player.Draw(spriteBatch);
        }
    }
}
