

using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.Entities;

namespace SE_Platformer_unlocker.Blocks
{
    public class Block : IInteractable
    {
        public Block(int x, int y, int width, int height)
        {
            HitBox = new Rectangle(x, y, width, height);
        }

        public Rectangle HitBox { get; private set; }

        public Interactions Interact(IInteractable interactable)
        {
            if (interactable.HitBox.Intersects(HitBox))
            {
                return Interactions.BLOCK;
            }
            return Interactions.NONE;
        }
    }
}
