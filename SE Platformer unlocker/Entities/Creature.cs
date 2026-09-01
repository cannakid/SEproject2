using Library.Graphics;
using Microsoft.Xna.Framework;


namespace SE_Platformer_unlocker.Entities
{
    internal abstract class Creature : Entity
    {
        protected Creature(Sprite sprite, Point pos, Point size) : base(sprite, new Rectangle(pos, size))
        {

        }

        public bool Alive => health > 0;
        private int health;
        public int Health
        {
            get => health;
            set
            {
                if (value <= 0)
                {
                    health = 0;
                }
                health = value;
            }
        }

    }
}
