using Microsoft.Xna.Framework;
using SE_Platformer_unlocker.Base;
using System;



namespace SE_Platformer_unlocker.Entities
{
    internal abstract class Entity : IDynamic, IInteractable, IGameObject
    {
        public Vector2 Speed { get; set; }
        private Vector2 extra { get; set; }

        public Rectangle HitBox { get => hitBox; }
        protected Rectangle hitBox;

        public virtual void Update()
        {
            Move();
        }

        public void Move()
        {
            Vector2 actual = Speed + extra;
            extra = new Vector2(actual.X - (int)actual.X, actual.Y - (int)actual.Y);
            hitBox.Offset(actual);
        }

        public abstract void Interact(IInteractable interactable);
    }
}
