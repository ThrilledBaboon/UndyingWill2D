﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UndyingWill2D.Controllers;
using static System.Formats.Asn1.AsnWriter;

namespace UndyingWill2D.Managers
{
    public class RoomManager
    {
        //Room Information Fields
        int _roomLength = 14;
        int _roomHeight = 10;
        //Collection Fields
        List<TileController> _floors = new List<TileController>();
        List<TileController> _walls = new List<TileController>();
        List<DoorController> _doors = new List<DoorController> { };
        List<EntityController> _entities = new List<EntityController>();
        List<Vector2> _childDirections = new List<Vector2>();
        //List<Vector2> _wallsWhereDoorsCouldntBe = new List<Vector2> { new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0) };
        List<Vector2> _whereDoorsCouldBe = new List<Vector2> { new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0) };
        List<Vector2> _whereDoorsAre = new List<Vector2> {};
        //Texture Fields
        Texture2D _floorTile;
        Texture2D _topWallTile;
        Texture2D _rightSideWallTile;
        Texture2D _leftSideWallTile;
        Texture2D _bottomWallTile;
        Texture2D _topDoorTile;
        Texture2D _rightSideDoorTile;
        Texture2D _leftSideDoorTile;
        Texture2D _bottomDoorTile;
        //Other Fields
        ContentManager _contentManager;
        Vector2 _screenPosition;
        Random _random = new Random();
        int? _numberOfMaxChildren;
        PlayerController _player;
        //Properties
        public Vector2 RoomOrigin {  get; set; }
        public List<Vector2> ChildDirections
        {
            get
            {
                if(_childDirections.Count == 0)
                {
                    List<Vector2> childDirections = CreateChildDirections();
                    return childDirections;
                }
                return _childDirections;
            }
        }
        public int? NumberOfMaxChildren
        {
            get
            {
                if (_numberOfMaxChildren == null) 
                {
                    int numberOfChildren = CreateNumberOfMaxChildren();
                    _numberOfMaxChildren = numberOfChildren;
                    return _numberOfMaxChildren;
                }
                return _numberOfMaxChildren;
            } 
        }
        //Constructor
        public RoomManager(ContentManager contentManager, Vector2 origin, Vector2 screenPosition) 
        { 
            this._contentManager = contentManager;
            this.RoomOrigin = origin;
            this._screenPosition = screenPosition;
        }
        //Core Methods
        public void Initialise() 
        {
            _floorTile = _contentManager.Load<Texture2D>("FloorTile");
            _topWallTile = _contentManager.Load<Texture2D>("TopWall");
            _leftSideWallTile = _contentManager.Load<Texture2D>("LeftSideWall");
            _rightSideWallTile = _contentManager.Load<Texture2D>("RightSideWall");
            _bottomWallTile = _contentManager.Load<Texture2D>("BottomWall");
            _topDoorTile = _contentManager.Load<Texture2D>("TopDoor");
            _leftSideDoorTile = _contentManager.Load<Texture2D>("LeftSideDoor");
            _rightSideDoorTile = _contentManager.Load<Texture2D>("RightSideDoor");
            _bottomDoorTile = _contentManager.Load<Texture2D>("BottomDoor");
            _floors = new List<TileController>();
            CreateFloor();
            CreateWalls();
            CreateEntities();
        }
        public void LoadContent()
        {
            foreach (EntityController entity in _entities)
            {
                entity.LoadContent();
            }
            if (_player != null)
            {
                _player.LoadContent();
            }
        }
        public void Update()
        {
            foreach (EntityController entity in _entities)
            {
                entity.Update(_roomHeight, _roomLength);
            }
            if (_player != null)
            {
                _player.Update(_roomHeight, _roomLength);
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.K))
                {
                    foreach (DoorController door in _doors) 
                    { door.CloseDoor(); }
                }
                //if (keyboardState.IsKeyDown(Keys.J))
                if (true)
                {
                    foreach (DoorController door in _doors)
                    { door.OpenDoor(); }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (TileController floor in _floors)
            {
                floor.ActualPosition = RoomGridToWorldCoordinatesMap(floor);
                floor.Draw(spriteBatch);
            }
            foreach (TileController wall in _walls)
            {
                wall.ActualPosition = RoomGridToWorldCoordinatesMap(wall);
                wall.Draw(spriteBatch);
            }
            foreach (DoorController door in _doors)
            {
                door.ActualPosition = RoomGridToWorldCoordinatesMap(door);
                door.Draw(spriteBatch);
            }
            foreach (EntityController entity in _entities)
            {
                entity.ActualPosition = RoomGridToWorldCoordinatesMap(entity);
                entity.Draw(spriteBatch);
            }
            if (_player != null)
            {
                _player.ActualPosition = RoomGridToWorldCoordinatesMap(_player);
                _player.Draw(spriteBatch);
            }
        }
        //Other Methods
        public void RemoveDoorPossibility(Vector2 direction)
        {
            _whereDoorsCouldBe.Remove(direction);
        }
        public void AddDoor(Vector2 direction)
        {
            //if()
            _whereDoorsAre.Add(direction);
        }
        //Creating Collections Methods
        private void CreateFloor()
        {
            for (int i = 0; i <= _roomHeight; i++)
            {
                for (int j = 0; j <= _roomLength; j++)
                {
                    int scale = 50;
                    float roomXPosition = j;
                    float roomYPosition = i;
                    TileController _floor = new TileController(_floorTile, scale, new Vector2(roomXPosition, roomYPosition), _contentManager);
                    _floors.Add(_floor);
                }
            }
        }
        private void CreateWalls() 
        {
            TileController topWall = null;
            TileController bottomWall = null;
            TileController leftSideWall = null;
            TileController rightSideWall = null;
            DoorController topDoor = null;
            DoorController bottomDoor = null;
            DoorController leftSideDoor = null;
            DoorController rightSideDoor = null;
            CreateDoors(topDoor, bottomDoor, leftSideDoor, rightSideDoor);
            CreateWallsWhereDoorsArent(topWall, bottomWall, leftSideWall, rightSideWall);
            CreateWall(_walls, topWall, _topWallTile, 0, _roomLength / 2 - 1, 50, 0);
            CreateWall(_walls, topWall, _topWallTile, _roomLength / 2 + 1, _roomLength, 50, 0);
            CreateWall(_walls, bottomWall, _bottomWallTile, 0, _roomLength / 2 - 1, 50, _roomHeight);
            CreateWall(_walls, bottomWall, _bottomWallTile, _roomLength / 2 + 1, _roomLength, 50, _roomHeight);
            CreateWall(_walls, leftSideWall, _leftSideWallTile, _roomLength / 2 - 1, _roomHeight + 1, 50, 0);
            CreateWall(_walls, leftSideWall, _leftSideWallTile, 0, _roomHeight/2, 50, 0);
            CreateWall(_walls, rightSideWall, _rightSideWallTile, _roomLength / 2 - 1, _roomHeight + 1, 50, _roomLength);
            CreateWall(_walls, rightSideWall, _rightSideWallTile, 0, _roomHeight/2, 50, _roomLength);
        }
        private void CreateWallsWhereDoorsArent(TileController topWall, TileController bottomWall, TileController leftSideWall, TileController rightSideWall)
        {
            List<Vector2> wallsWhereDoorsArent = new List<Vector2>{ new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0) };
            foreach (var direction in _whereDoorsAre)
            {
                wallsWhereDoorsArent.Remove(direction);
            }
            foreach (Vector2 wallDirection in wallsWhereDoorsArent)
            {
                Vector2 position = new Vector2(7, 6) * wallDirection;
                String positionString = position.X.ToString() + ", " + position.Y.ToString();
                switch (positionString)
                {
                    case "0, 6":
                        CreateWall(_walls, rightSideWall, _rightSideWallTile, _roomHeight / 2, _roomLength / 2 - 1, 50, _roomLength);
                        break;
                    case "0, -6":
                        CreateWall(_walls, leftSideWall, _leftSideWallTile, _roomHeight / 2, _roomLength / 2 - 1, 50, 0);
                        break;
                    case "7, 0":
                        CreateWall(_walls, bottomWall, _bottomWallTile, _roomLength / 2 - 1, _roomLength / 2, 50, _roomHeight);
                        break;
                    case "-7, 0":
                        CreateWall(_walls, topWall, _topWallTile, _roomLength / 2 - 1, _roomLength / 2, 50, 0);
                        break;
                }
            }
        }
        private void CreateEntities() 
        { 
            //to do
        }
        public List<object> CheckDoorCollision()
        {
            foreach (DoorController door in _doors)
            {
                Vector2 doorPosition = door.RoomPosition;
                Vector2 playerPosition = _player.RoomPosition;
                if (doorPosition.X == playerPosition.X &&
                    doorPosition.Y == playerPosition.Y &&
                    door.IsDoorOpen == true)
                {
                    List<object> list = new();
                    list.Add(_player);
                    list.Add(door.DoorDirection);
                    return list;
                }
            }
            return null;
        }
        //Creating Individual Tile Methods
        private void CreateDoors(DoorController topDoor, 
            DoorController bottomDoor, 
            DoorController leftSideDoor, 
            DoorController rightSideDoor)
        {
            foreach (Vector2 doorDirection in _whereDoorsAre)
            {
                Vector2 position = new Vector2(7, 6) * doorDirection;
                String positionString = position.X.ToString() + ", " + position.Y.ToString();
                switch (positionString)
                {
                    case "0, 6":
                        CreateDoor(doorDirection, _doors, rightSideDoor, _rightSideDoorTile, _roomHeight/2, _roomLength / 2 - 1, 50, _roomLength);
                        break;
                    case "0, -6":
                        CreateDoor(doorDirection, _doors, leftSideDoor, _leftSideDoorTile, _roomHeight / 2, _roomLength / 2 - 1, 50, 0);
                        break;
                    case "7, 0":
                        CreateDoor(doorDirection, _doors, bottomDoor, _bottomDoorTile, _roomLength / 2, _roomLength / 2, 50, _roomHeight);
                        break;
                    case "-7, 0":
                        CreateDoor(doorDirection, _doors, topDoor, _topDoorTile, _roomLength / 2, _roomLength / 2, 50, 0);
                        break;
                }
            }
        }
        private void CreateWall(List<TileController> List, 
            TileController TypeOfWall, Texture2D WallTile, 
            int lowerAxisCoordinate, int upperAxisCoordinate, int scale,
            int currentAxisPosition)
        {
            if (WallTile == _leftSideWallTile || WallTile == _rightSideWallTile)
            {
                for (int currentYPosition = lowerAxisCoordinate; currentYPosition < upperAxisCoordinate; currentYPosition++)
                {
                    TypeOfWall = new TileController(WallTile, scale, new Vector2(currentAxisPosition, currentYPosition), _contentManager);
                    List.Add(TypeOfWall);
                }
                return;
            }
            for (int currentXPosition = lowerAxisCoordinate; currentXPosition <= upperAxisCoordinate; currentXPosition++)
            {
                TypeOfWall = new TileController(WallTile, scale, new Vector2(currentXPosition, currentAxisPosition), _contentManager);
                List.Add(TypeOfWall);
            }
        }
        private void CreateDoor(Vector2 DoorDirection, List<DoorController> List,
            DoorController TypeOfDoor, Texture2D DoorTile,
            int lowerAxisCoordinate, int upperAxisCoordinate, int scale,
            int currentAxisPosition)
        {
            if (DoorTile == _leftSideDoorTile || DoorTile == _rightSideDoorTile)
            {
                for (int currentYPosition = lowerAxisCoordinate; currentYPosition < upperAxisCoordinate; currentYPosition++)
                {
                    TypeOfDoor = new DoorController(DoorDirection, DoorTile, scale, new Vector2(currentAxisPosition, currentYPosition), _contentManager);
                    List.Add(TypeOfDoor);
                }
                return;
            }
            for (int currentXPosition = lowerAxisCoordinate; currentXPosition <= upperAxisCoordinate; currentXPosition++)
            {
                TypeOfDoor = new DoorController(DoorDirection, DoorTile, scale, new Vector2(currentXPosition, currentAxisPosition), _contentManager);
                List.Add(TypeOfDoor);
            }
        }
        //Methods Used For Properties
        private int CreateNumberOfMaxChildren()
        {
            int[] numberOfPossibleChildren = { 2, 3, 4 };
            int[] distribution = { 4, 3, 3 };
            List<int> numberOfPossibleChildrenDistribution = new List<int>();
            for (int i = 0; i < distribution.Count(); i++)
            {
                int distributionValue = distribution[i];
                int possibleChildren = numberOfPossibleChildren[i];
                for (int j = 0; j <= distributionValue; j++)
                {
                    numberOfPossibleChildrenDistribution.Add(possibleChildren);
                }
            }
            int numberOfMaxChildrenIndex = _random.Next(numberOfPossibleChildrenDistribution.Count - 1);
            int numberOfMaxChildren = numberOfPossibleChildrenDistribution[numberOfMaxChildrenIndex];
            return numberOfMaxChildren;
        }
        private List<Vector2> CreateChildDirectionsDistribution()
        {
            Vector2[] possibleDirections = { new Vector2(0,-1), new Vector2(0,1), new Vector2(-1,0), new Vector2(1,0) };
            int[] distribution = { 2, 2, 2, 2 };
            List<Vector2> possibleDirectionsOfChildrenDistribution = new List<Vector2>();
            for (int i = 0; i < distribution.Count(); i++)
            {
                int distributionValue = distribution[i];
                Vector2 direction = possibleDirections[i];
                for (int j = 0; j <= distributionValue; j++)
                {
                    possibleDirectionsOfChildrenDistribution.Add(direction);
                }
            }
            return possibleDirectionsOfChildrenDistribution;
        }
        private List<Vector2> CreateChildDirections() 
        {
            List<Vector2> childDirectionsDistribution = CreateChildDirectionsDistribution();
            for (int numberOfChildren = _whereDoorsAre.Count; numberOfChildren < NumberOfMaxChildren; numberOfChildren++) 
            {
                int directionIndex = _random.Next(childDirectionsDistribution.Count - 1);
                Vector2 direction = childDirectionsDistribution[directionIndex];
                if (!_whereDoorsAre.Contains(direction)) 
                {
                    _whereDoorsAre.Add(direction);
                }
                else
                {
                    continue;
                }
            }
            return _whereDoorsAre;
        }
        //Player Related Methods
        public void AddPlayer(PlayerController player, Vector2 enteredRoomDirection)
        {
            Debug.WriteLine("Start of Add Player: " + player.RoomPosition);
            Vector2 inverseOfEnteredDirection = enteredRoomDirection * new Vector2(-1, -1);
            Vector2 something = inverseOfEnteredDirection * new Vector2(7, 6);
            String positionString = something.X.ToString() + ", " + something.Y.ToString();
            switch (positionString)
            {
                case "-0, 6":
                    player.RoomPosition = new Vector2(_roomLength / 2, 0); 
                    break;
                case "-0, -6":
                    player.RoomPosition = new Vector2(_roomLength / 2, _roomHeight);
                    break;
                case "7, -0":
                    player.RoomPosition = new Vector2(0, _roomHeight / 2);
                    break;
                case "-7, -0":
                    player.RoomPosition = new Vector2(_roomLength, _roomHeight / 2);
                    break;
                case "-0, -0":
                    player.RoomPosition = new Vector2(_roomLength / 2, _roomHeight / 2);
                    break;
            }
            _player = player;
            Debug.WriteLine("End of Add Player: " + _player.RoomPosition);
        }
        public void RemovePlayer()
        {
            _player = null;
        }
        //Used Within Draw Methods
        private Vector2 RoomGridToWorldCoordinatesMap(SpriteController Sprite)
        {
            Vector2 spritePosition = Sprite.RoomPosition;
            if (spritePosition.X <= 7)
            {
                spritePosition.X = _screenPosition.X + 50 * -(7 - spritePosition.X);
            }
            else if (spritePosition.X <= 15)
            {
                spritePosition.X = _screenPosition.X + 50 * (spritePosition.X - 7);
            }
            if (spritePosition.Y <= 5)
            {
                spritePosition.Y = _screenPosition.Y + 50 * -(5 - spritePosition.Y);
            }
            else if (spritePosition.Y <= 11)
            {
                spritePosition.Y = _screenPosition.Y + 50 * (spritePosition.Y - 5);
            }
            return spritePosition;
        }
    }
}
