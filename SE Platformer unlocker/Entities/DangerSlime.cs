using Library;
using Library.Graphics;
using Microsoft.Xna.Framework;
using SE_Platformer_unlocker.Collision;
using SE_Platformer_unlocker.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Entities
{
    internal class DangerSlime : Creature
    {

        public DangerSlime(Sprite sprite, Point pos, Point size, LevelScene scene, int health, Champion c) : base(sprite, pos, size, scene, health)
        {
            NextPos = HitBox;
            speed = new Vector2(2f, 0);
            champ = c;
        }

        private Champion champ;

        public override void Update(GameTime gameTime)
        {
            
            if (!Alive)
            {
                return;
            }
            base.Update(gameTime);
            hitBox.Offset(speed);

            if (hitBox.X < 0)
            {
                hitBox.X = 0;
                speed.X = -speed.X;
            }
            else if (hitBox.X > Core.WIDTH - hitBox.Width)
            {
                hitBox.X = Core.WIDTH - hitBox.Width;
                speed.X = -speed.X;
            }
            if (Vector2.Distance(champ.HitBox.Location.ToVector2(), HitBox.Location.ToVector2()) < 300) {
                if (champ.HitBox.X < HitBox.X)
                {
                    speed = new Vector2(-3f, 0);
                }
                else if (champ.HitBox.X > HitBox.X)
                {
                    speed = new Vector2(3f, 0);
                }
                else if (speed.X > 0)
                {
                    speed.X = 2f;
                }
                else
                {
                    speed.X = -2f;
                }
            }
        }

        public override InteractionType Interact(InteractionDirection direction)
        {
            if (direction != InteractionDirection.TOP && direction != InteractionDirection.WITHIN)
            {
                return InteractionType.HIT;
            }
            return InteractionType.NONE;
        }
    }
}
