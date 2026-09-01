using Library;
using Library.Graphics;
using Library.Input;
using Library.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.Blocks;
using SE_Platformer_unlocker.Entities;
using SE_Platformer_unlocker.UI;
using System;
using System.Collections.Generic;

namespace SE_Platformer_unlocker.Scenes
{
    public class Level1 : LevelScene
    {
        private Sprite _champSprite;
        private Champion _champ;

        // Defines the tilemap to draw.
        private TileMap _tileMap;

        // Defines the bounds of the room that the slime and bat are contained within.
        private Rectangle _roomBounds;

        // The sound effect to play when the bat bounces off the edge of the screen.
        private SoundEffect _hurtSoundEffect;

        // The sound effect to play when the slime eats a bat.
        private SoundEffect _jumpSoundEffect;

        private TextureAtlas idle;

        private UIIcon pauseButton;
        private Texture2D pauseTexture;

        private Block test;

        public override void Initialize()
        {
            base.Initialize();

            Core.ExitOnEscape = false;


            Rectangle screenBounds = Core.GraphicsDevice.PresentationParameters.Bounds;

            _champ = new Champion(_champSprite, new Point(100, 1000), new Point(80, 80), this);

            pauseButton = new UIIcon(new Sprite(new TextureRegion(pauseTexture, 0, 0, 600, 600)), new Rectangle(Core.WIDTH - 80, 80, 80, 80), () => { isPauseOpen = true; });
            pauseButton.CenterIcon();

            test = new Block(0, 1360, Core.WIDTH, 80);
            Interactables.Add(test);
        }

        public override void LoadContent()
        {
            idle = TextureAtlas.FromFile(Content, "sprites/idle-definition.xml");

            _champSprite = idle.CreateAnimatedSprite("idle-animation");

            // Create the tilemap from the XML configuration file.
            _tileMap = TileMap.FromFile(Content, "sprites/tilemap-definition.xml");
            _tileMap.Scale = new Vector2(5f, 5f);

            pauseTexture = Content.Load<Texture2D>("sprites/options_icon");
        }

        public override void Update(GameTime gameTime)
        {
            if (isPauseOpen)
            {
                pause.Update(gameTime);
            }
            else
            {
                _champ.Update(gameTime);

                // Check for keyboard input and handle it.
                CheckKeyboardInput();

                if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
                {
                    isPauseOpen = true;
                }
                pauseButton.Update(gameTime);
            }
                

            
        }

        private void CheckKeyboardInput()
        {
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

        public override void Draw(GameTime gameTime)
        {
            if (isPauseOpen)
            {
                Core.GraphicsDevice.Clear(Color.DarkBlue);
            }
            else
            {
                Core.GraphicsDevice.Clear(Color.CornflowerBlue);
            }
                

            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _tileMap.Draw(Core.SpriteBatch);

            _champ.Draw(gameTime);

            pauseButton.Draw(Core.SpriteBatch);

            Core.SpriteBatch.End();

            if (isPauseOpen)
            {
                pause.Draw(gameTime);
            }
        }

        public void CloseOptions()
        {
            isPauseOpen = false;
        }
    }
}
