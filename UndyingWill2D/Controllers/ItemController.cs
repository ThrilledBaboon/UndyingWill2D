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
        Vector2 _mouseDirection;
        //Properties
        public new Rectangle Rectangle 
        {
            get 
            {
                float swordX;
                float swordY;
                Vector2 direction = new Vector2((int)(_mouseDirection.X + 0.5f), (int)(_mouseDirection.Y + 0.5f));
                direction.Normalize();
                String directionString = direction.X.ToString() + ", " + direction.Y.ToString();
                switch (directionString)
                {
                    case "1, 0":
                        swordX = RoomPosition.X + direction.X ;
                        swordY = RoomPosition.Y;
                        break;
                    case "-1, 0":
                        swordX = RoomPosition.X - direction.X;
                        swordY = RoomPosition.Y;
                        break;
                    case "0, 1":
                        swordX = RoomPosition.X;
                        swordY = RoomPosition.Y + direction.Y;
                        break;
                    case "0, -1":
                        swordX = RoomPosition.X;
                        swordY = RoomPosition.Y - direction.Y;
                        RoomPosition = new Vector2 (swordX, swordY);
                        break;
                }
                return new Rectangle((int)ActualPosition.X, (int)ActualPosition.Y, _scale, _scale);
            } 
        }
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
        public void Update(Vector2 OwnerPosition, bool isAttacking, Vector2 MouseDirectionFromPlayer)
        {
            RoomPosition = OwnerPosition;
            _itemAnimationManager.Update(isAttacking);
            _mouseDirection = MouseDirectionFromPlayer ;
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
