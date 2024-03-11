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
        //Item Fields
        protected List<ItemController> _hotBar = new List<ItemController>();
        protected int _currentHotBarIndex;
        protected ItemController _sword;
        protected ItemController _heldItem;
        protected bool _previouslyAttacking = false;
        protected bool _isAttacking = false;
        //Move Fields
        protected Vector2 _moveDirection;
        protected float _moveSpeed;
        //Room Fields
        protected int _roomHeight;
        protected int _roomLength;
        //Properties
        public WalkAnimationManager WalkAnimationManager { get; private set; }
        public bool IsMoving { get; set; }
        public int Health { get; set; }
        public bool IsAlive { get; set; }
        public Rectangle Collision {  get; set; }
        public Texture2D SpriteAnimation { get; private set; }
        public Vector2 MouseDirection { get; set; }
        //Constructor
        public EntityController(Texture2D texture, int scale, Vector2 roomPosition, ContentManager contentManager) :base(texture, scale, roomPosition, contentManager)
        {
            Vector2 weaponPosition = new Vector2(roomPosition.X + scale / 2, roomPosition.Y);
            //Texture2D weaponTexture = contentManager.Load<Texture2D>("SwordAnimation");
            //_weapon = new ItemController("Sword", weaponTexture, scale, weaponPosition, contentManager);
            Texture2D bowTexture = contentManager.Load<Texture2D>("BowAnimation");
            Texture2D swordTexture = contentManager.Load<Texture2D>("SwordAnimation");
            Texture2D shieldTexture = contentManager.Load<Texture2D>("SwordAnimation");
            _sword = new ItemController("Sword", swordTexture, scale, weaponPosition, contentManager);
            _hotBar.Add(_sword);
            _currentHotBarIndex = 0;
        }
        //Core Methods
        public void LoadContent()
        {
            SpriteAnimation = _contentManager.Load<Texture2D>("PlayerAnimation");
            WalkAnimationManager = new(2, 2, new Vector2(32, 32), 1);
            _heldItem = _hotBar[_currentHotBarIndex];
        }
        public virtual void Update(int roomHeight, int roomLength)
        {
            _roomHeight = roomHeight;
            _roomLength = roomLength;
            Collision = new Rectangle((int)(RoomPosition.X + 0.5f), (int)(RoomPosition.Y + 0.5f), 1, 1);
            _heldItem.Update(RoomPosition, _isAttacking, MouseDirection);
        }
        public new ItemController Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, WalkAnimationManager.GetFrame(), Color.White);    
            return _heldItem;
        }
        //Other Methods
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
            RoomPosition += moveVelocity;
        }
    }
}
