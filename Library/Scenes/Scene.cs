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

        /// <summary>
        /// Gets a value that indicates if the scene has been disposed of.
        /// </summary>
        public bool IsDisposed { get; private set; }


        public virtual void Initialize()
        {
            LoadContent();
        }

        /// <summary>
        /// Override to provide logic to load content for the scene.
        /// </summary>
        public virtual void LoadContent() { }

        /// <summary>
        /// Unloads scene-specific content.
        /// </summary>
        public virtual void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Updates this scene.
        /// </summary>
        /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws this scene.
        /// </summary>
        /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
        public abstract void Draw(GameTime gameTime);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of this scene.
        /// </summary>
        /// <param name="disposing">'
        /// Indicates whether managed resources should be disposed.  This value is only true when called from the main
        /// Dispose method.  When called from the finalizer, this will be false.
        /// </param>
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
