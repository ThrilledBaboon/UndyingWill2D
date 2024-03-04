using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        List<TileController> _floors = new List<TileController>();
        List<TileController> _walls = new List<TileController>();
        List<DoorController> _doors = new List<DoorController> { };
        List<EntityController> _entities = new List<EntityController>();
        //List<Vector2> _wallsWhereDoorsCouldntBe = new List<Vector2> { new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0) };
        List<Vector2> _whereDoorsCouldBe = new List<Vector2> { new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0) };
        List<Vector2> _whereDoorsAre = new List<Vector2> {};
        Texture2D _floorTile;
        Texture2D _topWallTile;
        Texture2D _rightSideWallTile;
        Texture2D _leftSideWallTile;
        Texture2D _bottomWallTile;
        Texture2D _topDoorTile;
        Texture2D _rightSideDoorTile;
        Texture2D _leftSideDoorTile;
        Texture2D _bottomDoorTile;
        ContentManager _contentManager;
        Vector2 _screenPosition;
        Random _random = new Random();
        public Vector2 RoomOrigin {  get; set; }
        List<Vector2> _childDirections = new List<Vector2>();
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
        int? _numberOfMaxChildren;
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
        public RoomManager(ContentManager contentManager, Vector2 origin, Vector2 screenPosition) 
        { 
            this._contentManager = contentManager;
            this.RoomOrigin = origin;
            this._screenPosition = screenPosition;
        }
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
        public void RemoveDoorPossibility(Vector2 direction)
        {
            _whereDoorsCouldBe.Remove(direction);
            Debug.WriteLine("Removed " + direction + " from walls where doors cant be");
        }
        public void AddDoor(Vector2 direction)
        {
            //if()
            _whereDoorsAre.Add(direction);
        }
        private void CreateFloor()
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 15; j++)
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
            //WallsWhereDoorsCouldntBe(topWall, bottomWall, leftSideWall, rightSideWall);
            CreateDoors(topDoor, bottomDoor, leftSideDoor, rightSideDoor);
            CreateWallsWhereDoorsArent(topWall, bottomWall, leftSideWall, rightSideWall);
            CreateWall(_walls, topWall, _topWallTile, 0, 6, 50, 0);
            CreateWall(_walls, topWall, _topWallTile, 8, 14, 50, 0);
            CreateWall(_walls, bottomWall, _bottomWallTile, 0, 6, 50, 10);
            CreateWall(_walls, bottomWall, _bottomWallTile, 8, 14, 50, 10);
            CreateWall(_walls, leftSideWall, _leftSideWallTile, 6, 11, 50, 0);
            CreateWall(_walls, leftSideWall, _leftSideWallTile, 0, 5, 50, 0);
            CreateWall(_walls, rightSideWall, _rightSideWallTile, 6, 11, 50, 14);
            CreateWall(_walls, rightSideWall, _rightSideWallTile, 0, 5, 50, 14);
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
                        CreateWall(_walls, rightSideWall, _rightSideWallTile, 5, 6, 50, 14);
                        break;
                    case "0, -6":
                        CreateWall(_walls, leftSideWall, _leftSideWallTile, 5, 6, 50, 0);
                        break;
                    case "7, 0":
                        CreateWall(_walls, bottomWall, _bottomWallTile, 6, 7, 50, 10);
                        break;
                    case "-7, 0":
                        CreateWall(_walls, topWall, _topWallTile, 6, 7, 50, 0);
                        break;
                }
            }
        }
        private void CreateDoors(DoorController topDoor, 
            DoorController bottomDoor, 
            DoorController leftSideDoor, 
            DoorController rightSideDoor)
        {
            Debug.WriteLine("All Door Directions and their coordinate for Room " + RoomOrigin);
            foreach (Vector2 doorDirection in _whereDoorsAre)
            {
                Debug.WriteLine(doorDirection);
                Vector2 position = new Vector2(7, 6) * doorDirection;
                Debug.WriteLine(position);
                String positionString = position.X.ToString() + ", " + position.Y.ToString();
                Debug.WriteLine(positionString);
                switch (positionString)
                {
                    case "0, 6":
                        CreateDoor(_doors, rightSideDoor, _rightSideDoorTile, 5, 6, 50, 14);
                        break;
                    case "0, -6":
                        CreateDoor(_doors, leftSideDoor, _leftSideDoorTile, 5, 6, 50, 0);
                        break;
                    case "7, 0":
                        CreateDoor(_doors, bottomDoor, _bottomDoorTile, 7, 7, 50, 10);
                        break;
                    case "-7, 0":
                        CreateDoor(_doors, topDoor, _topDoorTile, 7, 7, 50, 0);
                        break;
                }
                Debug.WriteLine("");
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
        private void CreateDoor(List<DoorController> List,
            DoorController TypeOfDoor, Texture2D DoorTile,
            int lowerAxisCoordinate, int upperAxisCoordinate, int scale,
            int currentAxisPosition)
        {
            if (DoorTile == _leftSideDoorTile || DoorTile == _rightSideDoorTile)
            {
                for (int currentYPosition = lowerAxisCoordinate; currentYPosition < upperAxisCoordinate; currentYPosition++)
                {
                    TypeOfDoor = new DoorController(DoorTile, scale, new Vector2(currentAxisPosition, currentYPosition), _contentManager);
                    Debug.WriteLine(TypeOfDoor.Position);
                    List.Add(TypeOfDoor);
                }
                return;
            }
            for (int currentXPosition = lowerAxisCoordinate; currentXPosition <= upperAxisCoordinate; currentXPosition++)
            {
                TypeOfDoor = new DoorController(DoorTile, scale, new Vector2(currentXPosition, currentAxisPosition), _contentManager);
                Debug.WriteLine(TypeOfDoor.Position);
                List.Add(TypeOfDoor);
            }
        }
        private void CreateEntities() 
        { 
            //to do
        }
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
        public void AddEntity()
        {

        }
        public void RemoveEntity()
        {

        }
        private Vector2 RoomGridToWorldCoordinatesMap(TileController Sprite)
        {
            Vector2 spritePosition = Sprite.Position;
            if (spritePosition.X <= 7)
            {
                spritePosition.X = _screenPosition.X + Sprite.Scale * -(7 - spritePosition.X);
            }
            else if (spritePosition.X <= 15)
            {
                spritePosition.X = _screenPosition.X + Sprite.Scale * (spritePosition.X - 7);
            }
            if (spritePosition.Y <= 5)
            {
                spritePosition.Y = _screenPosition.Y + Sprite.Scale * -(5 - spritePosition.Y);
            }
            else if (spritePosition.Y <= 11)
            {
                spritePosition.Y = _screenPosition.Y + Sprite.Scale * (spritePosition.Y - 5);
            }
            return spritePosition;
        }
        public void Update() 
        {

        }
        public void LoadContent() 
        { 
        
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(TileController floor in _floors)
            {
                floor.Position = RoomGridToWorldCoordinatesMap(floor);
                floor.Draw(spriteBatch);
            }
            //for (int objectIndex = 0; objectIndex < _objectsInRoom.Count(); objectIndex++)
            //{
            //    SpriteController currentObject = _objectsInRoom[objectIndex];
            //    currentObject.Draw(spriteBatch);
            //}
            foreach (TileController wall in _walls)
            {
                wall.Position = RoomGridToWorldCoordinatesMap(wall);
                wall.Draw(spriteBatch);
            }
            foreach(DoorController door in _doors)
            {
                door.Position = RoomGridToWorldCoordinatesMap(door);
                door.Draw(spriteBatch);
            }
            //for (int currentEntity = 0; currentEntity < _entities.Count(); currentEntity++)
            //{
            //    EntityController currentEntity = _entities[currentEntity];
            //    currentEntity.Draw(spriteBatch);
            //}
        }
    }
}
