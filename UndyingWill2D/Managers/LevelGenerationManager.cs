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
        RoomManager _shopRoom;
        Vector2 _roomPosition;
        int _maxNumberOfRooms;

        Vector2 _up = new Vector2(0, -1);
        Vector2 _down = new Vector2(0, 1);
        Vector2 _left = new Vector2(0, -1);
        Vector2 _right = new Vector2(0, 1);
        public LevelGenerationManager(ContentManager contentManager, int screenWidth, int screenHeight) 
        {
            _roomPosition = new Vector2(screenWidth / 2, screenHeight / 2);
            initialise();
        }
        public void initialise() 
        {
            _maxNumberOfRooms = 25;
            _listOfRooms = new List<RoomManager>();
            RoomManager startRoom = new RoomManager(_contentManager, new Vector2(0, 0), _roomPosition);
            _shopRoom = CreateRoom(startRoom, _right);
            _listOfRooms.Add(startRoom);
            _listOfRooms.Add(_shopRoom);
        }
        public List<RoomManager> LevelGeneration()
        {
            List<RoomManager> listOfGeneratedRooms = new List<RoomManager>();
            Queue<RoomManager> queueOfGeneratedRooms = new Queue<RoomManager>();
            RoomManager firstGeneratedRoom = CreateRoom(_shopRoom, _right);
            listOfGeneratedRooms.Add(firstGeneratedRoom);
            queueOfGeneratedRooms.Enqueue(firstGeneratedRoom);
            while (listOfGeneratedRooms.Count < _maxNumberOfRooms)
            {
                RoomManager parentRoom = queueOfGeneratedRooms.Dequeue();
                List<Vector2> directionsToTryGenerateRoomsIn = parentRoom.ChildDirections;
                for (int i = 0; i <  directionsToTryGenerateRoomsIn.Count; i++) 
                {
                    RoomManager childRoom = CreateRoom(parentRoom, directionsToTryGenerateRoomsIn[i]);
                    listOfGeneratedRooms.Add(childRoom);
                    queueOfGeneratedRooms.Enqueue(childRoom);
                }
            }
            _listOfRooms.AddRange(listOfGeneratedRooms);
            return _listOfRooms; 
        }
        public RoomManager CreateRoom(RoomManager ParentRoom, Vector2 SpawnDirection) 
        {
            Vector2 parentRoomOrigin = ParentRoom.RoomOrigin;
            Vector2 childRoomOrigin = parentRoomOrigin + SpawnDirection;
            RoomManager room = new RoomManager(_contentManager, new Vector2(1, 0), _roomPosition);
            return room;
        }
    }
}
