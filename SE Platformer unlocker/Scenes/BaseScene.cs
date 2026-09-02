using Library;
using Library.Scenes;
using Microsoft.Xna.Framework;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Scenes
{
    public abstract class BaseScene : Scene
    {
        private List<IGameObject> sceneObjects;

        public override void Initialize()
        {
            base.Initialize();
            sceneObjects = new List<IGameObject>();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (IGameObject gameObject in sceneObjects)
            {
                if (!gameObject.Active) continue;
                if (gameObject is IUiElement ui)
                {
                    ui.Draw(Core.SpriteBatch);
                }
                if (gameObject is Drawable d)
                {
                    d.Draw();
                }
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IGameObject gameObject in sceneObjects)
            {
                if (!gameObject.Active) continue;
                if (gameObject is IDynamic d)
                {
                    d.Update(gameTime);
                }
            }
        }

        public void Add(IGameObject gameObject)
        {
            sceneObjects.Add(gameObject);
        }
    }
}
