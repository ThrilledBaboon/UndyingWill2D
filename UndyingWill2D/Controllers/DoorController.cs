using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace UndyingWill2D.Controllers
{
    public class DoorController : TileController
    {
        //Fields
        Vector2 _doorDirection;
        bool _isDoorOpen;
        //Properties
        public Vector2 DoorDirection { get { return _doorDirection; } }
        public bool IsDoorOpen { get { return _isDoorOpen; } }
        public Rectangle collisionRectangle
        {
            get
            {
                return new Microsoft.Xna.Framework.Rectangle((int)RoomPosition.X, (int)RoomPosition.Y, Scale, Scale);
            }
        }
        //Constructor
        public DoorController(Vector2 doorDirection, Texture2D texture, int scale, Vector2 position, ContentManager contentManager) :base(texture, scale, position, contentManager)
        {
            this._doorDirection = doorDirection;
            _isDoorOpen = false;
        }
        //Other Methods
        public void CloseDoor()
        {
            _isDoorOpen = false;
        }
        public void OpenDoor()
        {
            _isDoorOpen = true;
        }
    }
}
