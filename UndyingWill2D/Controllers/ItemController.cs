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
        AnimationManager _animationManager;
        bool _hasAttacked;
        Rectangle frameRectangle;
        public ItemController(Texture2D texture, int scale, Vector2 position, ContentManager contentManager) : base(texture, scale, position, contentManager)
        {
            _animationManager = new AnimationManager(2, 2, new Vector2(32, 32), 0);
        }

        public void Update(Vector2 OwnerPosition)
        {
            _position.X = OwnerPosition.X + _scale/2;
            _position.Y = OwnerPosition.Y + _scale / 2;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_hasAttacked)
            {
                frameRectangle = _animationManager.Attack();
                spriteBatch.Draw(_texture, Rectangle, frameRectangle, Color.White);
                frameRectangle = _animationManager.Attack();
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
