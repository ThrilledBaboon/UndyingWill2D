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

        Texture2D PlayerAnimation;

        public LevelManager(ContentManager contentManager, int screenWidth, int screenHeight) 
        { 
            this._contentManager = contentManager;
            this._screenWidth = screenWidth; 
            this._screenHeight = screenHeight;
        }

        public void Initialise()
        {
            LevelGeneration();
            PlayerAnimation = _contentManager.Load<Texture2D>("PlayerAnimation");
            _player = new PlayerController(PlayerAnimation, 90, new Vector2(_screenWidth / 2, _screenHeight / 2), _contentManager);

        }
        private void LevelGeneration()
        {
            LevelGenerationManager levelGenerationManager = new LevelGenerationManager();
            _roomManager = new RoomManager(_contentManager, new Vector2(0, 0), new Vector2(_screenWidth / 2, _screenHeight / 2));
            _rooms.Add(_roomManager);
            _roomManager.Initialise();
            //create start room
            //create shop room
            //gen the rest of the rooms
        }
        public void Update()
        {
            _roomManager.Update();
            _player.Update();
        }
        public void LoadContent()
        {
            _roomManager.LoadContent();
            _player.LoadContent();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int currentRoomIndex = _rooms.Find(_roomManager);
            _rooms[currentRoomIndex].Draw(spriteBatch);
            _player.Draw(spriteBatch);
        }
    }
}
