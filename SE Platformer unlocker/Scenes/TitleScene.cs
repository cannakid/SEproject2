using Library;
using Library.Graphics;
using Library.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SE_Platformer_unlocker.UI;
using System;
using System.Diagnostics;

namespace SE_Platformer_unlocker.Scenes
{
    internal class TitleScene : Scene
    {
        
        private SpriteFont _titleFont;

        private SpriteFont _normalFont;

        private Texture2D _backgroundTexture;

        private Sprite _background;

        private bool inMenu = false;

        // title props
        private Text titleTop;
        private Text titleBottom;
        private Text titleTopShadow;
        private Text titleBottomShadow;
        private Text instructionText;

        // for moving the background using floats instead of ints
        private Vector2 _backgroundOffset;

        // The speed that the background scrolls at.
        private readonly float _scrollSpeed = 20f;

        // weather the background is scrolling to the menu
        private bool toMenu = false;


        
        // Menu props
        private UIButton start;
        private UIButton options;
        private UIButton quit;

        private Sprite buttonSprite;

        public override void Initialize()
        {
            // LoadContent is called during base.Initialize().
            base.Initialize();

            // While on the title screen, we can enable exit on escape so the player
            // can close the game by pressing the escape key.
            Core.ExitOnEscape = true;

            // title initialization
            string title1 = "Move";
            titleTop = new Text(_titleFont, title1);
            titleTop.CenterText();
            

            titleTopShadow = new Text(_titleFont, title1);
            titleTopShadow.CenterText();
            titleTopShadow.Color = Color.Black * 0.5f;
            titleTopShadow.LayerDepth = 1;
            

            string title2 = "Locked";
            titleBottom = new Text(_titleFont, title2);
            titleBottom.CenterText();
            

            titleBottomShadow = new Text(_titleFont, title2);
            titleBottomShadow.CenterText();
            titleBottomShadow.Color = Color.Black * 0.5f;
            titleBottomShadow.LayerDepth = 1;
            

            string instruction = "Press Enter To Start";
            instructionText = new Text(_normalFont, instruction);
            instructionText.CenterText();
            


            _background = new Sprite(new TextureRegion(_backgroundTexture, 0, 0, 480, 272));
            _background.Scale = new Vector2(2* Core.WIDTH / _background.Width, 2 * Core.HEIGHT / _background.Height);


            // Menu initialization
            Text startText = new Text(_normalFont, "Start");

            Text optionsText = new Text(_normalFont, "Options");

            Text quitText = new Text(_normalFont, "Quit");

            buttonSprite.CenterOrigin();
            buttonSprite.Scale = new Vector2(4f, 4f);

            start = new UIButton(startText, buttonSprite, new Rectangle(Core.WIDTH / 2, 2 * Core.HEIGHT / 5, (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.ChangeScene(new Level1()); });
            start.CenterButton();

            options = new UIButton(optionsText, buttonSprite, new Rectangle(Core.WIDTH / 2, (int)(2.5f * Core.HEIGHT / 5), (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.ChangeScene(new OptionsScene()); });
            options.CenterButton();

            quit = new UIButton(quitText, buttonSprite, new Rectangle(Core.WIDTH / 2, 3 * Core.HEIGHT / 5, (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.Instance.Quit(); });
            quit.CenterButton();
        }

        public override void LoadContent()
        {
            // Load the font for the standard text.
            _normalFont = Core.Content.Load<SpriteFont>("fonts/InstructionFont");

            // Load the font for the start instruction
            _titleFont = Core.Content.Load<SpriteFont>("fonts/TitleFont");

            // Load the background pattern texture.
            _backgroundTexture = Content.Load<Texture2D>("sprites/Background");

            TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, "sprites/hud-definition.xml");

            buttonSprite = atlas.CreateSprite("button");
        }

        public override void Update(GameTime gameTime)
        {
            // If the user presses enter, switch to the menu scene if not already there.
            if (!inMenu && Core.Input.Keyboard.WasKeyJustPressed(Keys.Enter))
            {
                toMenu = true;
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
                }
            }
            else if (inMenu)
            {
                start.Update(gameTime);
                options.Update(gameTime);
                quit.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 test = Vector2.Zero;
            test.ToPoint();
            Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));

            // Background
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
            _background.Draw(Core.SpriteBatch, Vector2.Zero);
            Core.SpriteBatch.End();


            // Begin the sprite batch to prepare for rendering.
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.BackToFront);

            int center = Core.WIDTH / 2;

            titleTop.Draw(Core.SpriteBatch, new Vector2(center, Core.HEIGHT / 5));
            titleTopShadow.Draw(Core.SpriteBatch, new Vector2(center, Core.HEIGHT / 5) + new Vector2(10, 10));

            titleBottom.Draw(Core.SpriteBatch, new Vector2(center, (int)(1.5f * Core.HEIGHT / 5)));
            titleBottomShadow.Draw(Core.SpriteBatch, new Vector2(center, (int)(1.5f * Core.HEIGHT / 5)) + new Vector2(10, 10));

            if (!inMenu && !toMenu)
            {
                instructionText.Draw(Core.SpriteBatch, new Vector2(center, 2 * Core.HEIGHT / 5));
            }
            if (inMenu)
            {
                start.Draw(Core.SpriteBatch);
                options.Draw(Core.SpriteBatch);
                quit.Draw(Core.SpriteBatch);
            }

            Core.SpriteBatch.End();
        }
    }
}
