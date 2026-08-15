using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Collections
{
    internal class UIScreen : IScreen<IUiElement>
    {
        public List<IUiElement> Objects { get; set; }
        public bool Loaded { get; set; }

        public void Draw(SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        public void Load(Game game)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
