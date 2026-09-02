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
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Scenes
{
    internal class GameOverScene : BaseScene
    {
        public GameOverScene(LevelScene scene)
        {
            this.scene = scene;
        }

        private LevelScene scene;

        private SpriteFont _titleFont;

        private SpriteFont _normalFont;

        private Texture2D _backgroundTexture;

        private Sprite _background;

        private Text titleTop;
        private Text titleBottom;
        private Text titleTopShadow;
        private Text titleBottomShadow;

        private UIButton retry;
        private UIButton menu;
        private UIButton quit;

        private Sprite buttonSprite;

        public override void Initialize()
        {
            base.Initialize();

            UIText game = Game1.UIFactory.CreateText("Game", _titleFont, 1280, 150);
            Add(game);
            Add(Game1.UIFactory.CreateShadow(game));

            UIText over = Game1.UIFactory.CreateText("Over", _titleFont, 1280, 300);
            Add(over);
            Add(Game1.UIFactory.CreateShadow(over));

            buttonSprite.CenterOrigin();
            buttonSprite.Scale = new Vector2(6f, 4f);

            Add(Game1.UIFactory.CreateButton("Retry", _normalFont, buttonSprite, 1280, 500, 600, 200, () => { Core.ChangeScene(scene); }));

            Add(Game1.UIFactory.CreateButton("Menu", _normalFont, buttonSprite, 1280, 700, 600, 200, () => { Core.ChangeScene(new TitleScene()); }));

            Add(Game1.UIFactory.CreateButton("Quit", _normalFont, buttonSprite, 1280, 900, 600, 200, () => { Core.Instance.Quit(); }));
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
            /*Core.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
            _background.Draw(Core.SpriteBatch, Vector2.Zero);
            Core.SpriteBatch.End();*/


            // rest
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
