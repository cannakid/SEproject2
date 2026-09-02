using Library.Graphics;
using Microsoft.Xna.Framework;
using SE_Platformer_unlocker.Scenes;
using System.Collections.Generic;


namespace SE_Platformer_unlocker.Entities
{
    internal abstract class Creature : Entity
    {
        protected Creature(Sprite sprite, Point pos, Point size, LevelScene scene, int health) : base(sprite, new Rectangle(pos, size), scene)
        {
            Health = health;
        }

        protected Creature(List<Sprite> sprites, Point pos, Point size, LevelScene scene, int health) : base(sprites, new Rectangle(pos, size), scene)
        {
            Health = health;
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

        public virtual void TakeDamage(int amount)
        {
            Health -= amount;
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Alive)
            {
                return;
            }
            base.Draw(gameTime);
        }
    }
}
