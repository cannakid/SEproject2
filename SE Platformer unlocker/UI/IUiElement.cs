using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.UI
{
    public interface IUiElement
    {
        //string Text { get; set; }
        
        //List<IUiElement> Children { get; set; }

        //IUiElement Parent { get; set; }


        void Draw(SpriteBatch spriteBatch);
    }
}
