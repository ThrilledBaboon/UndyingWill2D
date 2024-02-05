using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        protected ItemController _bow;
        protected ItemController _sword;
        protected ItemController _shield;
        protected float _moveSpeed;
        protected List<ItemController> _hotBar = new List<ItemController>();
        protected int _currentHotBarIndex;
        //Property
        public WalkAnimationManager WalkAnimationManager { get; private set; }
        public bool IsMoving { get; set; }
        public int Health { get; set; }
        public bool IsAlive { get; set; }
        
        //Constructor
        public EntityController(Texture2D texture, int scale, Vector2 position, ContentManager contentManager) :base(texture, scale, position, contentManager)
        {
            Vector2 weaponPosition = new Vector2(position.X + scale / 2, position.Y);
            //Texture2D weaponTexture = contentManager.Load<Texture2D>("SwordAnimation");
            //_weapon = new ItemController("Sword", weaponTexture, scale, weaponPosition, contentManager);
            Texture2D bowTexture = contentManager.Load<Texture2D>("BowAnimation");
            Texture2D swordTexture = contentManager.Load<Texture2D>("SwordAnimation");
            Texture2D shieldTexture = contentManager.Load<Texture2D>("SwordAnimation");
            _bow = new ItemController("Bow", bowTexture, scale, weaponPosition, contentManager);
            _sword = new ItemController("Sword", swordTexture, scale, weaponPosition, contentManager);
            _shield = new ItemController("Shield", swordTexture, scale, weaponPosition, contentManager);
            _hotBar.Add(_sword);
            _hotBar.Add(_bow);
            _hotBar.Add(_shield);
            _currentHotBarIndex = 0;
        }
        //Methods
        public virtual void Update()
        {
            HandleInput();
            ItemController heldItem = _hotBar[_currentHotBarIndex];
            heldItem.Update(_position);
        }

        public void LoadContent()
        {
            WalkAnimationManager = new(2, 2, new Vector2(32, 32), 1);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, WalkAnimationManager.GetFrame(), Color.White);
            ItemController heldItem = _hotBar[_currentHotBarIndex];
            heldItem.Draw(spriteBatch);
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
            WalkAnimationManager.Update(IsMoving);
            if (moveDirection != Vector2.Zero)
            {
                moveDirection.Normalize();
            }
            Vector2 moveVelocity = moveDirection * moveSpeed;
            _position += moveVelocity;
        }
        public void OnAttack(Point point)
        {
            ItemController heldItem = _hotBar[_currentHotBarIndex];
            heldItem.Attack();
        }
        public void OnStun()
        {

        }
        public virtual void OnBlock(Point point)
        {

        }
    }
}
