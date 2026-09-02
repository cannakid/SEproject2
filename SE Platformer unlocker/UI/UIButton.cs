using Library;
using Library.Graphics;
using Library.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;
using System;

namespace SE_Platformer_unlocker.UI
{
    public class UIButton : IUiElement, IDynamic
    {
        public UIButton(Text text, Sprite sprite, Rectangle rect, Action action)
        {
            Active = true;
            this.text = text;
            this.image = sprite;
            image.LayerDepth = text.LayerDepth + 1;
            this.position = rect.Location.ToVector2();
            this.button = rect;
            this.action = action;
        }


        private Text text;
        private Sprite image;
        private Rectangle button;
        private Vector2 position;

        private Action action;

        public bool Active { get; set; }

        public void CenterButton()
        {
            text.CenterText();
            button.Offset(-button.Width / 2, -button.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            text.Draw(spriteBatch, position);
            image.Draw(spriteBatch, position);
        }

        public void Update(GameTime gameTime)
        {
            if (Core.Input.Mouse.WasButtonJustPressed(MouseButton.Left))
            {
                if (button.Contains(Core.Input.Mouse.Position))
                {
                    action.Invoke();
                }
            }
        }
    }
}
