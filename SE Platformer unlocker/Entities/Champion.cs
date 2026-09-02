using Library;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SE_Platformer_unlocker.Collision;
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
        public bool IsGrounded { get; set; } = false;
        public bool RemainGrounded { get; set; } = false;

        private SoundEffect jumpSound;
        //private SoundEffect hurtSound;

        private TimeSpan invincibility = TimeSpan.Zero;
        private const int invincibilityDuration = 3;

        private TimeSpan jumpTimeSpan = TimeSpan.Zero;

        public Champion(List<Sprite> sprites, Point pos, Point size, LevelScene scene, SoundEffect jump, SoundEffect hurt, int health) : base(sprites, pos, size, scene, health)
        {
            jumpSound = jump;
            //hurtSound = hurt;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!Alive)
            {
                if (scene is Level1)
                {
                    Core.ChangeScene(new GameOverScene(new Level1()));
                }
                else if (scene is Level2)
                {
                    Core.ChangeScene(new GameOverScene(new Level2()));
                }
            }

            invincibility -= gameTime.ElapsedGameTime;
            if (invincibility < TimeSpan.Zero) invincibility = TimeSpan.Zero;

            jumpTimeSpan -= gameTime.ElapsedGameTime;
            if (jumpTimeSpan < TimeSpan.Zero) jumpTimeSpan = TimeSpan.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                speed.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                speed.X -= 1;
            }
            speed.X = speed.X * 0.9f; // friction

            if (Math.Abs(speed.X) < 0.1f)
            {
                speed.X = 0;
            }
            if (!IsGrounded)
            {
                speed.Y += 0.5f;
                if (jumpTimeSpan == TimeSpan.Zero)
                {
                    spriteIndex = 3;
                }
            }
            if (IsGrounded && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Core.Audio.PlaySoundEffect(jumpSound);
                spriteIndex = 2;
                jumpTimeSpan.Add(TimeSpan.FromSeconds(1));
                speed.Y -= 15;
                IsGrounded = false;
            }
            RemainGrounded = false;

            if (speed.X > 0 && spriteIndex == 0)
            {
                spriteIndex = 1; // running
            }
            else if (speed.X == 0)
            {
                spriteIndex = 0;
            }
        }

        public override void TakeDamage(int amount)
        {
            if (invincibility != TimeSpan.Zero)
            {
                return;
            }
            invincibility = invincibility.Add(TimeSpan.FromSeconds(invincibilityDuration));
            base.TakeDamage(amount);
        }

        public override void UpdatePosition()
        {
            base.UpdatePosition();
            IsGrounded = RemainGrounded;
        }

        public override InteractionType Interact(InteractionDirection direction)
        {
            if (direction == InteractionDirection.BOTTOM)
            {
                return InteractionType.HIT;
            }
            return InteractionType.NONE;
        }
    }
}
