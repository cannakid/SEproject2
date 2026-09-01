using Microsoft.Xna.Framework;

namespace SE_Platformer_unlocker.Base
{
    internal interface IInteractable : IGameObject
    {
        public Rectangle HitBox { get; }
        public void Interact(IInteractable interactable);
    }
}
