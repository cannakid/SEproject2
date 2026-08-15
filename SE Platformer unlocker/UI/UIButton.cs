using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SE_Platformer_unlocker.UI
{
    internal class UIButton : IUiElement
    {
        private string text;
        private Rectangle button;
        private SpriteFont font;

        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(font, text, button.Location.ToVector2(), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
        }

        public void Update()
        {
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed)
            {

            }
        }
    }
}
