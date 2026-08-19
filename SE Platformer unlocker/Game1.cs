using Library;
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
    public class Game1 : Core
    {

        private Texture2D brown;
        private Texture2D yellow;
        private SpriteFont font;

        public List<IGameObject> LoadedObjects = new List<IGameObject>();
        public List<IUiElement> uiElements = new List<IUiElement>();

        public Game1() : base("Unlocker", false)
        {
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            LoadedObjects.Add(new Champion(yellow, new Point(0, 1200), new Point(50, 50)));
            LoadedObjects.Add(new Block(brown, new Point(0, 1300), new Point(1000, 50)));
            LoadedObjects.Add(new Block(brown, new Point(200, 1225), new Point(50, 75)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(800, 1000), new Point(500, 50)));
            LoadedObjects.Add(new Block(brown, new Point(1000, 800), new Point(200, 25)));
            /*LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));*/

            Rectangle rect = new Rectangle(new Point(600, 1200), new Point(120, 20));

            LoadedObjects.Add(new MovingPlatform(brown, rect, new Rectangle(rect.Location, rect.Size), new Point(600, 1200), new Point(1000, 1200), -1, 2));

            //uiElements.Add(new UIText("Hello world", new Rectangle(50, 50, 500, 100), font));
        }

        protected override void LoadContent()
        {

            brown = Content.Load<Texture2D>("Brown");
            yellow = Content.Load<Texture2D>("Yellow");

            font = Content.Load<SpriteFont>("File");

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

            SpriteBatch.Begin();

            foreach (IGameObject gameObject in LoadedObjects)
            {
                if (gameObject is IVisible v)
                {
                    v.Draw(SpriteBatch);
                }
            }
            foreach (IUiElement element in uiElements)
            {
                    element.Draw(SpriteBatch);
            }

            SpriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
