using Microsoft.Xna.Framework;

namespace SE_Platformer_unlocker.Base
{
    public interface IInteractable
    {
        public Rectangle HitBox { get; }
        public InteractionType Interact(InteractionDirection direction);
    }
}
