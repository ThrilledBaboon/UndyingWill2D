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
    internal class PlayerController : EntityController
    {
        private int _stamina;
        public int Stamina { get { return _stamina; } set { _stamina = value; } }
    }
}
