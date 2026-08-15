

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.Entities;

namespace SE_Platformer_unlocker.Blocks
{
    internal class Block : IGameObject, IVisible, IInteractable
    {
        public Block(Texture2D texture, Point pos, Point size)
        {
            //this.textureFile = textureFile;
            Texture = texture;
            TextureRect = new Rectangle(pos, size);
        }
        //private string textureFile;

        public Texture2D Texture { get ; set; }

        public Rectangle TextureRect { get; protected set; }

        public Rectangle HitBox
        {
            get
            {
                return TextureRect;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if (Texture != null)
            {
                batch.Draw(Texture, TextureRect, Color.White);
            }
        }

        public void Interact(IInteractable interactable)
        {
            
        }
    }
}
