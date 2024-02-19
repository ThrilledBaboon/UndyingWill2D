using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndyingWill2D.Managers
{
    public class LevelGenerationManager
    {
        List<RoomManager> _listOfRooms;
        ContentManager _contentManager;
        Vector2 _roomPosition;
        int _maxNumberOfRooms;

        public LevelGenerationManager(ContentManager contentManager, int screenWidth, int screenHeight) 
        {
            _roomPosition = new Vector2(screenWidth / 2, screenHeight / 2);
            initialise();
        }
        public void initialise() 
        {
            _maxNumberOfRooms = 25;
            RoomManager startRoom = new RoomManager(_contentManager, new Vector2(0, 0), _roomPosition);
            RoomManager shopRoom = new RoomManager(_contentManager, new Vector2(1, 0), _roomPosition);
            _listOfRooms.Add(startRoom);
            _listOfRooms.Add(shopRoom);
        }
        public List<RoomManager> LevelGeneration()
        {
            List<RoomManager> listOfGeneratedRooms = new List<RoomManager>();
            Queue<RoomManager> queueOfGeneratedRooms = new Queue<RoomManager>();
            while (listOfGeneratedRooms.Count < _maxNumberOfRooms)
            {
                
            }
            return _listOfRooms; 
        }
        public void CreateRoom(RoomManager ParentRoom, Vector2 SpawnDirection) 
        {
            Vector2 parentRoomOrigin = ParentRoom.RoomOrigin;
            Vector2 childRoomOrigin = parentRoomOrigin + SpawnDirection;
        }
    }
}
