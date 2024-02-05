using System;
using System.Collections.Generic;
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
        Rectangle frameRectangle;
        String _type;
        public ItemController(string type, Texture2D texture, int scale, Vector2 position, ContentManager contentManager) : base(texture, scale, position, contentManager)
        {
            this._type = type;
            switch (_type)
            {
                case "Sword":
                    _itemAnimationManager = new ItemAnimationManager(2, 2, new Vector2(32, 32), 0);
                    break;
                case "Bow":
                    _itemAnimationManager = new ItemAnimationManager(3, 2, new Vector2(32, 32), 0);
                    break;
                case "Shield":
                    _itemAnimationManager = new ItemAnimationManager(2, 2, new Vector2(32, 32), 0);
                    break;
            }
        }

        public void Update(Vector2 OwnerPosition)
        {
            _position.X = OwnerPosition.X + _scale/2;
            _position.Y = OwnerPosition.Y + 10;
            _itemAnimationManager.Update(_hasAttacked);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_hasAttacked && _type == "Sword")
            {
                frameRectangle = _itemAnimationManager.Attack();
                spriteBatch.Draw(_texture, Rectangle, frameRectangle, Color.White);
                _hasAttacked = false;
            }
            else if (_hasAttacked && _type == "Bow")
            {
                // need to add a for time holding down button 
                frameRectangle = _itemAnimationManager.Attack();
                spriteBatch.Draw(_texture, Rectangle, frameRectangle, Color.White);
                _hasAttacked = false;
            }
        }

        public void Attack()
        {
            _hasAttacked = true;
            
        }
    }
}
