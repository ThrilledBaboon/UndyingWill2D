using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UndyingWill2D.Controllers
{
    internal class EntityController
    {
        //Fields
        private int _health;
        private bool _isAlive;
        private Vector2 _moveDirection;
        private int _moveSpeed;

        //Property
        public int Health { get { return _health; } set { _health = value; } }
        public bool IsAlive {get {return _isAlive;} set { _isAlive = value;} }
        
        //Constructor
        public EntityController() { }
        //Methods
        public void Update()
        {
            HandleInput();
        }
        public void Draw()
        {
            if (_isAlive)
            {

            }
        }
        private void HandleInput() 
        {

        }
        private void OnMove(Vector2 moveDirection)
        {

        }
        private void OnAttack(MouseState mouse)
        {

        }
        private void OnRoll(KeyboardState keyboardState)
        {

        }
        private void OnBlock()
        {

        }


    }
}
