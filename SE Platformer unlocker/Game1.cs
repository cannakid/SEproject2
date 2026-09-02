using Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SE_Platformer_unlocker.Managers;
using SE_Platformer_unlocker.Scenes;
using SE_Platformer_unlocker.UI;
using System.Collections.Generic;

namespace SE_Platformer_unlocker
{
    public class Game1 : Core
    {
        public static InputInterpretter Interpretter { get; private set; }

        public static SizeManager SizeManager { get; private set; }

        private Texture2D brown;
        private Texture2D yellow;
        private Texture2D spike;

        
        public List<IUiElement> uiElements = new List<IUiElement>();
        

        private Song _themeSong;

        public Game1() : base("Move Locked", true)
        {
            
        }

        protected override void Initialize()
        {
            base.Initialize();

            Interpretter = new InputInterpretter(Input);
            //SizeManager = new SizeManager();

            Audio.PlaySong(_themeSong);

            ChangeScene(new TitleScene());
        }

        protected override void LoadContent()
        {
            brown = Content.Load<Texture2D>("Brown");
            yellow = Content.Load<Texture2D>("Yellow");
            spike = Content.Load<Texture2D>("Spike");

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
