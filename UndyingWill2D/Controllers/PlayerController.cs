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
        //Needs Refactoring wont meet requirements
        List<ItemController> _inventory = new List<ItemController>();
        List<ItemController> _hotBar = new List<ItemController>();

        //Fields
        new float _moveSpeed = 2.5f;
        float _dashSpeed = 100f;
        float _dashColldown = 150f;
        float _timeSinceLastDash = 0;
        int _currentHotBarSlot = 0;
        //Property
        public int Stamina { get; set; }
        public PlayerController(Texture2D texture, int scale, Vector2 positions, ContentManager contentManager) : base(texture, scale, positions, contentManager)
        { }
        public override void Update()
        {
            HandleInput();
            _weapon.Update(_position);
            _timeSinceLastDash++;
        }

        public override void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            _moveDirection = Vector2.Zero;
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
            if (mouseState.LeftButton == ButtonState.Pressed) 
            {
                OnAttack(mouseState.Position); 
            }
            if (_timeSinceLastDash >= _dashColldown && keyboardState.IsKeyDown(Keys.LeftShift)) 
            {
                _timeSinceLastDash = 0;
                OnDash(_moveDirection);
            }
            if (mouseState.RightButton == ButtonState.Pressed) 
            {
                OnBlock(mouseState.Position); 
            }
            if (keyboardState.IsKeyDown(Keys.D1))
            {
                ChangeHotBarItem(1);
            }
            if (keyboardState.IsKeyDown(Keys.D2))
            {
                ChangeHotBarItem(2);
            }
            if (keyboardState.IsKeyDown(Keys.D3))
            {
                ChangeHotBarItem(3);
            }
            if (keyboardState.IsKeyDown(Keys.F))
            {
                OnInteract();
            }
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                _moveDirection.X += 1;
            }
            OnMove(_moveDirection, _moveSpeed);
        }

        public override void OnMove(Vector2 moveDirection, float moveSpeed)
        {
            if (moveDirection == Vector2.Zero) { IsMoving = false; }
            else { IsMoving = true; }
            AnimationManager.Update(IsMoving);
            if (moveDirection != Vector2.Zero)
            {
                moveDirection.Normalize();
            }
            Vector2 moveVelocity = moveDirection * moveSpeed;
            _position += moveVelocity;
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
            _currentHotBarSlot = index;
        }
        public void OnInteract()
        {
            if (FindInteractablesInRange() == false) { return; }
            if (FindInteractablesInRange() == true)
            {
                object closestInteractable = ClosestInteractable();
                if (closestInteractable.GetType == GetType(ItemController))
                {
                    _hotBar.Add(closestInteractable);
                }
            }
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
            _hotBar.RemoveAt(_currentHotBarSlot - 1);
            // needs to use current hotbar slot and remove it from list
        }
    }
}
