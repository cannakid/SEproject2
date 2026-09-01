using Library;
using Library.Graphics;
using Library.Input;
using Library.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SE_Platformer_unlocker.Scenes
{
    public class GameScene : Scene
    {
        private AnimatedSprite _slime;

        // Defines the bat animated sprite.
        private AnimatedSprite _bat;

        // Tracks the position of the slime.
        private Vector2 _slimePosition;

        // Speed multiplier when moving.
        private const float MOVEMENT_SPEED = 5.0f;

        // Defines the tilemap to draw.
        private TileMap _tileMap;

        // Defines the bounds of the room that the slime and bat are contained within.
        private Rectangle _roomBounds;

        // The sound effect to play when the bat bounces off the edge of the screen.
        private SoundEffect _bounceSoundEffect;

        // The sound effect to play when the slime eats a bat.
        private SoundEffect _collectSoundEffect;


        public override void Initialize()
        {
            base.Initialize();

            Core.ExitOnEscape = false;

            Rectangle screenBounds = Core.GraphicsDevice.PresentationParameters.Bounds;

            _roomBounds = new Rectangle(
                 (int)_tileMap.TileWidth,
                 (int)_tileMap.TileHeight,
                 screenBounds.Width - (int)_tileMap.TileWidth * 2,
                 screenBounds.Height - (int)_tileMap.TileHeight * 2
             );

            // Initial slime position will be the center tile of the tile map.
            int centerRow = _tileMap.Rows / 2;
            int centerColumn = _tileMap.Columns / 2;
            _slimePosition = new Vector2(centerColumn * _tileMap.TileWidth, centerRow * _tileMap.TileHeight);

        }

        public override void LoadContent()
        {
            // Create the texture atlas from the XML configuration file.
            TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, "sprites/atlas-definition.xml");

            // Create the slime animated sprite from the atlas.
            _slime = atlas.CreateAnimatedSprite("slime-animation");
            _slime.Scale = new Vector2(4.0f, 4.0f);

            // Create the bat animated sprite from the atlas.
            _bat = atlas.CreateAnimatedSprite("bat-animation");
            _bat.Scale = new Vector2(4.0f, 4.0f);

            // Create the tilemap from the XML configuration file.
            _tileMap = TileMap.FromFile(Content, "sprites/tilemap-definition.xml");
            _tileMap.Scale = new Vector2(4.0f, 4.0f);

            // Load the bounce sound effect.
            _bounceSoundEffect = Content.Load<SoundEffect>("audio/bounce");

            // Load the collect sound effect.
            _collectSoundEffect = Content.Load<SoundEffect>("audio/collect");
        }

        public override void Update(GameTime gameTime)
        {
            _slime.Update(gameTime);
            _bat.Update(gameTime);

            // Check for keyboard input and handle it.
            CheckKeyboardInput();

            // Check for gamepad input and handle it.
            CheckGamePadInput();

            Rectangle slimeBounds = new Rectangle((int)_slimePosition.X, (int)_slimePosition.Y, (int)_slime.Width, (int)_slime.Height);

            if (slimeBounds.Left < _roomBounds.Left)
            {
                Core.Audio.PlaySoundEffect(_collectSoundEffect);
                _slimePosition.X = _roomBounds.Left;
            }
            else if (slimeBounds.Right > _roomBounds.Right)
            {
                Core.Audio.PlaySoundEffect(_collectSoundEffect);
                _slimePosition.X = _roomBounds.Right - _slime.Width;
            }

            if (slimeBounds.Top < _roomBounds.Top)
            {
                Core.Audio.PlaySoundEffect(_collectSoundEffect);
                _slimePosition.Y = _roomBounds.Top;
            }
            else if (slimeBounds.Bottom > _roomBounds.Bottom)
            {
                Core.Audio.PlaySoundEffect(_collectSoundEffect);
                _slimePosition.Y = _roomBounds.Bottom - _slime.Height;
            }

            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
            {
                Core.ChangeScene(new TitleScene());
            }
        }

        private void CheckKeyboardInput()
        {
            // If the space key is held down, the movement speed increases by 1.5
            float speed = MOVEMENT_SPEED;
            if (Core.Input.Keyboard.IsKeyDown(Keys.LeftShift))
            {
                speed *= 1.5f;
            }

            // If the W or Up keys are down, move the slime up on the screen.
            if (Core.Input.Keyboard.IsKeyDown(Keys.W))
            {
                _slimePosition.Y -= speed;
            }

            // if the S or Down keys are down, move the slime down on the screen.
            if (Core.Input.Keyboard.IsKeyDown(Keys.S))
            {
                _slimePosition.Y += speed;
            }

            // If the A or Left keys are down, move the slime left on the screen.
            if (Core.Input.Keyboard.IsKeyDown(Keys.A))
            {
                _slimePosition.X -= speed;
            }

            // If the D or Right keys are down, move the slime right on the screen.
            if (Core.Input.Keyboard.IsKeyDown(Keys.D))
            {
                _slimePosition.X += speed;
            }


            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.M))
            {
                Core.Audio.ToggleMute();
            }

            // If the + button is pressed, increase the volume.
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.OemPlus))
            {
                Core.Audio.SongVolume += 0.1f;
                Core.Audio.SoundEffectVolume += 0.1f;
            }

            // If the - button was pressed, decrease the volume.
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.OemMinus))
            {
                Core.Audio.SongVolume -= 0.1f;
                Core.Audio.SoundEffectVolume -= 0.1f;
            }
        }

        private void CheckGamePadInput()
        {
            GamePadInfo gamePadOne = Core.Input.GamePads[(int)PlayerIndex.One];

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

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.CornflowerBlue);

            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _tileMap.Draw(Core.SpriteBatch);

            _slime.Draw(Core.SpriteBatch, _slimePosition);

            // Draw the bat texture region 10px to the right of the slime at a scale of 4.0
            _bat.Draw(Core.SpriteBatch, new Vector2(_slime.Width + 10, 0));

            Core.SpriteBatch.End();
        }
    }
}
