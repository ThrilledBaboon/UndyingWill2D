using System;
using System.Collections;
using System.Collections.Generic;
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
        Texture2D FloorTile;
        Texture2D TopWallTile;
        Texture2D SideWallTile;
        Texture2D BottomWallTile;
        ContentManager _contentManager;
        Vector2 _roomOrigin = Vector2.Zero;

        public RoomManager(ContentManager contentManager, Vector2 origin) 
        { 
            this._contentManager = contentManager;
            this._roomOrigin = origin;
        }
        public void Initialise() 
        {
            FloorTile = _contentManager.Load<Texture2D>("FloorTile");
            TopWallTile = _contentManager.Load<Texture2D>("TopWall");
            //SideWallTile = _contentManager.Load<Texture2D>("SideWall");
            //BottomWallTile = _contentManager.Load<Texture2D>("BottomWall");
            _floors = new List<TileController>();

            CreateFloor();
            CreateWalls();
            CreateEntities();
        }
        public void CreateFloor()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    int scale = 50;
                    float roomXPosition = j;
                    float roomYPosition = i;
                    TileController _floor = new TileController(FloorTile, scale, new Vector2(roomXPosition, roomYPosition), _contentManager);
                    _floors.Add(_floor);
                }
            }
        }
        public void CreateWalls() 
        {;
            TileController topWall = null;
            TileController sideWall = null;
            CreateWall(_walls, topWall, TopWallTile, 1, 4, 50, 0);
            CreateWall(_walls, sideWall, FloorTile, 5, 9, 50, 0);
            CreateWall(_walls, sideWall, FloorTile, 1, 4, 50, 13);
            CreateWall(_walls, sideWall, FloorTile, 1, 4, 50, 13);
        }
        public void CreateWall(List<TileController> List, 
            TileController TypeOfWall, Texture2D WallTile, 
            int lowerYCoordinate, int upperYCoordinate, int scale,
            int currentXPosition)
        {
            for (int currentYPosition = lowerYCoordinate; currentYPosition < upperYCoordinate; currentYPosition++)
            {
                TypeOfWall = new TileController(WallTile, scale, new Vector2(currentXPosition, currentYPosition), _contentManager);
                List.Add(TypeOfWall);
            }
        }
        public void CreateEntities() 
        { 
            //to do
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
                TileController currentObject = _floors[floorIndex];
                //mapping local grid to actual coordinates function called here
                Vector2 currentObjectPosition = currentObject.Position;
                if (currentObjectPosition.X < 6)
                {
                    currentObjectPosition.X = _roomOrigin.X + currentObject.Scale * -(6 - currentObjectPosition.X);
                }
                if (currentObjectPosition.Y < 4)
                {
                    currentObjectPosition.Y = _roomOrigin.Y + currentObject.Scale * -(4 - currentObjectPosition.Y);
                }
                currentObject.Position = currentObjectPosition;
                currentObject.Draw(spriteBatch);
            }
            //for (int i = 0; i < _objectsInRoom.Count(); i++)
            //{
            //    SpriteController currentObject = _objectsInRoom[i];
            //    currentObject.Draw(spriteBatch);
            //}
            for (int i = 0; i < _walls.Count(); i++)
            {
                TileController currentObject = _walls[i];
                currentObject.Draw(spriteBatch);
            }
            for (int i = 0; i < _entities.Count(); i++)
            {
                EntityController currentObject = _entities[i];
                currentObject.Draw(spriteBatch);
            }
        }
    }
}
