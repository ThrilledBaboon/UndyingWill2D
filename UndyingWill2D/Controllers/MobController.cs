using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UndyingWill2D.Managers;

namespace UndyingWill2D.Controllers
{
    public class MobController : EntityController
    {
          MobController(Texture2D texture, int scale, Vector2 positions, ContentManager contentManager) : base(texture, scale, positions, contentManager) { }

         
    }
}
