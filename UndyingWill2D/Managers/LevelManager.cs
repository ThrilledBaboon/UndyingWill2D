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
using System.Diagnostics;

namespace UndyingWill2D.Managers
{
    public class LevelManager
    {
        private List<EntityController> _controllers;
        Dictionary<Vector2, RoomManager> _rooms;
        PlayerController _player;
        ContentManager _contentManager;

        int _screenWidth;
        int _screenHeight;

        Texture2D _playerAnimation;

        Vector2 _currentRoomOrigin;
        RoomManager _currentRoom;

        public LevelManager(ContentManager contentManager, int screenWidth, int screenHeight) 
        { 
            this._contentManager = contentManager;
            this._screenWidth = screenWidth; 
            this._screenHeight = screenHeight;
        }

        public void Initialise()
        {
            LevelGeneration();
            foreach (var room in _rooms)
            {
                RoomManager actualRoom = room.Value;
                actualRoom.Initialise();

            }
            _playerAnimation = _contentManager.Load<Texture2D>("PlayerAnimation");
            _player = new PlayerController(_playerAnimation, 90, new Vector2(_screenWidth / 2, _screenHeight / 2), _contentManager);
            _currentRoomOrigin = new Vector2(0, 0);
            _currentRoom = _rooms[_currentRoomOrigin];
            _currentRoom.AddPlayer(_player, _currentRoomOrigin);
        }
        private void LevelGeneration()
        {
            LevelGenerationManager levelGenerationManager = new LevelGenerationManager(_contentManager, _screenWidth, _screenHeight);
            _rooms = levelGenerationManager.LevelGeneration();
        }
        public void Update()
        {
            List<object> doorCollisionData = _currentRoom.CheckDoorCollision();
            if(doorCollisionData != null ) 
            {
                _currentRoom.RemovePlayer();
                Vector2 enteredRoomDirection = (Vector2)doorCollisionData[1];
                RoomManager enteredRoom = _rooms[_currentRoom.RoomOrigin + enteredRoomDirection];
                enteredRoom.AddPlayer((PlayerController)doorCollisionData[0], enteredRoomDirection);
            }
            _currentRoom.Update();
        }
        public void LoadContent()
        {
            foreach (var room in _rooms)
            {
                RoomManager actualRoom = room.Value;
                actualRoom.LoadContent();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _currentRoom.Draw(spriteBatch);
        }
    }
}
