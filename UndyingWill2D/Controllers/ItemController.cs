using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UndyingWill2D.Managers;

namespace UndyingWill2D.Controllers
{
    public class ItemController : SpriteController
    {
        ItemAnimationManager _itemAnimationManager;
        bool _hasAttacked;
        Rectangle _frameRectangle;
        String _type;
        //Constructor
        public ItemController(string type, Texture2D texture, int scale, Vector2 position, ContentManager contentManager) : base(texture, scale, position, contentManager)
        {
            this._type = type;
            switch (_type)
            {
                case "Sword":
                    _itemAnimationManager = new ItemAnimationManager(2, 2, new Vector2(32, 32), 1);
                    break;
            }
        }
        //Core Methods
        public void Update(Vector2 OwnerPosition, bool isAttacking)
        {
            RoomPosition = OwnerPosition;
            _itemAnimationManager.Update(isAttacking);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _frameRectangle = _itemAnimationManager.NextFrameRect;
            if (!_frameRectangle.IsEmpty) 
            { 
                spriteBatch.Draw(_texture, Rectangle, _frameRectangle, Color.White);
            }
        }
            
    }
}
