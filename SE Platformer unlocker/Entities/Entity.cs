using Library;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.Collision;
using SE_Platformer_unlocker.Scenes;
using System.Collections.Generic;
using System.Diagnostics;


namespace SE_Platformer_unlocker.Entities
{
    internal abstract class Entity : IDynamic, IInteractable
    {
        public Entity(Sprite sprite, Rectangle hitBox, LevelScene scene)
        {
            sprites = new List<Sprite>();
            sprites.Add(sprite);
            this.hitBox = hitBox;
            sprite.Scale = new Vector2((float)hitBox.Width / sprite.Region.Width, (float)hitBox.Height / sprite.Region.Height);
            this.scene = scene;
            NextPos = hitBox;
        }

        public Entity(List<Sprite> sprites, Rectangle hitBox, LevelScene scene)
        {
            this.sprites = sprites;
            this.hitBox = hitBox;
            //sprite.Scale = new Vector2((float)hitBox.Width / sprite.Region.Width, (float)hitBox.Height / sprite.Region.Height);
            this.scene = scene;
            NextPos = hitBox;
        }


        protected LevelScene scene;

        protected List<Sprite> sprites;
        public int spriteIndex;

        public Rectangle HitBox { get => hitBox; }
        protected Rectangle hitBox;

        public Rectangle NextPos;

        public Vector2 speed = Vector2.Zero;

        public virtual void Draw(GameTime gameTime)
        {
            sprites[spriteIndex].Draw(Core.SpriteBatch, hitBox.Location.ToVector2());
        }

        public abstract InteractionType Interact(InteractionDirection direction);
        

        public virtual void Update(GameTime gameTime)
        {
            NextPos.Offset(speed);
            if (sprites[spriteIndex] is AnimatedSprite a)
            {
                a.Update(gameTime);
            }
        }

        public virtual void UpdatePosition()
        {
            hitBox = NextPos;
        }
        
    }
}
