using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SE_Platformer_unlocker.UI
{
    internal class UIText : IUiElement
    {
        private string text;
        private Rectangle button;
        private SpriteFont font;

        public UIText(string text, Rectangle button, SpriteFont font)
        {
            this.text = text;
            this.button = button;
            this.font = font;
        }
        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(font, text, button.Location.ToVector2(), Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 1);
        }
    }
}
