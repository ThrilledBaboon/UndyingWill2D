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
    public class EntityController
    {
        //Fields
        private protected int _health;
        private protected bool _isAlive;
        private protected Vector2 _moveDirection;
        private protected int _moveSpeed;

        //Property
        public int Health { get { return _health; } set { _health = value; } }
        public bool IsAlive {get {return _isAlive;} set { _isAlive = value;} }
        
        //Constructor
        public EntityController() { }
        //Methods
        public void Update()
        {
            HandleInput();
            Draw();
        }
        public void Draw()
        {
            if (_isAlive)
            {

            }
        }

        public virtual void HandleInput() 
        {

        }
        public void OnMove(Vector2 moveDirection)
        {

        }
        public void OnAttack(MouseState mouse)
        {

        }
        public void OnStun()
        {

        }


    }
}
