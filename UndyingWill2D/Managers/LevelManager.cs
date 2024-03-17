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
        //Screen Fields
        int _screenWidth;
        int _screenHeight;
        //Player Fields
        PlayerController _player;
        Texture2D _playerAnimation;
        //Room Fields
        RoomManager _currentRoom;
        Dictionary<Vector2, RoomManager> _rooms;
        //Other Fields
        ContentManager _contentManager;
        //Constructor
        public LevelManager(ContentManager contentManager, int screenWidth, int screenHeight) 
        { 
            this._contentManager = contentManager;
            this._screenWidth = screenWidth; 
            this._screenHeight = screenHeight;
        }
        //Core Methods
        public void Initialise()
        {
            LevelGeneration();
            Debug.WriteLine(_rooms.Count);
            Vector2 currentRoomOrigin = new Vector2(0, 0);
            _currentRoom = _rooms[currentRoomOrigin];
            _currentRoom.Initialise();
            _playerAnimation = _contentManager.Load<Texture2D>("PlayerAnimation");
            _player = new PlayerController(_playerAnimation, 90, new Vector2(0, 0), _contentManager);
            _currentRoom.AddPlayer(_player, new Vector2(0, 0));
            _currentRoom.LoadContent();
        }
        public void Update()
        {
            _currentRoom.Update();
            CheckForDoorCollision();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _currentRoom.Draw(spriteBatch);
        }
        //Other Methods
        private void LevelGeneration()
        {
            LevelGenerationManager levelGenerationManager = new LevelGenerationManager(_contentManager, _screenWidth, _screenHeight);
            _rooms = levelGenerationManager.LevelGeneration();
        }
        private void CheckForDoorCollision()
        {
            List<object> doorCollisionData = _currentRoom.CheckDoorCollision();
            if (doorCollisionData != null)
            {
                _currentRoom.RemovePlayer();
                Vector2 enteredRoomDirection = (Vector2)doorCollisionData[1];
                RoomManager enteredRoom = _rooms[_currentRoom.RoomOrigin + enteredRoomDirection];
                _currentRoom = enteredRoom;
                _currentRoom.Initialise();
                enteredRoom.AddPlayer((PlayerController)doorCollisionData[0], enteredRoomDirection);
                _currentRoom.LoadContent();
            }
        }
    }
}
