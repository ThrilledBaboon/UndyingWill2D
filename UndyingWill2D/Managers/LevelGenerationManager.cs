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
                if(queueOfGeneratedRooms.Count != 0)
                {
                    parentRoom = queueOfGeneratedRooms.Dequeue();
                    previousRoom = parentRoom;
                }
                else
                {
                    parentRoom = previousRoom;
                }
                Debug.WriteLine("Current parent room origin " + parentRoom.RoomOrigin);
                List<Vector2> directionsToTryGenerateRoomsIn = parentRoom.ChildDirections;
                Debug.WriteLine("Below is the list of directions for parent room: " + parentRoom.RoomOrigin);
                foreach (Vector2 direction in directionsToTryGenerateRoomsIn)
                {
                    Debug.WriteLine(direction);
                }
                Debug.WriteLine(" ");
                foreach (Vector2 directionToGenerateRoom in directionsToTryGenerateRoomsIn) 
                {
                    List<Vector2> roomDirections = new List<Vector2> { new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0) };
                    Debug.WriteLine("direction to try make a room in: " + directionToGenerateRoom);
                    RoomManager childRoom = CreateRoom(parentRoom, directionToGenerateRoom);
                    try
                    {
                        _dictionaryOfRooms.Add(childRoom.RoomOrigin, childRoom);
                        Debug.WriteLine("A room was created in this direction");
                        Debug.WriteLine("created child room origin: " + childRoom.RoomOrigin);
                    }
                    catch (ArgumentException)
                    {
                        Debug.WriteLine("A room was already in this direction, so we remove the door opportunity in the parent room");
                        parentRoom.RemoveDoorPossibility(directionToGenerateRoom);
                        continue;
                    }
                    Vector2 childDoorToParentConnection = directionToGenerateRoom * new Vector2(-1, -1);
                    childRoom.AddDoor(childDoorToParentConnection);
                    roomDirections.Remove(childDoorToParentConnection);
                    foreach (Vector2 direction in roomDirections)
                    {
                        Vector2 checkForCollision = childRoom.RoomOrigin + direction;
                        if (_dictionaryOfRooms.ContainsKey(checkForCollision))
                        {
                            childRoom.AddDoor(direction);
                            RoomManager collidedRoom = _dictionaryOfRooms[checkForCollision];
                            Vector2 directionOfCollidedRoomToChildRoom = directionToGenerateRoom * new Vector2(-1, -1);
                            collidedRoom.AddDoor(directionOfCollidedRoomToChildRoom);
                        }
                    }
                    queueOfGeneratedRooms.Enqueue(childRoom);
                    Debug.WriteLine(" ");
                }
                Debug.WriteLine(" ");
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
