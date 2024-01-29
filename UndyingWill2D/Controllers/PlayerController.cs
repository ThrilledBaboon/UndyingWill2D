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
        private new float _moveSpeed = 2.5f;
        private int _stamina;
        private bool _isRPressed;
        //Property
        public int Stamina { get { return _stamina; } set { _stamina = value; } }
        public PlayerController(Texture2D texture, Vector2 positions, ContentManager contentManager) : base(texture, positions, contentManager)
        { }

        public override void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            _moveDirection = new Vector2(0, 0);
            //WASD
            if (keyboardState.IsKeyDown(Keys.W))
            {
                _moveDirection.Y = -_moveSpeed;
                OnMove(_moveDirection);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _moveDirection.X = -_moveSpeed;
                OnMove(_moveDirection);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                _moveDirection.Y = _moveSpeed;
                OnMove(_moveDirection);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                _moveDirection.X = _moveSpeed;
                OnMove(_moveDirection);
            }
            //Mouse
            if (mouseState.LeftButton == ButtonState.Pressed) 
            {
                OnAttack(mouseState); 
            }
            //Other Actions
            if (!_isRPressed && keyboardState.IsKeyDown(Keys.R)) 
            {
                _isRPressed = true;
                OnRoll(_moveDirection); 
            }
            if (keyboardState.IsKeyDown(Keys.R))
            {
                _isRPressed = false;
                OnRoll(_moveDirection);
            }
            if (keyboardState.IsKeyDown(Keys.LeftShift)) 
            {
                OnBlock(mouseState); 
            }
        }

        public void OnRoll(Vector2 _moveDirection)
        {

        }

        public void OnBlock(MouseState mouseState)
        {

        }
    }
}
