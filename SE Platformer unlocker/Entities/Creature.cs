using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;


namespace SE_Platformer_unlocker.Entities
{
    internal abstract class Creature : AnimatedSprite, IInteractable
    {
        public Rectangle HitBox { get => hitBox; }
        protected Rectangle hitBox;

        public Rectangle TextureRect { get => textureRect; }
        protected Rectangle textureRect;

        

        public abstract void Interact(IInteractable interactable);
        

        public abstract void Update();
        
    }
}
