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
    internal class PauseScene : Scene
    {
        public PauseScene(LevelScene level)
        {
            this.level = level;
        }


        private LevelScene level;

        private SpriteFont _titleFont;

        private SpriteFont _normalFont;

        private Text titleTop;
        private Text titleTopShadow;

        private UIButton cont;
        private UIButton options;
        private UIButton menu;

        private Sprite buttonSprite;

        public override void Initialize()
        {
            base.Initialize();

            string title1 = "Paused";
            titleTop = new Text(_titleFont, title1);
            titleTop.CenterText();


            titleTopShadow = new Text(_titleFont, title1);
            titleTopShadow.CenterText();
            titleTopShadow.Color = Color.Black * 0.5f;
            titleTopShadow.LayerDepth = 1;

            Text contText = new Text(_normalFont, "Continue");

            Text optionsText = new Text(_normalFont, "Options");

            Text menuText = new Text(_normalFont, "Back to Menu");

            buttonSprite.CenterOrigin();
            buttonSprite.Scale = new Vector2(6f, 4f);

            cont = new UIButton(contText, buttonSprite, new Rectangle(Core.WIDTH / 2, 2 * Core.HEIGHT / 5, (int)buttonSprite.Width, (int)buttonSprite.Height), () => { level.isPauseOpen = false; });
            cont.CenterButton();

            options = new UIButton(optionsText, buttonSprite, new Rectangle(Core.WIDTH / 2, (int)(2.5f * Core.HEIGHT / 5), (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.ChangeScene(new OptionsScene()); });
            options.CenterButton();

            menu = new UIButton(menuText, buttonSprite, new Rectangle(Core.WIDTH / 2, 3 * Core.HEIGHT / 5, (int)buttonSprite.Width, (int)buttonSprite.Height), () => { Core.ChangeScene(new TitleScene()); });
            menu.CenterButton();
        }

        public override void LoadContent()
        {
            _normalFont = Core.Content.Load<SpriteFont>("fonts/InstructionFont");

            _titleFont = Core.Content.Load<SpriteFont>("fonts/TitleFont");

            TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, "sprites/hud-definition.xml");

            buttonSprite = atlas.CreateSprite("button");
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
            cont.Update(gameTime);
            options.Update(gameTime);
            menu.Update(gameTime);
        }
    }
}
