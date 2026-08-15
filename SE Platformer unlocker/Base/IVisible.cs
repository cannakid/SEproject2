using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SE_Platformer_unlocker.Base
{
    internal interface IVisible
    {
        public Texture2D Texture { get; set; }
        public Rectangle TextureRect { get; }
        public void Draw(SpriteBatch batch);
    }
}
