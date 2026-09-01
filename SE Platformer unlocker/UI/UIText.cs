using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SE_Platformer_unlocker.UI
{
    internal class UIText : IUiElement
    {
        private string text;
        private SpriteFont font;
        
        public List<IUiElement> Children { get; set; }
        public IUiElement Parent { get; set; }
        public string Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public UIText(string text, SpriteFont font)
        {
            this.text = text;
            this.font = font;
        }

       

        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(font, text, Vector2.Zero, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 1);
        }
    }
}
