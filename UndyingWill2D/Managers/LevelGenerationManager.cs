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
        ContentManager _contentManager;
        Vector2 _roomPosition;
        Vector2 _up = new Vector2(0, -1);
        Vector2 _down = new Vector2(0, 1);
        Vector2 _left = new Vector2(-1, 0);
        Vector2 _right = new Vector2(1, 0);
        public LevelGenerationManager(ContentManager contentManager, int screenWidth, int screenHeight) 
        {
            _contentManager = contentManager;
            _roomPosition = new Vector2(screenWidth / 2, screenHeight / 2);
        }
        public Dictionary<Vector2, RoomManager> LevelGeneration()
        {
            Dictionary<Vector2, RoomManager> dictionaryOfRooms;
            int maxNumberOfRooms = 25;
            RoomManager previousRoom = null;
            RoomManager parentRoom = null;
            dictionaryOfRooms = new Dictionary<Vector2, RoomManager>();
            RoomManager startRoom = new RoomManager(_contentManager, new Vector2(0, 0), _roomPosition);
            dictionaryOfRooms.Add(startRoom.RoomOrigin, startRoom);
            Queue<RoomManager> queueOfGeneratedRooms = new Queue<RoomManager>();
            queueOfGeneratedRooms.Enqueue(startRoom);
            int attemptOnSameRoom = 0;
            while (dictionaryOfRooms.Count < maxNumberOfRooms)
            {
                if(attemptOnSameRoom > 5)
                {
                    return LevelGeneration();
                }
                if (queueOfGeneratedRooms.Count != 0)
                {
                    parentRoom = queueOfGeneratedRooms.Dequeue();
                    previousRoom = parentRoom;
                    attemptOnSameRoom = 0;
                }
                else
                {
                    attemptOnSameRoom++;
                    parentRoom = previousRoom;
                }
                List<Vector2> directionsToTryGenerateRoomsIn = parentRoom.ChildDirections;
                foreach (Vector2 directionToGenerateRoom in directionsToTryGenerateRoomsIn) 
                {
                    List<Vector2> roomDirections = new List<Vector2> { new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0) };
                    RoomManager childRoom = CreateRoom(parentRoom, directionToGenerateRoom);
                    try
                    {
                        dictionaryOfRooms.Add(childRoom.RoomOrigin, childRoom);
                    }
                    catch (ArgumentException)
                    {
                        parentRoom.RemoveDoorPossibility(directionToGenerateRoom);
                        continue;
                    }
                    Vector2 childDoorToParentConnection = directionToGenerateRoom * new Vector2(-1, -1);
                    childRoom.AddDoor(childDoorToParentConnection);
                    roomDirections.Remove(childDoorToParentConnection);
                    foreach (Vector2 direction in roomDirections)
                    {
                        Vector2 checkForCollision = childRoom.RoomOrigin + direction;
                        if (dictionaryOfRooms.ContainsKey(checkForCollision))
                        {
                            childRoom.AddDoor(direction);
                            RoomManager collidedRoom = dictionaryOfRooms[checkForCollision];
                            Vector2 directionOfCollidedRoomToChildRoom = directionToGenerateRoom * new Vector2(-1, -1);
                            collidedRoom.AddDoor(directionOfCollidedRoomToChildRoom);
                        }
                    }
                    queueOfGeneratedRooms.Enqueue(childRoom);
                }
            }
            return dictionaryOfRooms; 
        }
        public RoomManager CreateRoom(RoomManager ParentRoom, Vector2 SpawnDirection) 
        {
            Vector2 childRoomOrigin = ParentRoom.RoomOrigin + SpawnDirection;
            return new RoomManager(_contentManager, childRoomOrigin, _roomPosition);
        }
    }
}
