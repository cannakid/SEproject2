
using Library;
using Microsoft.Xna.Framework;
using SE_Platformer_unlocker.Entities;
using SE_Platformer_unlocker.Scenes;
using System.Collections.Generic;


namespace SE_Platformer_unlocker.Collision
{
    public class CollisionHandler
    {
        public CollisionHandler(LevelScene scene)
        {
            this.scene = scene;
            interactables = new List<IInteractable>();
            entities = new List<Entity>();
        }


        private LevelScene scene;

        private List<IInteractable> interactables;

        private List<Entity> entities;

        public void HandleCollisions()
        {
            // check for dead creatures
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                if (entities[i] is Creature c)
                {
                    if (!c.Alive)
                    {
                        interactables.Remove(entities[i]);
                        entities.RemoveAt(i);
                        
                    }
                }
            }


            foreach (Entity e in entities)
            {
                List<IInteractable> possibleHits = new List<IInteractable>();
                foreach (IInteractable interactable in interactables)
                {
                    if (e.Equals(interactable)) continue; // no collisions with yourself
                    InteractionDirection direction = e.NextPos.CollisionDirection(interactable.HitBox, e.HitBox);
                    if (interactable is Entity && direction == InteractionDirection.WITHIN)
                    {
                        InteractionDirection otherDirection = (interactable as Entity).NextPos.CollisionDirection(interactable.HitBox, e.HitBox);
                        if (otherDirection != InteractionDirection.WITHIN)
                        {
                            direction = e.NextPos.CollisionDirection((interactable as Entity).NextPos, e.HitBox);
                        }
                    }
                    if (direction != InteractionDirection.NONE)
                    {
                        InteractionType interaction = interactable.Interact(direction);
                        switch (interaction)
                        {
                            case InteractionType.BLOCK:
                                ApplyBlock(e, direction, interactable.HitBox);
                                break;
                            case InteractionType.HIT:
                                if (e is Creature)
                                {
                                    possibleHits.Add(interactable);
                                }
                                break;
                            case InteractionType.VICTORY:
                                if (e is Champion)
                                {
                                    if (scene is Level1)
                                    {
                                        Core.ChangeScene(new VictoryScene(new Level2()));
                                    }
                                    else
                                    {
                                        Core.ChangeScene(new VictoryScene());
                                    }
                                }
                                break;
                            case InteractionType.PUSH:
                                // not implemented
                                break;
                            default:
                                break;
                        }
                    }
                }
                foreach (IInteractable interactable in possibleHits)
                {
                    InteractionDirection direction = e.NextPos.CollisionDirection(interactable.HitBox, e.HitBox);
                    if (direction != InteractionDirection.NONE)
                    {
                        InteractionType interaction = interactable.Interact(direction);
                        if (interaction == InteractionType.HIT)
                        {
                            (e as Creature).TakeDamage(1);
                            break;
                        }
                    }
                }
            }






            foreach (Entity e in entities)
            {
                e.UpdatePosition();
            }
        }

        public void Add(IInteractable interactable)
        {
            interactables.Add(interactable);
            if (interactable is Entity e)
            {
                entities.Add(e);
            }
        }

        private void ApplyBlock(Entity e, InteractionDirection direction, Rectangle blocker)
        {
            if (direction == InteractionDirection.TOP)
            {
                if (e is Champion c)
                {
                    c.IsGrounded = true;
                    c.RemainGrounded = true;
                    c.spriteIndex = 4;
                }
                
                e.speed.Y = 0;
                e.NextPos.Y = blocker.Top - e.NextPos.Height;
            }
            else if (direction == InteractionDirection.BOTTOM)
            {
                e.speed.Y = 0;
                e.NextPos.Y = blocker.Bottom;
            }
            else if (direction == InteractionDirection.LEFT)
            {
                e.speed.X = 0;
                e.NextPos.X = blocker.Left - e.NextPos.Width;
            }
            else if (direction == InteractionDirection.RIGHT)
            {
                e.speed.X = 0;
                e.NextPos.X = blocker.Right;
            }
        }
    }
}
