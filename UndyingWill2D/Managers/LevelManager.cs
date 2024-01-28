using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndyingWill2D.Controllers;

namespace UndyingWill2D.Managers
{
    internal class LevelManager
    {
        private List<EntityController> _objects;

        public LevelManager() { }

        public void Initialise()
        {
            PlayerController _player = new PlayerController();
        }
        public void Update() 
        { 
            for (int item = 0; item < _objects.Count; item++) 
            {
                EntityController currentObject = _objects[item];
                currentObject.Update();
            }
        }
    }
}
