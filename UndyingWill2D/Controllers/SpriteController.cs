using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UndyingWill2D.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndyingWill2D.Controllers
{
    public class SpriteController
    {
        protected Texture2D _texture;
        protected Vector2 _position;
        protected int _scale;
        protected ContentManager _contentManager;
        protected SpriteBatch _spriteBatch;
        
        public Rectangle Rectangle { get { return new Rectangle((int)_position.X, (int)_position.Y, _scale, _scale); } }

        public SpriteController(Texture2D texture, int scale, Vector2 position, ContentManager contentManager)
        {
            this._texture = texture;
            this._scale = scale;
            this._position = position;
            this._contentManager = contentManager;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color.White);
        }
    }
}
