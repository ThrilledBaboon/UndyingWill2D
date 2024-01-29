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

namespace UndyingWill2D.Controllers
{
    public class PlayerController : EntityController
    {
        //Fields
        new float _moveSpeed = 2.5f;
        int _stamina;
        bool _isRPressed;
        //Property
        public int Stamina { get { return _stamina; } set { _stamina = value; } }
        public PlayerController(Texture2D texture, int scale, Vector2 positions, ContentManager contentManager) : base(texture, scale, positions, contentManager)
        { }

        public override void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            _moveDirection = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W))
            {
                _moveDirection.Y = -1;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _moveDirection.X = -1;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                _moveDirection.Y = 1;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                _moveDirection.X = 1;
            }
            if (mouseState.LeftButton == ButtonState.Pressed) 
            {
                OnAttack(mouseState); 
            }
            if (!_isRPressed && keyboardState.IsKeyDown(Keys.R)) 
            {
                _isRPressed = true;
            }
            if (keyboardState.IsKeyDown(Keys.R))
            {
                _isRPressed = false;
                OnDash(_moveDirection);
            }
            if (keyboardState.IsKeyDown(Keys.LeftShift)) 
            {
                OnBlock(mouseState); 
            }
            OnMove(_moveDirection, _moveSpeed);
        }

        public void OnDash(Vector2 _moveDirection)
        {

        }

        public void OnBlock(MouseState mouseState)
        {

        }
    }
}
