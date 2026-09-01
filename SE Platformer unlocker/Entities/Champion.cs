using Library;
using Library.Graphics;
using Microsoft.Xna.Framework;
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

        private LevelScene scene;

        public Rectangle NextPos;

        public Champion(Sprite sprite, Point pos, Point size, LevelScene scene) : base(sprite, new Rectangle(pos, size))
        {
            NextPos = hitBox;
            this.scene = scene;
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
            if (HitBox.Top > Game1.HEIGHT)
            {
                Game1.Instance.Exit(); // replace with game over screen
            }
            if (isGrounded && Keyboard.GetState().IsKeyDown(Keys.W))
            {
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
                    Interactions result = interactable.Interact(this);
                    HandleResult(result);
                }
                
            }
            isGrounded = remainGrounded;
            hitBox = NextPos;
            
        }

        public override Interactions Interact(IInteractable interactable)
        {
            if (interactable.HitBox.Top == HitBox.Bottom + 1)
            {
                remainGrounded = true;
            }
            if (!interactable.HitBox.Intersects(NextPos))
            {
                return Interactions.NONE;
            }
            if (HitBox.Bottom <= interactable.HitBox.Top)
            {
                speed.Y = 0;
                isGrounded = true;
                remainGrounded = true;
                NextPos.Y = interactable.HitBox.Top - HitBox.Size.Y; // Y is top side
            }
            else if (HitBox.Top >= interactable.HitBox.Bottom)
            {
                speed.Y = 0;
                NextPos.Y = interactable.HitBox.Bottom;
            }
            else if (HitBox.Right <= interactable.HitBox.Left)
            {
                speed.X = 0;
                NextPos.X = interactable.HitBox.Left - HitBox.Size.X; // X is left side
            }
            else if (HitBox.Left >= interactable.HitBox.Right)
            {
                speed.X = 0;
                NextPos.X = interactable.HitBox.Right;
            }
            return Interactions.BLOCK;
        }

        private void HandleResult(Interactions interaction)
        {

        }
    }
}
