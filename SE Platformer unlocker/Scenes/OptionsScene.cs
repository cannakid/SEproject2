using Library;
using Library.Graphics;
using Library.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.UI;

namespace SE_Platformer_unlocker.Scenes
{
    internal class OptionsScene : Scene
    {
        private SpriteFont _titleFont;

        private SpriteFont _normalFont;

        private Texture2D _backgroundTexture;

        private Sprite _background;

        private Text titleTop;
        private Text titleBottom;
        private Text titleTopShadow;
        private Text titleBottomShadow;

        private UIButton cont;
        private UIButton options;
        private UIButton menu;

        private Sprite buttonSprite;


        public override void Initialize()
        {
            base.Initialize();

            Core.ExitOnEscape = false;

            string title1 = "Options";
            titleTop = new Text(_titleFont, title1);
            titleTop.CenterText();


            titleTopShadow = new Text(_titleFont, title1);
            titleTopShadow.CenterText();
            titleTopShadow.Color = Color.Black * 0.5f;
            titleTopShadow.LayerDepth = 1;


            Text startText = new Text(_normalFont, "Start");

            Text optionsText = new Text(_normalFont, "Options");

            Text quitText = new Text(_normalFont, "Quit");

            buttonSprite.CenterOrigin();
            buttonSprite.Scale = new Vector2(4f, 4f);

            //start = new UIButton(startText, buttonSprite, new Rectangle(Core.WIDTH / 2, 2 * Core.HEIGHT / 5, (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.ChangeScene(new Level1()); });
            //start.CenterButton();

            options = new UIButton(optionsText, buttonSprite, new Rectangle(Core.WIDTH / 2, (int)(2.5f * Core.HEIGHT / 5), (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.ChangeScene(new OptionsScene()); });
            options.CenterButton();

            //quit = new UIButton(quitText, buttonSprite, new Rectangle(Core.WIDTH / 2, 3 * Core.HEIGHT / 5, (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.Instance.Quit(); });
            //quit.CenterButton();

        }
        public override void LoadContent()
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.BackToFront);

            cont.Draw(Core.SpriteBatch);
            options.Draw(Core.SpriteBatch);
            menu.Draw(Core.SpriteBatch);

            int center = Core.WIDTH / 2;

            titleTop.Draw(Core.SpriteBatch, new Vector2(center, Core.HEIGHT / 5));
            titleTopShadow.Draw(Core.SpriteBatch, new Vector2(center, Core.HEIGHT / 5) + new Vector2(10, 10));

            Core.SpriteBatch.End();
        }

        

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
