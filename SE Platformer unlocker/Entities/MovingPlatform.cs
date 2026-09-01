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
    internal class MovingPlatform : Entity
    {
        private Vector2 speed;

        protected Vector2 moveSpeed;
        protected int steps;
        protected int currentSteps;
        protected bool inverse; // weather to go in the oposite direction

        public MovingPlatform(Sprite sprite, Rectangle hitBox, Point min, Point max, float start, int speed) : base(sprite, hitBox)
        {

            if (start <= 0)
            {
                this.hitBox.Location = min;
            }
            if (start >= 1)
            {
                this.hitBox.Location = max;
            }

            Point distance = max - min;
            steps = (int)distance.ToVector2().Length() / speed;
            currentSteps = 0;
            Vector2 temp = distance.ToVector2();
            temp.Normalize();
            this.speed = speed * temp;
        }

        public override InteractionType Interact(InteractionDirection direction)
        {
            return InteractionType.BLOCK;
        }

        public override void Update(GameTime gameTime)
        {
            //hitBox.Location = HitBox.Location;
            if (currentSteps > steps)
            {
                speed *= -1;
                currentSteps = 0;
            }
            currentSteps++;
        }
    }
}
