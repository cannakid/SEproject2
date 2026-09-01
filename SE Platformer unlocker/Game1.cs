using Library;
using Library.Graphics;
using Library.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.Blocks;
using SE_Platformer_unlocker.Entities;
using SE_Platformer_unlocker.Managers;
using SE_Platformer_unlocker.Scenes;
using SE_Platformer_unlocker.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SE_Platformer_unlocker
{
    public class Game1 : Core
    {
        public static InputInterpretter Interpretter { get; private set; }

        private Texture2D brown;
        private Texture2D yellow;
        private Texture2D spike;

        public List<IGameObject> LoadedObjects = new List<IGameObject>();
        public List<IUiElement> uiElements = new List<IUiElement>();
        

        private Song _themeSong;

        public Game1() : base("Move Locked", false)
        {
            Interpretter = new InputInterpretter(Input);
        }

        protected override void Initialize()
        {
            base.Initialize();

            LoadedObjects.Add(new Champion(yellow, new Point(0, 1200), new Point(50, 50)));
            LoadedObjects.Add(new Block(brown, new Point(0, 1300), new Point(1000, 50)));
            LoadedObjects.Add(new Block(brown, new Point(200, 1225), new Point(50, 75)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(800, 1100), new Point(500, 50)));
            LoadedObjects.Add(new Block(brown, new Point(1300, 700), new Point(200, 25)));
            LoadedObjects.Add(new Block(brown, new Point(1200, 900), new Point(100, 50)));
            LoadedObjects.Add(new Spike(spike, new Point(350, 1150), new Point(50, 50)));
            /*LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));
            LoadedObjects.Add(new Block(brown, new Point(500, 1250), new Point(100, 50)));*/

            Rectangle rect = new Rectangle(new Point(600, 1200), new Point(120, 20));

            LoadedObjects.Add(new MovingPlatform(brown, rect, new Rectangle(rect.Location, rect.Size), new Point(600, 1200), new Point(1000, 1200), -1, 2));

            //uiElements.Add(new UIText("Hello world", new Rectangle(50, 50, 500, 100), font));

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
        /*
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

       

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        */
    }
}
