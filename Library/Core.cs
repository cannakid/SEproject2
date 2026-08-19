using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Library
{
    public class Core : Game
    {
        public static readonly int WIDTH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static readonly int HEIGHT = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        internal static Core instance;

        public static Core Instance => instance;

        public static GraphicsDeviceManager Graphics { get; private set; }

        public static new GraphicsDevice GraphicsDevice { get; private set; }

        public static SpriteBatch SpriteBatch { get; private set; }

        public static new ContentManager Content { get; private set; }

        public Core(string title, bool fullScreen)
        {
            // Ensure that multiple cores are not created.
            if (instance != null)
            {
                throw new InvalidOperationException("Only a single Core instance can be created");
            }

            // Store reference to engine for global member access.
            instance = this;

            // Create a new graphics device manager.
            Graphics = new GraphicsDeviceManager(this);

            // Set the graphics defaults.
            Graphics.PreferredBackBufferWidth = WIDTH;
            Graphics.PreferredBackBufferHeight = HEIGHT;
            Graphics.IsFullScreen = fullScreen;

            // Apply the graphic presentation changes.
            Graphics.ApplyChanges();

            // Set the window title.
            Window.Title = title;

            // Set the core's content manager to a reference of the base Game's
            // content manager.
            Content = base.Content;

            // Set the root directory for content.
            Content.RootDirectory = "Content";

            // Mouse is visible by default.
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            // Set the core's graphics device to a reference of the base Game's
            // graphics device.
            GraphicsDevice = base.GraphicsDevice;

            // Create the sprite batch instance.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}
