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
        List<EntityController> _entities = new List<EntityController>();
        List<Vector2> _wallsWhereDoorsCouldntBe = new List<Vector2> { new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0) };
        List<Vector2> _whereDoorsCanBe = new List<Vector2> {};
        Texture2D _floorTile;
        Texture2D _topWallTile;
        Texture2D _rightSideWallTile;
        Texture2D _leftSideWallTile;
        Texture2D _bottomWallTile;
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
            _floors = new List<TileController>();
            CreateFloor();
            CreateWalls();
            CreateEntities();
        }
        public void RemoveDoorPossibility(Vector2 direction)
        {
            _wallsWhereDoorsCouldntBe.Remove(direction);
            _whereDoorsCanBe.Add(direction);
            Debug.WriteLine("Removed " + direction + " from walls where doors cant be");
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
            WallsWhereDoorsCouldntBe(topWall, bottomWall, leftSideWall, rightSideWall);
            CreateWall(_walls, topWall, _topWallTile, 0, 6, 50, 0);
            CreateWall(_walls, topWall, _topWallTile, 8, 14, 50, 0);
            CreateWall(_walls, bottomWall, _bottomWallTile, 0, 6, 50, 10);
            CreateWall(_walls, bottomWall, _bottomWallTile, 8, 14, 50, 10);
            CreateWall(_walls, leftSideWall, _leftSideWallTile, 6, 11, 50, 0);
            CreateWall(_walls, leftSideWall, _leftSideWallTile, 0, 5, 50, 0);
            CreateWall(_walls, rightSideWall, _rightSideWallTile, 6, 11, 50, 14);
            CreateWall(_walls, rightSideWall, _rightSideWallTile, 0, 5, 50, 14);
        }
        private void WallsWhereDoorsCouldntBe(TileController topWall,
        TileController bottomWall,
        TileController leftSideWall,
        TileController rightSideWall)
        {
            foreach (Vector2 wallDirection in _wallsWhereDoorsCouldntBe) 
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
        private void CreateEntities() 
        { 
            //to do
        }
        private int CreateNumberOfMaxChildren()
        {

            int[] numberOfPossibleChildren = { 2, 3, 4 };
            int[] distribution = { 0, 10, 0 };
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
            int[] distribution = { 4, 3, 3 };
            List<Vector2> possibleDirectionsOfChildrenDistribution = new List<Vector2>();
            for (int i = 0; i < distribution.Count(); i++)
            {
                int distributionValue = distribution[i];
                //possibleDirections will never have all directions possible
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
            List<Vector2> childDirections = new List<Vector2>();
            List<Vector2> childDirectionsDistribution = CreateChildDirectionsDistribution();
            for (int numberOfChildren = 0; numberOfChildren < NumberOfMaxChildren; numberOfChildren++) 
            {
                int directionIndex = _random.Next(childDirectionsDistribution.Count - 1);
                Vector2 direction = childDirectionsDistribution[directionIndex];
                if (!childDirections.Contains(direction)) 
                {
                    childDirections.Add(direction);
                }
                else
                {
                    continue;
                }
                _wallsWhereDoorsCouldntBe.Remove(direction);
            }
            return childDirections;
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
            for (int floorIndex = 0; floorIndex < _floors.Count(); floorIndex++)
            {
                TileController currentFloor = _floors[floorIndex];
                currentFloor.Position = RoomGridToWorldCoordinatesMap(currentFloor);
                currentFloor.Draw(spriteBatch);
            }
            //for (int objectIndex = 0; objectIndex < _objectsInRoom.Count(); objectIndex++)
            //{
            //    SpriteController currentObject = _objectsInRoom[objectIndex];
            //    currentObject.Draw(spriteBatch);
            //}
            for (int wallIndex = 0; wallIndex < _walls.Count(); wallIndex++)
            {
                TileController currentWall = _walls[wallIndex];
                currentWall.Position = RoomGridToWorldCoordinatesMap(currentWall);
                currentWall.Draw(spriteBatch);
            }
            //for (int currentEntity = 0; currentEntity < _entities.Count(); currentEntity++)
            //{
            //    EntityController currentEntity = _entities[currentEntity];
            //    currentEntity.Draw(spriteBatch);
            //}
        }
    }
}
