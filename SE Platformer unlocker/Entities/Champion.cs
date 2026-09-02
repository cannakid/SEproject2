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
    internal class Champion : Creature
    {
        private Vector2 speed;
        private bool isGrounded;
        private bool remainGrounded = false;

        private SoundEffect jumpSound;
        private SoundEffect hurtSound;

        public Rectangle PrevPos;

        private TimeSpan invincibility = TimeSpan.Zero;
        private const int invincibilityDuration = 3;

        public Champion(Sprite sprite, Point pos, Point size, LevelScene scene, SoundEffect jump, SoundEffect hurt, int health) : base(sprite, pos, size, scene, health)
        {
            PrevPos = hitBox;
            jumpSound = jump;
            hurtSound = hurt;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!Alive)
            {
                Core.ChangeScene(new GameOverScene());
            }

            invincibility -= gameTime.ElapsedGameTime;
            if (invincibility < TimeSpan.Zero) invincibility = TimeSpan.Zero;

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
                speed.Y -= 15;
                isGrounded = false;
            }
            hitBox.Offset(speed);
            // interact
            remainGrounded = false;
            
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
            }
            else if (hitBox.X > Core.WIDTH - hitBox.Width)
            {
                hitBox.X = Core.WIDTH - hitBox.Width;
            }
            isGrounded = remainGrounded;
            PrevPos = hitBox;
            
        }

        public override InteractionType Interact(InteractionDirection direction)
        {
            if (direction == InteractionDirection.TOP)
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
                    hitBox.Y = interactable.HitBox.Top - HitBox.Height;
                }
                else if (direction == InteractionDirection.BOTTOM)
                {
                    speed.Y = 0;
                    hitBox.Y = interactable.HitBox.Bottom;
                }
                else if (direction == InteractionDirection.LEFT)
                {
                    speed.X = 0;
                    hitBox.X = interactable.HitBox.Left - HitBox.Width;
                }
                else if (direction == InteractionDirection.RIGHT)
                {
                    speed.X = 0;
                    hitBox.X = interactable.HitBox.Right;
                }
            }
            else if (type == InteractionType.HIT)
            {
                if (invincibility == TimeSpan.Zero)
                {
                    Core.Audio.PlaySoundEffect(hurtSound);
                    Health -= 1;
                    invincibility = invincibility.Add(TimeSpan.FromSeconds(3));
                }
            }
            else if (type == InteractionType.PUSH)
            {
                // not yet implemented
            }
            else if (type == InteractionType.VICTORY)
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
        }
    }
}
