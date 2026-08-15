using Microsoft.Xna.Framework;

namespace SE_Platformer_unlocker.Base
{
    internal interface IInteractable
    {
        public Rectangle HitBox { get; }
        public void Interact(IInteractable interactable);
    }
}
