using Library.Graphics;
using Microsoft.Xna.Framework;
using SE_Platformer_unlocker.Scenes;


namespace SE_Platformer_unlocker.Entities
{
    internal abstract class Creature : Entity
    {
        protected Creature(Sprite sprite, Point pos, Point size, LevelScene scene) : base(sprite, new Rectangle(pos, size), scene)
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
