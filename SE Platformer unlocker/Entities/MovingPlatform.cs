using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Entities
{
    internal class MovingPlatform : Sprite, IDynamic, IInteractable
    {

        public Texture2D Texture { get; set; }

        public Rectangle TextureRect { get => textureRect; }

        public Rectangle HitBox { get => TextureRect; }

        protected Rectangle textureRect;
        private Vector2 speed;

        protected Vector2 moveSpeed;
        protected int steps;
        protected int currentSteps;
        protected bool inverse; // weather to go in the oposite direction

        public MovingPlatform(Texture2D texture, Rectangle hitBox, Rectangle textureRect, Point min, Point max, float start, int speed)
        {
            this.Texture = texture;
            this.textureRect = textureRect;

            if (start <= 0)
            {
                this.textureRect.Location = min;
            }
            if (start >= 1)
            {
                this.textureRect.Location = max;
            }

            Point distance = max - min;
            steps = (int)distance.ToVector2().Length() / speed;
            currentSteps = 0;
            Vector2 temp = distance.ToVector2();
            temp.Normalize();
            this.speed = speed * temp;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Texture, TextureRect, Color.White);
        }

        public void Interact(IInteractable interactable)
        {
            
        }

        public void Update()
        {
            textureRect.Location = HitBox.Location;
            if (currentSteps > steps)
            {
                speed *= -1;
                currentSteps = 0;
            }
            currentSteps++;
        }
    }
}
