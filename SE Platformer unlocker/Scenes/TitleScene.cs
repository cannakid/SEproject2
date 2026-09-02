using Library;
using Library.Graphics;
using Library.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SE_Platformer_unlocker.Scenes
{
    internal class TitleScene : BaseScene
    {
        private SpriteFont _titleFont;

        private SpriteFont _normalFont;

        private Texture2D _backgroundTexture;

        private Sprite _background;

        private bool inMenu = false;

        // for moving the background using floats instead of ints
        private Vector2 _backgroundOffset;

        // The speed that the background scrolls at.
        private readonly float _scrollSpeed = 20f;

        // weather the background is scrolling to the menu
        private bool toMenu = false;

        private IUiElement instructionText;

        private List<IUiElement> menuItems;

        private Sprite buttonSprite;

        public override void Initialize()
        {
            base.Initialize();


            Core.ExitOnEscape = true;

            UIText move = Game1.UIFactory.CreateText("Move", _titleFont, 960, 300);
            Add(move);
            Add(Game1.UIFactory.CreateShadow(move));

            UIText locked = Game1.UIFactory.CreateText("Locked", _titleFont, 960, 450);
            Add(locked);
            Add(Game1.UIFactory.CreateShadow(locked));

            instructionText = Game1.UIFactory.CreateText("Press Enter To Start", _normalFont, 960, 600);
            Add(instructionText);

            _background = new Sprite(new TextureRegion(_backgroundTexture, 0, 0, 480, 272));
            _background.Scale = new Vector2(2 * Core.WIDTH / _background.Width, 2 * Core.HEIGHT / _background.Height);
            //Add(_background);

            menuItems = new List<IUiElement>();

            UIButton start = Game1.UIFactory.CreateButton("Start", _normalFont, buttonSprite, 960, 600, 400, 200, () => { Core.ChangeScene(new Level1()); });
            Add(start);
            menuItems.Add(start);

            UIButton options = Game1.UIFactory.CreateButton("Options", _normalFont, buttonSprite, 960, 800, 400, 200, () => { Core.ChangeScene(new OptionsScene()); });
            Add(options);
            menuItems.Add(options);

            UIButton quit = Game1.UIFactory.CreateButton("Quit", _normalFont, buttonSprite, 960, 1000, 400, 200, () => { Core.Instance.Quit(); });
            Add(quit);
            menuItems.Add(quit);
            foreach (IUiElement ui in menuItems)
            {
                ui.Active = false;
            }
        }

        public override void LoadContent()
        {
            _normalFont = Core.Content.Load<SpriteFont>("fonts/InstructionFont");

            _titleFont = Core.Content.Load<SpriteFont>("fonts/TitleFont");

            _backgroundTexture = Content.Load<Texture2D>("sprites/Background");

            // create button sprite from an atlas
            TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, "sprites/hud-definition.xml");

            buttonSprite = atlas.CreateSprite("button");
            buttonSprite.CenterOrigin();
            buttonSprite.Scale = new Vector2(4f, 4f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // If the user presses enter, switch to the menu scene if not already there.
            if (!inMenu && Core.Input.Keyboard.WasKeyJustPressed(Keys.Enter))
            {
                toMenu = true;
                instructionText.Active = false;
            }

            // Update the offsets for the background pattern wrapping so that it
            // scrolls down and to the right.
            float offset = _scrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _backgroundOffset.X -= offset;
            _backgroundOffset.X %= _background.Width;

            _background.Region.SourceRectangle = new Rectangle(_backgroundOffset.ToPoint(), _background.Region.SourceRectangle.Size);

            // handels the menu screen
            Menu(gameTime);
        }

        private void Menu(GameTime gameTime)
        {
            if (toMenu)
            {
                float offset = 2 * _scrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _backgroundOffset.Y += offset;
                if (_backgroundOffset.Y >= _background.Region.Height / 3)
                {
                    _backgroundOffset.Y = _background.Region.Height / 3;
                    toMenu = false;
                    inMenu = true;
                    foreach (IUiElement ui in menuItems)
                    {
                        ui.Active = true;
                    }
                }
            }
            
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));

            // Background
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
            _background.Draw(Core.SpriteBatch, Vector2.Zero);
            Core.SpriteBatch.End();


            // Begin the sprite batch to prepare for rendering.
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.BackToFront);

            base.Draw(gameTime);

            Core.SpriteBatch.End();
        }
    }
}
