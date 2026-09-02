using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.UI
{
    public interface IUiElement : IGameObject
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
