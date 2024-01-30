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
        new float _moveSpeed = 2.5f;
        float _dashSpeed = 100f;
        float _dashColldown = 150f;
        float _timeSinceLastDash = 0;
        //Property
        public int Stamina { get; set; }
        public PlayerController(Texture2D texture, int scale, Vector2 positions, ContentManager contentManager) : base(texture, scale, positions, contentManager)
        { }
        public override void Update()
        {
            HandleInput();
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
    }
}
