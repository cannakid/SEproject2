using Library;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.UI
{
    public class UIText : IUiElement
    {
        public UIText(Text text, Vector2 position)
        {
            Active = true;
            Text = text;
            Position = position;
        }


        public bool Active { get; set; }

        public Text Text { get; private set; }
        public Vector2 Position { get; private set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            Text.Draw(Core.SpriteBatch, Position);
        }
    }
}
