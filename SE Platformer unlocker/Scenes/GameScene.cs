using Library;
using Library.Graphics;
using Library.Scenes;
using Microsoft.Xna.Framework;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.Collision;
using System.Collections.Generic;

namespace SE_Platformer_unlocker.Scenes
{
    public abstract class GameScene : Scene
    {
        private List<Drawable> drawables;

        private List<IDynamic> updatables;

        public List<IInteractable> Interactables { get; private set; }

        public override void Draw(GameTime gameTime)
        {
            foreach (Drawable sprite in drawables)
            {
                sprite.Draw();
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IDynamic item in updatables)
            {
                
                item.Update(gameTime);
            }
        }

        public void Add(object item)
        {
            if (item is Drawable d)
            {
                drawables.Add(d);
            }
            if (item is IDynamic dynamic)
            {
                updatables.Add(dynamic);
            }
            if (item is IInteractable i)
            {
                Interactables.Add(i);
            }
        }
    }
}
