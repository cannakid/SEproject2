using Library;
using Library.Graphics;
using Library.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace SE_Platformer_unlocker.Scenes
{
    internal class TitleScene : Scene
    {
        private Text titleTop;
        private Text titleBottom;
        private Text titleTopShadow;
        private Text titleBottomShadow;
        private Text instructionText;

        private SpriteFont _titleFont;

        private SpriteFont _instructionFont;

        // The texture used for the background pattern.
        private Texture2D _backgroundPattern;

        private Sprite _background;

        // The offset to apply when drawing the background pattern so it appears to
        // be scrolling.
        private Vector2 _backgroundOffset;

        // The speed that the background pattern scrolls.
        private float _scrollSpeed = 20f;

        public override void Initialize()
        {
            // LoadContent is called during base.Initialize().
            base.Initialize();

            // While on the title screen, we can enable exit on escape so the player
            // can close the game by pressing the escape key.
            Core.ExitOnEscape = true;

            string title1 = "Move";
            titleTop = new Text(_titleFont, title1); 
            Vector2 size = _titleFont.MeasureString(title1);
            titleTop.Origin = size * 0.5f;

            titleTopShadow = new Text(_titleFont, title1);
            titleTopShadow.Origin = size * 0.5f;
            titleTopShadow.Color = Color.Black;
            titleTopShadow.LayerDepth = 1;

            string title2 = "Locked";
            titleBottom = new Text(_titleFont, title2);
            size = _titleFont.MeasureString(title2);
            titleBottom.Origin = size * 0.5f;

            titleBottomShadow = new Text(_titleFont, title2);
            titleBottomShadow.Origin = size * 0.5f;
            titleBottomShadow.Color = Color.Black;
            titleBottomShadow.LayerDepth = 1;

            string instruction = "Press Enter To Start";
            instructionText = new Text(_instructionFont, instruction);
            size = _instructionFont.MeasureString(instruction);
            instructionText.Origin = size * 0.5f;


            _background = new Sprite(new TextureRegion(_backgroundPattern, 0, 0, 480, 272));
            _background.Scale = new Vector2(Core.WIDTH / _background.Width, Core.HEIGHT / _background.Height);
        }

        public override void LoadContent()
        {
            // Load the font for the standard text.
            _instructionFont = Core.Content.Load<SpriteFont>("fonts/InstructionFont");

            // Load the font for the start instruction
            _titleFont = Core.Content.Load<SpriteFont>("fonts/TitleFont");

            // Load the background pattern texture.
            _backgroundPattern = Content.Load<Texture2D>("sprites/Background");
        }

        public override void Update(GameTime gameTime)
        {
            // If the user presses enter, switch to the game scene.
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Enter))
            {
                Core.ChangeScene(new MenuScene());
            }

            // Update the offsets for the background pattern wrapping so that it
            // scrolls down and to the right.
            float offset = _scrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _backgroundOffset.X -= offset;
            _backgroundOffset.X %= _background.Width;

            _background.Region.SourceRectangle = new Rectangle(_backgroundOffset.ToPoint(), _background.Region.SourceRectangle.Size);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 test = Vector2.Zero;
            test.ToPoint();
            Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));

            // Draw the background pattern first using the PointWrap sampler state.
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
            _background.Draw(Core.SpriteBatch, Vector2.Zero);
            Core.SpriteBatch.End();


            // Begin the sprite batch to prepare for rendering.
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.BackToFront);

            // The color to use for the drop shadow text.
            Color dropShadowColor = Color.Black * 0.5f;

            int center = Core.WIDTH / 2;

            titleTop.Draw(Core.SpriteBatch, new Vector2(center, Core.HEIGHT / 5));
            titleTopShadow.Draw(Core.SpriteBatch, new Vector2(center, Core.HEIGHT / 5) + new Vector2(10, 10));

            titleBottom.Draw(Core.SpriteBatch, new Vector2(center, (int)(1.5f * Core.HEIGHT / 5)));
            titleBottomShadow.Draw(Core.SpriteBatch, new Vector2(center, (int)(1.5f * Core.HEIGHT / 5)) + new Vector2(10, 10));


            instructionText.Draw(Core.SpriteBatch, new Vector2(center, 2 * Core.HEIGHT / 5));

            // Always end the sprite batch when finished.
            Core.SpriteBatch.End();
        }
    }
}
