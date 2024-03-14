using System;
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
        List<MobController> _mobs = new List<MobController>();
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
        Texture2D _enemy;
        //Other Fields
        ContentManager _contentManager;
        Vector2 _screenPosition;
        Random _random = new Random();
        int? _numberOfMaxChildren;
        PlayerController _player;
        bool _isPlayerHasBeenToRoom;
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
            if (_isPlayerHasBeenToRoom == false) 
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
                _enemy = _contentManager.Load<Texture2D>("SkeletonAnimation");
                _floors = new List<TileController>();
                CreateFloor();
                CreateWalls();
                CreateMobs();
            }
        }
        public void LoadContent()
        {
            foreach (EntityController entity in _mobs)
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
            if (_player != null)
            {
                _player.Update(_roomHeight, _roomLength);
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.K))
                {
                    foreach (DoorController door in _doors) 
                    { door.CloseDoor(); }
                }
                if (keyboardState.IsKeyDown(Keys.J))
                {
                    foreach (DoorController door in _doors)
                    { door.OpenDoor(); }
                }
            }
            foreach (MobController entity in _mobs)
            {
                entity.Update(_roomHeight, _roomLength);
                entity.MobMovement(_player.Collision, _player.RoomPosition);
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
                if(door.IsDoorOpen == false)
                {
                    door.ActualPosition = RoomGridToWorldCoordinatesMap(door);
                    door.Draw(spriteBatch);
                }
            }
            foreach (MobController mob in _mobs)
            {
                mob.ActualPosition = RoomGridToWorldCoordinatesMap(mob);
                ItemController item = mob.Draw(spriteBatch);
                if (item != null)
                {
                    item.ActualPosition = RoomGridToWorldCoordinatesMap(item);
                    item.Draw(spriteBatch);
                }
            }
            if (_player != null)
            {
                _player.ActualPosition = RoomGridToWorldCoordinatesMap(_player);
                ItemController item =_player.Draw(spriteBatch);
                if (item != null)
                {
                    item.ActualPosition = RoomGridToWorldCoordinatesMap(item);
                    item.Draw(spriteBatch);
                }
            }
        }
        //Other Methods
        public void AddDoor(Vector2 direction)
        {
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
                    case "7, 0":
                        CreateWall(_walls, rightSideWall, _rightSideWallTile, _roomHeight / 2, _roomLength / 2 - 1, 50, _roomLength);
                        break;
                    case "-7, 0":
                        CreateWall(_walls, leftSideWall, _leftSideWallTile, _roomHeight / 2, _roomLength / 2 - 1, 50, 0);
                        break;
                    case "0, 6":
                        CreateWall(_walls, bottomWall, _bottomWallTile, _roomLength / 2 - 1, _roomLength / 2, 50, _roomHeight);
                        break;
                    case "0, -6":
                        CreateWall(_walls, topWall, _topWallTile, _roomLength / 2 - 1, _roomLength / 2, 50, 0);
                        break;
                }
            }
        }
        private void CreateMobs() 
        {
            List<Vector2> possibleEnemySpawnLocation = 
                new List<Vector2>()
                { 
                    new Vector2(3, 2),
                    new Vector2(11, 2),
                    new Vector2(3, 8),
                    new Vector2(11, 8),
                };
            List<int> numberOfPossibleEnemies = new List<int>() { 1, 2, 3, 4};
            int numberOfEnemiesIndex = _random.Next(numberOfPossibleEnemies.Count - 1);
            int numberOfEnemies = numberOfPossibleEnemies[numberOfEnemiesIndex];
            for (int i = 0; i <= numberOfEnemies; i++)
            {
                int enemiesSpawnLocationIndex = _random.Next(possibleEnemySpawnLocation.Count - 1);
                Vector2 enemiesSpawnLocation = possibleEnemySpawnLocation[enemiesSpawnLocationIndex];
                possibleEnemySpawnLocation.Remove(enemiesSpawnLocation);
                MobController enemy = new MobController(_enemy, 90, enemiesSpawnLocation, _contentManager);
                _mobs.Add(enemy);
            }

        }
        public List<object> CheckDoorCollision()
        {
            foreach (DoorController door in _doors)
            {
                Vector2 doorPosition = door.RoomPosition;
                Vector2 playerPosition = _player.RoomPosition;
                bool isThereACollision = doorPosition.X == (int)(playerPosition.X + 0.5f) &&
                    doorPosition.Y == (int)(playerPosition.Y + 0.5f) &&
                    door.IsDoorOpen == true;
                if (isThereACollision)
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
                int doorX;
                int doorY;
                Vector2 direction = doorDirection + new Vector2(0, 0);
                String directionString = direction.X.ToString() + ", " + direction.Y.ToString();
                switch (directionString)
                {
                    case "1, 0":
                        doorX = _roomLength;
                        doorY = _roomHeight / 2;
                        CreateDoor(doorDirection, _doors, rightSideDoor, _rightSideDoorTile, doorY, doorX, 50);
                        break;
                    case "-1, 0":
                        doorX = 0;
                        doorY = _roomHeight / 2;
                        CreateDoor(doorDirection, _doors, leftSideDoor, _leftSideDoorTile, doorY, doorX, 50);
                        break;
                    case "0, 1":
                        doorX = _roomLength / 2;
                        doorY = _roomHeight;
                        CreateDoor(doorDirection, _doors, bottomDoor, _bottomDoorTile, doorX, doorY, 50);
                        break;
                    case "0, -1":
                        doorX = _roomLength / 2;
                        doorY = 0;
                        CreateDoor(doorDirection, _doors, topDoor, _topDoorTile, doorX, doorY, 50);
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
            int AxisLocation, int otherAxisPosition, int scale)
        {
            if (DoorTile == _leftSideDoorTile || DoorTile == _rightSideDoorTile)
            {
                TypeOfDoor = new DoorController(DoorDirection, DoorTile, scale, new Vector2(otherAxisPosition, AxisLocation), _contentManager);
                List.Add(TypeOfDoor);
                return;
            }
            TypeOfDoor = new DoorController(DoorDirection, DoorTile, scale, new Vector2(AxisLocation, otherAxisPosition), _contentManager);
            List.Add(TypeOfDoor);
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
            for (int numberOfChildren = _childDirections.Count; numberOfChildren < NumberOfMaxChildren; numberOfChildren++) 
            {
                int directionIndex = _random.Next(childDirectionsDistribution.Count - 1);
                Vector2 direction = childDirectionsDistribution[directionIndex];
                if (!_childDirections.Contains(direction)) 
                {
                    _childDirections.Add(direction);
                }
                else
                {
                    continue;
                }
            }
            return _childDirections;
        }
        //Player Related Methods
        public void AddPlayer(PlayerController player, Vector2 enteredRoomDirection)
        {
            _isPlayerHasBeenToRoom = true;
            Vector2 DirectionOfDoorEnteredThrough = enteredRoomDirection + new Vector2(0, 0);
            String positionString = DirectionOfDoorEnteredThrough.X.ToString() + ", " + DirectionOfDoorEnteredThrough.Y.ToString();
            switch (positionString)
            {
                case "0, 1":
                    player.RoomPosition = new Vector2(_roomLength / 2, 1); 
                    break;
                case "0, -1":
                    player.RoomPosition = new Vector2(_roomLength / 2, _roomHeight - 1);
                    break;
                case "1, 0":
                    player.RoomPosition = new Vector2(1, _roomHeight / 2);
                    break;
                case "-1, 0":
                    player.RoomPosition = new Vector2(_roomLength - 1, _roomHeight / 2 );
                    break;
                case "0, 0":
                    player.RoomPosition = new Vector2(_roomLength / 2, _roomHeight / 2);
                    break;
            }
            _player = player;
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
