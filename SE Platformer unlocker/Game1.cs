using Library;
using Library.Graphics;
using Library.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.Blocks;
using SE_Platformer_unlocker.Entities;
using SE_Platformer_unlocker.UI;
using System;
using System.Collections.Generic;

namespace SE_Platformer_unlocker
{
    public class Game1 : Core
    {

        private Texture2D brown;
        private Texture2D yellow;
        private Texture2D spike;
        private SpriteFont font;

        public List<IGameObject> LoadedObjects = new List<IGameObject>();
        public List<IUiElement> uiElements = new List<IUiElement>();

        // texture region that defines the slime sprite in the atlas.
        private AnimatedSprite _slime;

        // Tracks the position of the slime.
        private Vector2 _slimePosition;

        // Speed multiplier when moving.
        private const float MOVEMENT_SPEED = 5.0f;

        // texture region that defines the bat sprite in the atlas.
        private AnimatedSprite _bat;

        public Game1() : base("Unlocker", false)
        {
            
        }

        protected override void Initialize()
        {
            base.Initialize();

            LoadedObjects.Add(new Champion(yellow, new Point(0, 1200), new Point(50, 50)));
            LoadedObjects.Add(new Block(brown, new Point(0, 1300), new Point(1000, 50)));
            LoadedObjects.Add(new Block(brown, new Point(200, 1225), new Point(50, 75)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(800, 1100), new Point(500, 50)));
            LoadedObjects.Add(new Block(brown, new Point(1300, 700), new Point(200, 25)));
            LoadedObjects.Add(new Block(brown, new Point(1200, 900), new Point(100, 50)));
            LoadedObjects.Add(new Spike(spike, new Point(350, 1150), new Point(50, 50)));
            /*LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
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

            //  Create a TextureAtlas instance from the atlas
            TextureAtlas atlas = TextureAtlas.FromFile(Content, "sprites/atlasDef.xml");

            // retrieve the slime region from the atlas.
            _slime = atlas.CreateAnimatedSprite("slime-animation");
            _slime.Scale = new Vector2(4.0f, 4.0f);

            // retrieve the bat region from the atlas.
            _bat = atlas.CreateAnimatedSprite("bat-animation");
            _bat.Scale = new Vector2(4.0f, 4.0f);

            brown = Content.Load<Texture2D>("Brown");
            yellow = Content.Load<Texture2D>("Yellow");
            spike = Content.Load<Texture2D>("Spike");

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
            _slime.Update(gameTime);
            _bat.Update(gameTime);

            // Check for keyboard input and handle it.
            CheckKeyboardInput();

            // Check for gamepad input and handle it.
            CheckGamePadInput();



            base.Update(gameTime);
        }

        private void CheckKeyboardInput()
        {
            // Get the state of keyboard input
            KeyboardState keyboardState = Keyboard.GetState();

            // If the space key is held down, the movement speed increases by 1.5
            float speed = MOVEMENT_SPEED;
            if (Input.Keyboard.IsKeyDown(Keys.LeftShift))
            {
                speed *= 1.5f;
            }

            // If the W or Up keys are down, move the slime up on the screen.
            if (Input.Keyboard.IsKeyDown(Keys.W) || Input.Keyboard.IsKeyDown(Keys.Up))
            {
                _slimePosition.Y -= speed;
            }

            // if the S or Down keys are down, move the slime down on the screen.
            if (Input.Keyboard.IsKeyDown(Keys.S) || Input.Keyboard.IsKeyDown(Keys.Down))
            {
                _slimePosition.Y += speed;
            }

            // If the A or Left keys are down, move the slime left on the screen.
            if (Input.Keyboard.IsKeyDown(Keys.A) || Input.Keyboard.IsKeyDown(Keys.Left))
            {
                _slimePosition.X -= speed;
            }

            // If the D or Right keys are down, move the slime right on the screen.
            if (Input.Keyboard.IsKeyDown(Keys.D) || Input.Keyboard.IsKeyDown(Keys.Right))
            {
                _slimePosition.X += speed;
            }
        }

        private void CheckGamePadInput()
        {
            GamePadInfo gamePadOne = Input.GamePads[(int)PlayerIndex.One];

            // If the A button is held down, the movement speed increases by 1.5
            // and the gamepad vibrates as feedback to the player.
            float speed = MOVEMENT_SPEED;
            if (gamePadOne.IsButtonDown(Buttons.A))
            {
                speed *= 1.5f;
                gamePadOne.SetVibration(1.0f, TimeSpan.FromSeconds(1));
            }
            else
            {
                 gamePadOne.StopVibration();
            }

            // Check thumbstick first since it has priority over which gamepad input
            // is movement.  It has priority since the thumbstick values provide a
            // more granular analog value that can be used for movement.
            if (gamePadOne.LeftThumbStick != Vector2.Zero)
            {
                _slimePosition.X += gamePadOne.LeftThumbStick.X * speed;
                _slimePosition.Y -= gamePadOne.LeftThumbStick.Y * speed;
            }
            else
            {
                // If DPadUp is down, move the slime up on the screen.
                if (gamePadOne.IsButtonDown(Buttons.DPadUp))
                {
                    _slimePosition.Y -= speed;
                }

                // If DPadDown is down, move the slime down on the screen.
                if (gamePadOne.IsButtonDown(Buttons.DPadDown))
                {
                    _slimePosition.Y += speed;
                }

                // If DPapLeft is down, move the slime left on the screen.
                if (gamePadOne.IsButtonDown(Buttons.DPadLeft))
                {
                    _slimePosition.X -= speed;
                }

                // If DPadRight is down, move the slime right on the screen.
                if (gamePadOne.IsButtonDown(Buttons.DPadRight))
                {
                    _slimePosition.X += speed;
                }
            }
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

            _slime.Draw(SpriteBatch, _slimePosition);

            // Draw the bat texture region 10px to the right of the slime at a scale of 4.0
            _bat.Draw(SpriteBatch, new Vector2(_slime.Width + 10, 0));

            SpriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
