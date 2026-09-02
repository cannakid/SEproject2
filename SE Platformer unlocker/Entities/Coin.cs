using Library.Graphics;
using Microsoft.Xna.Framework;
using SE_Platformer_unlocker.Collision;
using SE_Platformer_unlocker.Scenes;

namespace SE_Platformer_unlocker.Entities
{
    internal class Coin : Entity
    {
        private bool isCollected;

        public Coin(Sprite sprite, Rectangle hitBox, LevelScene scene) : base(sprite, hitBox, scene)
        {

        }

        public override InteractionType Interact(InteractionDirection direction)
        {
            return InteractionType.VICTORY;
        }

        public override void Draw(GameTime gameTime)
        {
            if (isCollected)
            {
                return;
            }
            base.Draw(gameTime);
        }
    }
}
