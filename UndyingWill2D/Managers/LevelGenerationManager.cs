using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndyingWill2D.Managers
{
    public class LevelGenerationManager
    {
        Dictionary<Vector2, RoomManager> _dictionaryOfRooms;
        ContentManager _contentManager;
        RoomManager _startRoom;
        Vector2 _roomPosition;
        int _maxNumberOfRooms;
        Vector2 _up = new Vector2(0, -1);
        Vector2 _down = new Vector2(0, 1);
        Vector2 _left = new Vector2(-1, 0);
        Vector2 _right = new Vector2(1, 0);
        public LevelGenerationManager(ContentManager contentManager, int screenWidth, int screenHeight) 
        {
            _contentManager = contentManager;
            _roomPosition = new Vector2(screenWidth / 2, screenHeight / 2);
            initialise();
        }
        public void initialise() 
        {
            _maxNumberOfRooms = 25;
            _dictionaryOfRooms = new Dictionary<Vector2, RoomManager>();
            _startRoom = new RoomManager(_contentManager, new Vector2(0, 0), _roomPosition);

            _dictionaryOfRooms.Add(_startRoom.RoomOrigin, _startRoom);
        }
        public Dictionary<Vector2, RoomManager> LevelGeneration()
        {
            RoomManager previousRoom = null;
            RoomManager parentRoom;
            Queue<RoomManager> queueOfGeneratedRooms = new Queue<RoomManager>();
            queueOfGeneratedRooms.Enqueue(_startRoom);
            while (_dictionaryOfRooms.Count < _maxNumberOfRooms)
            {
                try
                {
                    parentRoom = queueOfGeneratedRooms.Dequeue();
                    previousRoom = parentRoom;
                }
                catch (ArgumentException) 
                {
                    parentRoom = previousRoom;
                }
                List<Vector2> directionsToTryGenerateRoomsIn = parentRoom.ChildDirections;
                foreach (Vector2 direction in directionsToTryGenerateRoomsIn) 
                {
                    RoomManager childRoom = CreateRoom(parentRoom, direction);
                    try
                    {
                        _dictionaryOfRooms.Add(childRoom.RoomOrigin, childRoom);
                    }
                    catch (ArgumentException)
                    {
                        continue;
                    }
                    childRoom.RemoveDoorPossibility(direction);
                    queueOfGeneratedRooms.Enqueue(childRoom);
                }
            }
            return _dictionaryOfRooms; 
        }
        public RoomManager CreateRoom(RoomManager ParentRoom, Vector2 SpawnDirection) 
        {
            Vector2 childRoomOrigin = ParentRoom.RoomOrigin + SpawnDirection;
            return new RoomManager(_contentManager, childRoomOrigin, _roomPosition);
        }
    }
}
