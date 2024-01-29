using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        public Texture2D texture;
        public  Vector2 position;
        public ContentManager contentManager;

        //Fields
        private protected int _health;
        private protected bool _isAlive;
        private protected Vector2 _moveDirection;
        private protected int _moveSpeed;

        //Property
        public int Health { get { return _health; } set { _health = value; } }
        public bool IsAlive {get {return _isAlive;} set { _isAlive = value;} }
        public Rectangle Rectangle{get { return new Rectangle((int)position.X, (int)position.Y, 75, 75); }}
        
        //Constructor
        public EntityController(Texture2D texture, Vector2 position, ContentManager contentManager) 
        { 
            this.texture = texture;
            this.position = position;
            this.contentManager = contentManager;
        }
        //Methods
        public void Update()
        {
            HandleInput();
            //Draw();
        }

        public void LoadContent()
        {
            texture = contentManager.Load<Texture2D>("PlayerAnimation");
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
            position += moveDirection;
        }
        public void OnAttack(MouseState mouse)
        {

        }
        public void OnStun()
        {

        }


    }
}
