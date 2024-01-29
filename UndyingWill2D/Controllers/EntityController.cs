using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UndyingWill2D.Managers;

namespace UndyingWill2D.Controllers
{
    public class EntityController : SpriteController
    {
        AnimationManager _animationManager;

        //Fields
        protected int _health;
        protected bool _isAlive;
        protected Vector2 _moveDirection;
        protected int _moveSpeed;
        private bool _isMoving;
        //Property
        public bool IsMoving { get { return _isMoving; } set { _isMoving = value; } }
        public int Health { get {return _health;} set {_health = value;}}
        public bool IsAlive {get {return _isAlive;} set {_isAlive = value;}}
        
        //Constructor
        public EntityController(Texture2D texture, int scale, Vector2 position, ContentManager contentManager) :base(texture, scale, position, contentManager)
        { }
        //Methods
        public virtual void Update()
        {
            HandleInput();
            Debug.WriteLine(_isMoving);
            _animationManager.Update(_isMoving);
        }

        public override void LoadContent()
        {
            _texture = _contentManager.Load<Texture2D>(_texture.ToString());
            _animationManager = new(2, 2, new Vector2(32, 32));
            _animationManager.GetFrame();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, _animationManager.GetFrame(), Color.White);
            if (_isAlive)
            {

            }
        }

        public virtual void HandleInput() 
        {

        }
        public void OnMove(Vector2 moveDirection, float moveSpeed)
        {
            _isMoving= true;
            if (moveDirection != Vector2.Zero)
            {
                moveDirection.Normalize();
            }
            Vector2 moveVelocity = moveDirection * moveSpeed;
            Debug.WriteLine(moveVelocity);
            _position += moveVelocity;
        }
        public void OnAttack(MouseState mouse)
        {

        }
        public void OnStun()
        {

        }


    }
}
