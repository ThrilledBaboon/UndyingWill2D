using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UndyingWill2D.Controllers;

namespace UndyingWill2D.Managers
{
    public class RoomManager
    {
        List<TileController> _tiles;
        TileController _floor;
        Texture2D FloorTile;
        ContentManager _contentManager;
        Vector2 _roomOrigin = Vector2.Zero;

        public RoomManager(ContentManager contentManager) 
        { this._contentManager = contentManager; }

        public void Initialise() 
        {
            FloorTile = _contentManager.Load<Texture2D>("FloorTile");
            _tiles = new List<TileController>();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    int scale = 50;
                    //this will need to be calculated based on things from constructor
                    float currentXPosition = j * scale;
                    float currentYPosition = i * scale;
                    //
                    _floor = new TileController(FloorTile, scale, new Vector2(currentXPosition, currentYPosition), _contentManager);
                    _tiles.Add(_floor);
                }
            }
        }

        public void Update() { }

        public void LoadContent() { }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _tiles.Count; i++)
            {
                TileController floor = _tiles[i];
                floor.Draw(spriteBatch);
            }
        }
    }
}
