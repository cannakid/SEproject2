using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.Blocks;
using SE_Platformer_unlocker.Entities;
using SE_Platformer_unlocker.UI;
using System.Collections.Generic;

namespace SE_Platformer_unlocker
{
    public class Game1 : Game
    {
        public static Game1 Instance { get; private set; }

        public static readonly int WIDTH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static readonly int HEIGHT = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D brown;
        private Texture2D yellow;
        private SpriteFont font;

        private Champion champ;

        public List<IGameObject> LoadedObjects = new List<IGameObject>();
        public List<IUiElement> uiElements = new List<IUiElement>();

        public Game1()
        {
            if (Instance is null)
            {
                Instance = this;

                _graphics = new GraphicsDeviceManager(this);
                _graphics.IsFullScreen = true;
                _graphics.PreferredBackBufferWidth = WIDTH;
                _graphics.PreferredBackBufferHeight = HEIGHT;
                Content.RootDirectory = "Content";
                IsMouseVisible = true;
            }
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            LoadedObjects.Add(new Champion(yellow, new Point(0, 0), new Point(50, 50)));
            LoadedObjects.Add(new Block(brown, new Point(0, 1300), new Point(1000, 50)));
            LoadedObjects.Add(new Block(brown, new Point(200, 1225), new Point(50, 75)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));

            Rectangle rect = new Rectangle(new Point(600, 1200), new Point(120, 20));

            LoadedObjects.Add(new MovingPlatform(brown, rect, new Rectangle(rect.Location, rect.Size), new Point(600, 1200), new Point(1000, 1200), -1, 2));

            //uiElements.Add(new UIText("Hello world", new Rectangle(50, 50, 500, 100), font));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            brown = this.Content.Load<Texture2D>("Brown");
            yellow = this.Content.Load<Texture2D>("Yellow");

            font = this.Content.Load<SpriteFont>("File");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (IGameObject gameObject in LoadedObjects)
            {
                if (gameObject is IDynamic d)
                {
                    d.Update();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (IGameObject gameObject in LoadedObjects)
            {
                if (gameObject is IVisible v)
                {
                    v.Draw(_spriteBatch);
                }
            }
            foreach (IUiElement element in uiElements)
            {
                    element.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
