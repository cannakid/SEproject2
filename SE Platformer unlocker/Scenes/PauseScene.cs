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
    internal class PauseScene : BaseScene
    {
        public PauseScene(LevelScene level)
        {
            this.level = level;
        }

        private LevelScene level;

        private SpriteFont _titleFont;

        private SpriteFont _normalFont;

        

        private Sprite buttonSprite;

        public override void Initialize()
        {
            base.Initialize();

            UIText paused = Game1.UIFactory.CreateText("Paused", _titleFont, 1280, 200);
            Add(paused);
            Add(Game1.UIFactory.CreateShadow(paused));
            
            buttonSprite.CenterOrigin();
            buttonSprite.Scale = new Vector2(6f, 4f);

            Add(Game1.UIFactory.CreateButton("Continue", _normalFont, buttonSprite, 1280, 400, 600, 200, () => { level.isPauseOpen = false; }));

            Add(Game1.UIFactory.CreateButton("Options", _normalFont, buttonSprite, 1280, 600, 600, 200, () => { Core.ChangeScene(new OptionsScene()); }));

            Add(Game1.UIFactory.CreateButton("Back to Menu", _normalFont, buttonSprite, 1280, 800, 600, 200, () => { Core.ChangeScene(new TitleScene()); }));

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

            base.Draw(gameTime);

            Core.SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
