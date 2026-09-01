using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Library.Graphics
{
    public class Text
    {
        public Text(SpriteFont font, string text)
        {
            Font = font;
            InnerText = text;
        }

        public SpriteFont Font { get; private set; }

        public string InnerText { get; set; }


        public float Rotation { get; set; } = 0.0f;

        public Vector2 Scale { get; set; } = Vector2.One;

        public Vector2 Origin { get; set; } = Vector2.Zero;

        public Color Color { get; set; } = Color.White;

        public float LayerDepth { get; set; } = 0f;

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.DrawString(Font, InnerText, position, Color, Rotation, Origin, Scale, SpriteEffects.None, LayerDepth);
        }
    }
}
