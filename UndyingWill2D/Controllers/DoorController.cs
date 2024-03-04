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
        bool _isDoorOpen;
        private static Texture2D texture;

        public DoorController(Texture2D openTexture, Texture2D closedTexture, int scale, Vector2 position, ContentManager contentManager) :base(texture, scale, position, contentManager)
        {
            _isDoorOpen = false;
        }
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
