using Library;
using Library.Graphics;
using Library.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SE_Platformer_unlocker.UI
{
    public class UIIcon : IUiElement
    {
        public UIIcon(Sprite sprite, Rectangle rect, Action action)
        {
            Active = true;
            this.image = sprite;
            this.position = rect.Location.ToVector2();
            this.button = rect;
            this.action = action;
            sprite.Scale = rect.Size.ToVector2() / sprite.Region.SourceRectangle.Size.ToVector2();
        }


        private Sprite image;
        private Rectangle button;
        private Vector2 position;

        private Action action;

        public bool Active { get; set; }

        public void CenterIcon()
        {
            position -= new Vector2(button.Width / 2, button.Height / 2);
            button.Offset(-button.Width / 2, -button.Height / 2);
            image.Origin = button.Size.ToVector2() / 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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

        public void ChangeIcon(Sprite newIcon)
        {
            image = newIcon;
            image.Scale = button.Size.ToVector2() / image.Region.SourceRectangle.Size.ToVector2();
        }
    }
}
