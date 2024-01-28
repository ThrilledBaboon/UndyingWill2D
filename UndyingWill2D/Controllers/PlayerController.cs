using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UndyingWill2D.Controllers
{
    public class PlayerController : EntityController
    {
        //Fields
        private int _stamina;
        //Property
        public int Stamina { get { return _stamina; } set { _stamina = value; } }
        public PlayerController() { }

        public override void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            //WASD
            if (keyboardState.IsKeyDown(Keys.W))
            {
                OnMove(_moveDirection);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                OnMove(_moveDirection);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                OnMove(_moveDirection);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                OnMove(_moveDirection);
            }
            //Mouse
            if (mouseState.LeftButton == ButtonState.Pressed) { OnAttack(mouseState); }
            //Other Actions
            if (keyboardState.IsKeyDown(Keys.R)) { OnRoll(keyboardState); }
            if (keyboardState.IsKeyDown(Keys.LeftShift)) { OnBlock(keyboardState, mouseState); }
        }

        public void OnRoll(KeyboardState keyboardState)
        {

        }

        public void OnBlock(KeyboardState keyboardState, MouseState mouseState)
        {

        }
    }
}
