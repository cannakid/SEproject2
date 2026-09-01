using Library;
using Library.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Base
{
    public class Drawable
    {
        private Sprite sprite;
        private Vector2 position;

        public void Draw()
        {
            sprite.Draw(Core.SpriteBatch, position);
        }
       
    }
}
