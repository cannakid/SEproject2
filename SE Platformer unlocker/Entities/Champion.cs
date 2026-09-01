using Library;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Entities
{
    internal class Champion : Entity
    {
        private Vector2 speed;
        private bool isGrounded;
        private bool remainGrounded = false;

        private SoundEffect jumpSound;

        public Rectangle NextPos;

        public Champion(Sprite sprite, Point pos, Point size, LevelScene scene, SoundEffect jump) : base(sprite, new Rectangle(pos, size), scene)
        {
            NextPos = hitBox;
            jumpSound = jump;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                speed.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                speed.X -= 1;
            }
            speed.X = speed.X * 0.9f;
            if (Math.Abs(speed.X) < 0.1f)
            {
                speed.X = 0;
            }
            if (!isGrounded)
            {
                speed.Y += 0.5f;
            }
            if (isGrounded && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Core.Audio.PlaySoundEffect(jumpSound);
                speed.Y -= 12;
                isGrounded = false;
            }
            NextPos.Offset(speed);
            // interact
            remainGrounded = false;
            
            foreach (IInteractable interactable in scene.Interactables)
            {
                if (!interactable.Equals(this))
                {
                    InteractionDirection direction = NextPos.CollisionDirection(interactable.HitBox, HitBox);
                    if (direction != InteractionDirection.NONE)
                    {
                        InteractionType result = interactable.Interact(direction);
                        HandleResult(result, direction, interactable);
                    }
                }
                
            }
            isGrounded = remainGrounded;
            hitBox = NextPos;
            
        }

        public override InteractionType Interact(InteractionDirection direction)
        {
            if (direction == InteractionDirection.BOTTOM)
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
                if (direction == InteractionDirection.TOP)
                {
                    isGrounded = true;
                    remainGrounded = true;
                    speed.Y = 0;
                    NextPos.Y = interactable.HitBox.Top - HitBox.Height;
                }
                else if (direction == InteractionDirection.BOTTOM)
                {
                    speed.Y = 0;
                    NextPos.Y = interactable.HitBox.Bottom;
                }
                else if (direction == InteractionDirection.LEFT)
                {
                    speed.X = 0;
                    NextPos.X = interactable.HitBox.Left - HitBox.Width;
                }
                else if (direction == InteractionDirection.RIGHT)
                {
                    speed.X = 0;
                    NextPos.X = interactable.HitBox.Right;
                }
            }
            else if (type == InteractionType.HIT)
            {
                if (interactable is Creature c)
                {
                    c.Health -= 1;
                }
            }
            else if (type == InteractionType.PUSH)
            {
                // not yet implemented
            }
        }
    }
}
