using Library.Graphics;
using Microsoft.Xna.Framework;


namespace SE_Platformer_unlocker.Entities
{
    internal abstract class Creature : Entity
    {
        protected Creature(Sprite sprite, Point pos, Point size) : base(sprite, new Rectangle(pos, size))
        {

        }

        private int health;

    }
}
