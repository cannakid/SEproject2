using Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SE_Platformer_unlocker.Factories;
using SE_Platformer_unlocker.Managers;
using SE_Platformer_unlocker.Scenes;
using SE_Platformer_unlocker.UI;
using System.Collections.Generic;

namespace SE_Platformer_unlocker
{
    public class Game1 : Core
    {
        public static InputInterpretter Interpretter { get; private set; }

        public static SizeScaler Scaler { get; private set; }

        public static UIFactory UIFactory { get; private set; }
        

        private Song _themeSong;

        public Game1() : base("Move Locked", true)
        {
            
        }

        protected override void Initialize()
        {
            base.Initialize();

            Interpretter = new InputInterpretter(Input);
            Scaler = new SizeScaler();

            UIFactory = new UIFactory();

            Audio.PlaySong(_themeSong);

            ChangeScene(new TitleScene());
        }

        protected override void LoadContent()
        {
            // Load the background theme music
            _themeSong = Content.Load<Song>("audio/time_for_adventure");
        }
        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Interpretter.Update(gameTime);
        }
    }
}
