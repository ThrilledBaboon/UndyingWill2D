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
            //LevelGeneration()
            _roomManager = new RoomManager(_contentManager, new Vector2(_screenWidth / 2, _screenHeight / 2));
            _roomManager.Initialise();
            PlayerAnimation = _contentManager.Load<Texture2D>("PlayerAnimation");
            _player = new PlayerController(PlayerAnimation, 90, new Vector2(_screenWidth / 2, _screenHeight / 2), _contentManager);

        }
        private void LevelGeneration()
        { 

        }
        private void CreateRoom()
        {

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
            //for (int roomIndex; roomIndex < _rooms.Count(); roomIndex++)
            //{
            //    room = _rooms[roomIndex]
            //    floors = room.Floors;
            //    objectsInRoom = room.ObjectsInRoom;
            //    walls = room.Walls; 
            //    entities = room.Entities;
            //    for (int floorIndex; floorIndex < floors.Count(); floorIndex++)
            //    {
            //        object = floors[floorIndex];
            //        Object.Draw();
            //    }
            //    for (int i; i < objectsInRoom.Count(); i++)
            //    {
            //        object = objectsInRoom[i];
            //        Object.Draw();
            //    }
            //    for (int i; i < walls.Count(); i++)
            //    {
            //        object = _walls[i];
            //        Object.Draw();
            //    }
            //    for (int i; i < entities.Count(); i++)
            //    {
            //        object = entities[i];
            //        Object.Draw();
            //    }
            //}
            _roomManager.Draw(spriteBatch);
            _player.Draw(spriteBatch);
        }
    }
}
