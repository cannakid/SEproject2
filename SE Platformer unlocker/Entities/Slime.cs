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
    internal class Slime : Creature
    {
        private Vector2 speed = new Vector2(2f, 0);
        private Rectangle PrevPos;

        public Slime(Sprite sprite, Point pos, Point size, LevelScene scene, int health) : base(sprite, pos, size, scene, health)
        {
            PrevPos = HitBox;
        }

        public override void Update(GameTime gameTime)
        {
            
            if (!Alive)
            {
                return;
            }
            base.Update(gameTime);
            hitBox.Offset(speed);

            foreach (IInteractable interactable in scene.Interactables)
            {
                if (!interactable.Equals(this))
                {
                    InteractionDirection direction = hitBox.CollisionDirection(interactable.HitBox, PrevPos);
                    if (direction != InteractionDirection.NONE)
                    {
                        InteractionType result = interactable.Interact(direction);
                        HandleResult(result, direction, interactable);
                    }
                }
            }
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
            PrevPos = hitBox;
        }

        public override InteractionType Interact(InteractionDirection direction)
        {
            if (direction != InteractionDirection.TOP)
            {
                return InteractionType.HIT;
            }
            return InteractionType.NONE;
        }

        private void HandleResult(InteractionType type, InteractionDirection direction, IInteractable interactable)
        {
            if (type == InteractionType.NONE)
            {
                return;
            }
            if (type == InteractionType.BLOCK)
            {
                if (direction == InteractionDirection.LEFT)
                {
                    speed.X = -speed.X;
                    hitBox.X = interactable.HitBox.Left - HitBox.Width;
                }
                else if (direction == InteractionDirection.RIGHT)
                {
                    speed.X = -speed.X;
                    hitBox.X = interactable.HitBox.Right;
                }
            }
            else if (type == InteractionType.HIT)
            {
                    Health -= 1;
            }
            else if (type == InteractionType.PUSH)
            {
                // not yet implemented
            }
        }
    }
}
