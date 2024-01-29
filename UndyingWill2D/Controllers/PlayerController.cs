using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private int _stamina;
        //Property
        public int Stamina { get { return _stamina; } set { _stamina = value; } }
        public PlayerController(Texture2D texture, Vector2 positions, ContentManager contentManager) : base(texture, positions, contentManager)
        { }

        public override void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            //WASD
            if (keyboardState.IsKeyDown(Keys.W))
            {
                Debug.WriteLine("W Pressed");
                OnMove(_moveDirection);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Debug.WriteLine("A Pressed");
                OnMove(_moveDirection);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                Debug.WriteLine("S Pressed");
                OnMove(_moveDirection);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                Debug.WriteLine("D Pressed");
                OnMove(_moveDirection);
            }
            //Mouse
            if (mouseState.LeftButton == ButtonState.Pressed) 
            {
                Debug.WriteLine("Left Click Pressed");
                OnAttack(mouseState); 
            }
            //Other Actions
            if (keyboardState.IsKeyDown(Keys.R)) 
            {
                Debug.WriteLine("R Pressed");
                OnRoll(keyboardState); 
            }
            if (keyboardState.IsKeyDown(Keys.LeftShift)) 
            {
                Debug.WriteLine("Left Shift Pressed");
                OnBlock(keyboardState, mouseState); 
            }
        }

        public void OnRoll(KeyboardState keyboardState)
        {

        }

        public void OnBlock(KeyboardState keyboardState, MouseState mouseState)
        {

        }
    }
}
