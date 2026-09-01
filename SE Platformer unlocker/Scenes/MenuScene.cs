using Library;
using Library.Graphics;
using Library.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.UI;



namespace SE_Platformer_unlocker.Scenes
{
    public class MenuScene : Scene
    {
        private UIButton start;

        private SpriteFont _standardFont;

        private Sprite buttonSprite;

        public override void Initialize()
        {
            base.Initialize();

            Core.ExitOnEscape = true;

            string s = "Start";
            Text startText = new Text(_standardFont, s);
            Vector2 size = _standardFont.MeasureString(s);
            startText.Origin = size * 0.5f;

            buttonSprite.CenterOrigin();
            buttonSprite.Scale = new Vector2(4f, 4f);
            
            start = new UIButton(startText, buttonSprite, new Rectangle((Core.WIDTH / 2) - ((int)buttonSprite.Width / 2), Core.HEIGHT / 2 - ((int)buttonSprite.Height / 2), (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.ChangeScene(new Level1()); });
        }

        public override void LoadContent()
        {
            TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, "sprites/atlas-definition.xml");

            buttonSprite = atlas.CreateSprite("button");

            _standardFont = Core.Content.Load<SpriteFont>("fonts/InstructionFont");
        }

        public override void Update(GameTime gameTime)
        {
            start.Update();
        }
        
        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));

            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.BackToFront);

            start.Draw(Core.SpriteBatch);

            Core.SpriteBatch.End();
        }
    }
}
