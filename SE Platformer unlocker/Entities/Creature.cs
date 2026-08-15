using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;


namespace SE_Platformer_unlocker.Entities
{
    internal abstract class Creature : IGameObject, IDynamic, IInteractable, IVisible
    {
        public Rectangle HitBox { get => hitBox; }
        protected Rectangle hitBox;

        public Texture2D Texture { get; set; }

        public Rectangle TextureRect { get => textureRect; }
        protected Rectangle textureRect;

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Texture, textureRect, Color.White);
        }

        public abstract void Interact(IInteractable interactable);
        

        public abstract void Update();
        
    }
}
