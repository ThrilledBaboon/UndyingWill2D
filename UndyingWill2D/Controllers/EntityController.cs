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
        protected Vector2 _moveDirection;
        protected ItemController _weapon;
        protected int _moveSpeed;
        //Property
        public AnimationManager AnimationManager { get; private set; }
        public bool IsMoving { get; set; }
        public int Health { get; set; }
        public bool IsAlive { get; set; }
        
        //Constructor
        public EntityController(Texture2D texture, int scale, Vector2 position, ContentManager contentManager) :base(texture, scale, position, contentManager)
        {
            Vector2 weaponPosition = new Vector2(position.X + scale / 2, position.Y);
            //Texture2D weaponTexture = contentManager.Load<Texture2D>("SwordAnimation");
            //_weapon = new ItemController("Sword", weaponTexture, scale, weaponPosition, contentManager);
            Texture2D weaponTexture = contentManager.Load<Texture2D>("BowAnimation");
            _weapon = new ItemController("Bow", weaponTexture, scale, weaponPosition, contentManager);
        }
        //Methods
        public virtual void Update()
        {
            HandleInput();
            _weapon.Update(_position);
        }

        public void LoadContent()
        {
            AnimationManager = new(2, 2, new Vector2(32, 32), 1);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, AnimationManager.GetFrame(), Color.White);
            _weapon.Draw(spriteBatch);
            if (IsAlive)
            {

            }
        }

        public virtual void HandleInput() 
        {

        }
        public virtual void OnMove(Vector2 moveDirection, float moveSpeed)
        {
            if (moveDirection == Vector2.Zero) { IsMoving = false; }
            else { IsMoving = true; }
            AnimationManager.Update(IsMoving);
            if (moveDirection != Vector2.Zero)
            {
                moveDirection.Normalize();
            }
            Vector2 moveVelocity = moveDirection * moveSpeed;
            Debug.WriteLine(moveVelocity);
            _position += moveVelocity;
        }
        public void OnAttack(Point point)
        {
            _weapon.Attack();
        }
        public void OnStun()
        {

        }
        public virtual void OnBlock(Point point)
        {

        }


    }
}
