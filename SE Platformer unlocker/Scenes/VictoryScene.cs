using Library;
using Library.Graphics;
using Library.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Scenes
{
    internal class VictoryScene : Scene
    {
        private SpriteFont _titleFont;

        private SpriteFont _normalFont;

        private Texture2D _backgroundTexture;

        private Sprite _background;

        private Text titleTop;
        private Text titleTopShadow;

        private UIButton next;
        private UIButton menu;
        private UIButton quit;

        private Sprite buttonSprite;

        public override void Initialize()
        {
            base.Initialize();

            string title1 = "Victory";
            titleTop = new Text(_titleFont, title1);
            titleTop.CenterText();


            titleTopShadow = new Text(_titleFont, title1);
            titleTopShadow.CenterText();
            titleTopShadow.Color = Color.Black * 0.5f;
            titleTopShadow.LayerDepth = 1;


            Text nextText = new Text(_normalFont, "Next Level");

            Text menuText = new Text(_normalFont, "To Menu");

            Text quitText = new Text(_normalFont, "Quit");

            buttonSprite.CenterOrigin();
            buttonSprite.Scale = new Vector2(4f, 4f);

            next = new UIButton(nextText, buttonSprite, new Rectangle(Core.WIDTH / 2, (int)(2 * Core.HEIGHT / 5), (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.ChangeScene(new Level2()); });
            next.CenterButton();

            menu = new UIButton(menuText, buttonSprite, new Rectangle(Core.WIDTH / 2, (int)(2.5f * Core.HEIGHT / 5), (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.ChangeScene(new TitleScene()); });
            menu.CenterButton();

            quit = new UIButton(quitText, buttonSprite, new Rectangle(Core.WIDTH / 2, 3 * Core.HEIGHT / 5, (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.Instance.Quit(); });
            quit.CenterButton();
        }
        
        public override void LoadContent()
        {
            _normalFont = Core.Content.Load<SpriteFont>("fonts/InstructionFont");

            _titleFont = Core.Content.Load<SpriteFont>("fonts/TitleFont");

            // Load the background pattern texture.
            _backgroundTexture = Content.Load<Texture2D>("sprites/Background");

            TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, "sprites/hud-definition.xml");

            buttonSprite = atlas.CreateSprite("button");
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));

            // Background
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
            //_background.Draw(Core.SpriteBatch, Vector2.Zero);
            Core.SpriteBatch.End();


            // rest
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.BackToFront);

            int center = Core.WIDTH / 2;

            titleTop.Draw(Core.SpriteBatch, new Vector2(center, Core.HEIGHT / 5));
            titleTopShadow.Draw(Core.SpriteBatch, new Vector2(center, Core.HEIGHT / 5) + new Vector2(10, 10));

            menu.Draw(Core.SpriteBatch);
            quit.Draw(Core.SpriteBatch);

            Core.SpriteBatch.End();
        }

        

        public override void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
            quit.Update(gameTime);
        }
    }
}
