using Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SE_Platformer_unlocker.Base;
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
        private Rectangle nextPos;

        public Champion(Texture2D texture, Point pos, Point size)
        {
            Texture = texture;
            textureRect = new Rectangle(pos, size);
            hitBox = new Rectangle(pos, size);
            nextPos = hitBox;
        }

        public override void Update()
        {
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
            if (TextureRect.Top > Game1.HEIGHT)
            {
                Game1.Instance.Exit(); // replace with game over screen
            }
            if (isGrounded && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                speed.Y -= 12;
                isGrounded = false;
            }
            nextPos.Offset(speed);
            // interact
            remainGrounded = false;
            foreach (IGameObject gameObject in (Core.Instance as Game1).LoadedObjects)
            {
                if (gameObject is IInteractable)
                {
                    Interact(gameObject as IInteractable);
                }
            }
            isGrounded = remainGrounded;
            hitBox = nextPos;
            textureRect = nextPos;
        }

        public override void Interact(IInteractable interactable)
        {
            if (interactable.HitBox.Top == HitBox.Bottom + 1)
            {
                remainGrounded = true;
            }
            if (!interactable.HitBox.Intersects(nextPos))
            {
                return;
            }
            if (HitBox.Bottom <= interactable.HitBox.Top)
            {
                speed.Y = 0;
                isGrounded = true;
                remainGrounded = true;
                nextPos.Y = interactable.HitBox.Top - HitBox.Size.Y; // Y is top side
            }
            else if (HitBox.Top >= interactable.HitBox.Bottom)
            {
                speed.Y = 0;
                nextPos.Y = interactable.HitBox.Bottom;
            }
            else if (HitBox.Right <= interactable.HitBox.Left)
            {
                speed.X = 0;
                nextPos.X = interactable.HitBox.Left - HitBox.Size.X; // X is left side
            }
            else if (HitBox.Left >= interactable.HitBox.Right)
            {
                speed.X = 0;
                nextPos.X = interactable.HitBox.Right;
            }
        }
    }
}
