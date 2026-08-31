using Library.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public static InputManager Input { get; private set; }

        public static bool ExitOnEscape { get; set; }

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

            // Exit on escape is true by default
            ExitOnEscape = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            // Set the core's graphics device to a reference of the base Game's
            // graphics device.
            GraphicsDevice = base.GraphicsDevice;

            // Create the sprite batch instance.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Create a new input manager.
            Input = new InputManager();
        }

        protected override void Update(GameTime gameTime)
        {
            // Update the input manager.
            Input.Update(gameTime);

            if (ExitOnEscape && Input.Keyboard.WasKeyJustPressed(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }
    }
}
