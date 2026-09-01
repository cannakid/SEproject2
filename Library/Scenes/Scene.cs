using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Scenes
{
    public abstract class Scene : IDisposable
    {
        public Scene()
        {
            // Create a content manager for the scene
            Content = new ContentManager(Core.Content.ServiceProvider);

            // Set the root directory for content to the same as the root directory
            // for the game's content.
            Content.RootDirectory = Core.Content.RootDirectory;
        }

        // Finalizer, called when object is cleaned up by garbage collector.
        ~Scene() => Dispose(false);


        protected ContentManager Content { get; }

        public bool IsDisposed { get; private set; }


        public virtual void Initialize()
        {
            LoadContent();
        }

        public abstract void LoadContent();

        public virtual void UnloadContent()
        {
            Content.Unload();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                UnloadContent();
                Content.Dispose();
            }
            IsDisposed = true;
        }
    }
}
