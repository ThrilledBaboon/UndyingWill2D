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
        //Fields
        ContentManager _contentManager;
        Vector2 _roomPosition;
        //Constructor
        public LevelGenerationManager(ContentManager contentManager, int screenWidth, int screenHeight) 
        {
            _contentManager = contentManager;
            _roomPosition = new Vector2(screenWidth / 2, screenHeight / 2);
        }
        //Other Methods
        public Dictionary<Vector2, RoomManager> LevelGeneration()
        {
            Dictionary<Vector2, RoomManager> dictionaryOfRooms;
            int maxNumberOfRooms = 15;
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
                if(attemptOnSameRoom > 1)
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
                    List<Vector2> roomDirections = new List<Vector2> { new Vector2(-0, -1), new Vector2(-0, 1), new Vector2(-1, -0), new Vector2(1, -0) };
                    RoomManager childRoom = CreateRoom(parentRoom, directionToGenerateRoom);
                    if(dictionaryOfRooms.ContainsKey(childRoom.RoomOrigin))
                    {
                        parentRoom.AddDoor(directionToGenerateRoom);
                        RoomManager OtherRoom = dictionaryOfRooms[childRoom.RoomOrigin];
                        Vector2 otherDoorToParentConnection = directionToGenerateRoom * new Vector2(-1, -1);
                        OtherRoom.AddDoor(otherDoorToParentConnection);
                        continue;
                    }
                    dictionaryOfRooms.Add(childRoom.RoomOrigin, childRoom);
                    parentRoom.AddDoor(directionToGenerateRoom);
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
                            Vector2 directionFromCollidedRoomToChildRoom = direction * new Vector2(-1, -1);
                            collidedRoom.AddDoor(directionFromCollidedRoomToChildRoom);
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
