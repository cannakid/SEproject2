using Library;
using Library.Graphics;
using Library.Input;
using Library.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SE_Platformer_unlocker.Entities;
using System;

namespace SE_Platformer_unlocker.Scenes
{
    public class Level1 : Scene
    {
        private Champion _champ;

        // Defines the tilemap to draw.
        private TileMap _tileMap;

        // Defines the bounds of the room that the slime and bat are contained within.
        private Rectangle _roomBounds;

        // The sound effect to play when the bat bounces off the edge of the screen.
        private SoundEffect _hurtSoundEffect;

        // The sound effect to play when the slime eats a bat.
        private SoundEffect _jumpSoundEffect;


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

            
            
        }

        public override void LoadContent()
        {
            // Create the texture atlas from the XML configuration file.
            TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, "sprites/world-definition.xml");

            

            // Create the tilemap from the XML configuration file.
            _tileMap = TileMap.FromFile(Content, "sprites/tilemap-definition.xml");
            _tileMap.Scale = new Vector2(5f, 5f);

            // Load the bounce sound effect.
            //_hurtSoundEffect = Content.Load<SoundEffect>("audio/hunt");
            
        }

        public override void Update(GameTime gameTime)
        {
           

            // Check for keyboard input and handle it.
            CheckKeyboardInput();



            

            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
            {
                Core.ChangeScene(new TitleScene());
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
            Core.GraphicsDevice.Clear(Color.CornflowerBlue);

            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _tileMap.Draw(Core.SpriteBatch);

            Core.SpriteBatch.End();
        }
    }
}
