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
using System.Diagnostics;

namespace UndyingWill2D.Controllers
{
    public class SpriteController
    {
        //Fields
        protected Texture2D _texture;
        protected Vector2 _actualPosition;
        protected int _scale;
        protected ContentManager _contentManager;
        //Properties
        public int Scale { get { return _scale; } set { _scale = value; } }
        public Vector2 RoomPosition { get; set;}
        public Vector2 ActualPosition
        {
            get { return _actualPosition; }
            set
            {
                _actualPosition.X = value.X - Scale / 2;
                _actualPosition.Y = value.Y - Scale / 2;
            }
        }
        public Rectangle Rectangle { get { return new Rectangle((int)ActualPosition.X, (int)ActualPosition.Y, _scale, _scale); } }
        //Contrustor
        public SpriteController(Texture2D texture, int scale, Vector2 roomPosition, ContentManager contentManager)
        {
            this._texture = texture;
            this._scale = scale;
            this.RoomPosition = roomPosition;
            this._contentManager = contentManager;
        }
        //Core Methods
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color.White);
        }
    }
}
