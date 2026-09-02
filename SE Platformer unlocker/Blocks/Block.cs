

using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.Entities;

namespace SE_Platformer_unlocker.Blocks
{
    public abstract class Block : IInteractable
    {
        public Block(int x, int y, int width, int height)
        {
            HitBox = new Rectangle(x, y, width, height);
        }

        public Rectangle HitBox { get; private set; }

        public abstract InteractionType Interact(InteractionDirection direction);
    }
}
