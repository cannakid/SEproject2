using Library;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SE_Platformer_unlocker.Blocks;
using SE_Platformer_unlocker.Entities;
using SE_Platformer_unlocker.UI;
using System.Collections.Generic;

namespace SE_Platformer_unlocker.Scenes
{
    public class Level2 : LevelScene
    {
        private List<Sprite> _champSprites = new List<Sprite>();
        private Champion _champ;

        private Sprite _slimeSprite;
        private Creature _slime_1;

        private Sprite _dangerSlimeSprite;
        private Creature _slime_2;

        private Sprite _coinSprite;
        private Entity _coin;

        // Defines the tilemap to draw.
        private BlockMap _blockMap;

        // The sound effect to play when the bat bounces off the edge of the screen.
        private SoundEffect _hurtSoundEffect;

        // The sound effect to play when the slime eats a bat.
        private SoundEffect _jumpSoundEffect;

        private TextureAtlas idle;

        private UIIcon pauseButton;
        private Texture2D pauseTexture;

        private Sprite _heartSprite;
        private Sprite _emptyHeartSprite;
        private List<UIIcon> _hearts;

        private int currentHeartDisplay;

        public override void Initialize()
        {
            base.Initialize();

            Core.ExitOnEscape = false;


            Rectangle screenBounds = Core.GraphicsDevice.PresentationParameters.Bounds;

            _champ = new Champion(_champSprites, new Point(100, 880), new Point(60, 60), this, _jumpSoundEffect, _hurtSoundEffect, 3);
            collisionHandler.Add(_champ);

            _slime_1 = new Slime(_slimeSprite, new Point(500, 880), new Point(60, 60), this, 1);
            collisionHandler.Add(_slime_1);

            _slime_2 = new DangerSlime(_dangerSlimeSprite, new Point(500, 600), new Point(60, 60), this, 1, _champ);
            collisionHandler.Add(_slime_2);

            _coin = new Coin(_coinSprite, new Rectangle(1700, 240, 60, 60), this);
            collisionHandler.Add(_coin);

            pauseButton = new UIIcon(new Sprite(new TextureRegion(pauseTexture, 0, 0, 600, 600)), new Rectangle(Core.WIDTH - 80, 80, 80, 80), () => { isPauseOpen = true; });
            pauseButton.CenterIcon();


            List<Block> blocks = _blockMap.CreateBlocks();
            foreach (Block b in blocks)
            {
                collisionHandler.Add(b);
            }
            _hearts = new List<UIIcon>();
            for (int i = 0; i < 3; i++)
            {
                _hearts.Add(new UIIcon(_heartSprite, new Rectangle(40 + 80 * i, 40, 80, 80), () => { }));
            }
            currentHeartDisplay = _champ.Health;
        }

        public override void LoadContent()
        {
            idle = TextureAtlas.FromFile(Content, "sprites/idle-definition.xml");

            _champSprites.Add(idle.CreateAnimatedSprite("idle-animation"));

            TextureAtlas slimeAtlas = TextureAtlas.FromFile(Content, "sprites/slime-definition.xml");

            _slimeSprite = slimeAtlas.CreateAnimatedSprite("slime-animation");

            TextureAtlas dangerSlimeAtlas = TextureAtlas.FromFile(Content, "sprites/dangerslime-definition.xml");

            _dangerSlimeSprite = dangerSlimeAtlas.CreateAnimatedSprite("slime-animation");

            TextureAtlas coinAtlas = TextureAtlas.FromFile(Content, "sprites/coin-definition.xml");

            _coinSprite = coinAtlas.CreateAnimatedSprite("coin-animation");

            TextureAtlas heartAtlas = TextureAtlas.FromFile(Content, "sprites/hearts-definition.xml");

            _heartSprite = heartAtlas.CreateSprite("heart-full");
            _emptyHeartSprite = heartAtlas.CreateSprite("heart-empty");

            // Create the tilemap from the XML configuration file.
            TileMap _tileMap = TileMap.FromFile(Content, "sprites/tilemap2-definition.xml");
            _blockMap = new BlockMap(_tileMap);
            _blockMap.Scale = new Vector2(4f, 4f);

            pauseTexture = Content.Load<Texture2D>("sprites/options_icon");

            _jumpSoundEffect = Content.Load<SoundEffect>("audio/jump");

            _hurtSoundEffect = Content.Load<SoundEffect>("audio/hurt");


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

                _slime_1.Update(gameTime);

                _slime_2.Update(gameTime);

                _coin.Update(gameTime);

                collisionHandler.HandleCollisions();

                // Check for keyboard input and handle it.
                CheckKeyboardInput();

                if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
                {
                    isPauseOpen = true;
                }
                pauseButton.Update(gameTime);

                if (_champ.Health < currentHeartDisplay)
                {
                    _hearts[currentHeartDisplay - 1].ChangeIcon(_emptyHeartSprite);
                    currentHeartDisplay--;
                }
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

            _blockMap.Draw(Core.SpriteBatch);

            _champ.Draw(gameTime);

            _slime_1.Draw(gameTime);

            _slime_2.Draw(gameTime);

            _coin.Draw(gameTime);

            pauseButton.Draw(Core.SpriteBatch);

            foreach (UIIcon heart in _hearts)
            {
                heart.Draw(Core.SpriteBatch);
            }

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
