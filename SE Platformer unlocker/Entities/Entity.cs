using Library;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;
using System.Diagnostics;


namespace SE_Platformer_unlocker.Entities
{
    internal abstract class Entity : IDynamic, IInteractable
    {
        public Entity(Sprite sprite, Rectangle hitBox)
        {
            this.sprite = sprite;
            this.hitBox = hitBox;
            sprite.Scale = new Vector2((float)hitBox.Width / sprite.Region.Width, (float)hitBox.Height / sprite.Region.Height);
        }

        private Sprite sprite;

        public Rectangle HitBox { get => hitBox; }
        protected Rectangle hitBox;

        public void Draw(GameTime gameTime)
        {
            sprite.Draw(Core.SpriteBatch, hitBox.Location.ToVector2());
        }

        public abstract InteractionType Interact(InteractionDirection direction);
        

        public virtual void Update(GameTime gameTime)
        {
            if (sprite is AnimatedSprite a)
            {
                a.Update(gameTime);
            }
        }

        
    }
}
