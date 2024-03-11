using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UndyingWill2D.Managers;

namespace UndyingWill2D.Controllers
{
    public class PlayerController : EntityController
    {
        //Fields
        List<ItemController> _inventory = new List<ItemController>();
        float _dashSpeed = 50f;
        private Vector2 _RoomArea { get; set; }
        //Counters
        float _timeSinceLastDash = 0f;
        float _timeSinceLastSwitchHotBar = 0f;
        float _timeSinceLastAttack = 0f;
        //Cooldowns
        float _dashColldown = 150f;
        float _switchHotBarColldown = 15f;
        float _attackColldown = 45f;
        //Property
        public int Stamina { get; set; }
        public Rectangle collisionRectangle
        {
            get
            {
                return new Microsoft.Xna.Framework.Rectangle((int)RoomPosition.X, (int)RoomPosition.Y, Scale, Scale);
            }
        }
        //Contructor
        public PlayerController(Texture2D texture, int scale, Vector2 roomPosition, ContentManager contentManager) : base(texture, scale, roomPosition, contentManager)
        { _moveSpeed = 1f; }
        //Core Methods
        public override void Update(int roomHeight, int roomLength)
        {
            _roomHeight = roomHeight;
            _roomLength = roomLength;
            HandleInput();
            Collision = new Rectangle((int)(RoomPosition.X + 0.5f), (int)(RoomPosition.Y + 0.5f), 1, 1);
            _heldItem = _hotBar[_currentHotBarIndex];
            _heldItem.Update(RoomPosition, _isAttacking);
            _timeSinceLastDash++;
            _timeSinceLastAttack++;
            _timeSinceLastSwitchHotBar++;
        }
        //Other Methods
        public void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            _moveDirection = Vector2.Zero;
            if (_previouslyAttacking)
            {
                _isAttacking = false;
                _previouslyAttacking = _isAttacking;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                _moveDirection.Y += -1;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _moveDirection.X += -1;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                _moveDirection.Y += 1;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                _moveDirection.X += 1;
            }
            if (_timeSinceLastAttack >= _attackColldown && mouseState.LeftButton == ButtonState.Pressed) 
            {
                _timeSinceLastAttack = 0f;
                _isAttacking = true;
                _previouslyAttacking = true;
                Debug.WriteLine(_isAttacking);
            }
            if (_timeSinceLastDash >= _dashColldown && keyboardState.IsKeyDown(Keys.LeftShift)) 
            {
                _timeSinceLastDash = 0f;
                OnDash(_moveDirection);
            }
            if (mouseState.RightButton == ButtonState.Pressed) 
            {
                OnBlock(mouseState.Position); 
            }
            if (_timeSinceLastSwitchHotBar >= _switchHotBarColldown)
            {
                if (keyboardState.IsKeyDown(Keys.D1))
                {
                    ChangeHotBarItem(0);
                }
            }
            if (keyboardState.IsKeyDown(Keys.F))
            {
                OnInteract();
            }
            OnMove(_moveDirection, _moveSpeed);
        }
        public override void OnMove(Vector2 moveDirection, float moveSpeed)
        {
            if (moveDirection == Vector2.Zero) { IsMoving = false; }
            else { IsMoving = true; }
            WalkAnimationManager.Update(IsMoving);
            if (moveDirection != Vector2.Zero)
            {
                moveDirection.Normalize();
            }
            Vector2 moveVelocity = moveDirection/14 * moveSpeed;
            Vector2 currentRoomPosition = RoomPosition;
            Vector2 roomArea = new Vector2(_roomLength, _roomHeight);
            Vector2 positionToMoveTo = currentRoomPosition + moveVelocity;
            bool isMovePossible = (0 <= positionToMoveTo.X &&
                positionToMoveTo.X <= roomArea.X) 
                && (0 <= positionToMoveTo.Y && 
                positionToMoveTo.Y <= roomArea.Y);
            if (isMovePossible)
            {
                RoomPosition += moveVelocity;
            }
        }
        public void OnDash(Vector2 _moveDirection)
        {
            OnMove(_moveDirection, _dashSpeed);
        }
        public override void OnBlock(Point point)
        {

        }
        public void ChangeHotBarItem(int index)
        {
            _heldItem = _hotBar[index];
        }
        public void OnInteract()
        {
            if (FindInteractablesInRange() == false) { return; }
            //if (FindInteractablesInRange() == true)
            //{
            //    object closestInteractable = ClosestInteractable();
            //    if (closestInteractable.GetType == GetType(ItemController))
            //    {
            //        _hotBar.Add(closestInteractable);
            //    }
            //}
            // this will need to do a lot:
            // - open doors
            // - pick up items on the floor
            // - open shop

            // will likely need to check closest interactable
            // sounds like i will need an interactable class as well then 
        }
        private bool FindInteractablesInRange()
        {
            throw new NotImplementedException();
        }
        private object ClosestInteractable()
        {
            throw new NotImplementedException();
        }
        public void OnDrop()
        {
            _hotBar.RemoveAt(_currentHotBarIndex);
            // needs to use current hotbar slot and remove it from list
        }
    }
}
