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
        List<RoomManager> _rooms;
        PlayerController _player;
        ContentManager _contentManager;
        RoomManager _roomManager;

        int _screenWidth;
        int _screenHeight;

        Texture2D _playerAnimation;

        int _currentRoomIndex;

        public LevelManager(ContentManager contentManager, int screenWidth, int screenHeight) 
        { 
            this._contentManager = contentManager;
            this._screenWidth = screenWidth; 
            this._screenHeight = screenHeight;
        }

        public void Initialise()
        {
            LevelGeneration();
            _playerAnimation = _contentManager.Load<Texture2D>("PlayerAnimation");
            _player = new PlayerController(_playerAnimation, 90, new Vector2(_screenWidth / 2, _screenHeight / 2), _contentManager);

        }
        private void LevelGeneration()
        {
            LevelGenerationManager levelGenerationManager = new LevelGenerationManager(_contentManager, _screenWidth, _screenHeight);
            _rooms = levelGenerationManager.LevelGeneration();
        }
        public void Update()
        {
            _currentRoomIndex = 0;
            //_roomManager.Update();
            _player.Update();
        }
        public void LoadContent()
        {
            //_roomManager.LoadContent();
            _player.LoadContent();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _rooms[_currentRoomIndex].Draw(spriteBatch);
            _player.Draw(spriteBatch);
        }
    }
}
