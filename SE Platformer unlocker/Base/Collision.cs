

using Microsoft.Xna.Framework;

namespace SE_Platformer_unlocker.Base
{
    public static class Collision
    {
        public static InteractionDirection CollisionDirection(this Rectangle a, Rectangle b, Rectangle previous)
        {
            if (!a.Intersects(b))
            {
                return InteractionDirection.NONE;
            }
            if (previous.Bottom <= b.Top)
            {
                return InteractionDirection.TOP;
            }
            else if (previous.Top >= b.Bottom)
            {
                return InteractionDirection.BOTTOM;
            }
            else if (previous.Right <= b.Left)
            {
                return InteractionDirection.LEFT;
            }
            else if (previous.Left >= b.Right)
            {
                return InteractionDirection.RIGHT;
            }
            return InteractionDirection.NONE;
        }
    }
}
