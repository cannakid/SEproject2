using Library.Scenes;
using Microsoft.Xna.Framework;
using SE_Platformer_unlocker.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Scenes
{
    public abstract class LevelScene : Scene
    {
        protected CollisionHandler collisionHandler;

        public bool isPauseOpen { get; set; }
        protected Scene pause;


        public override void Initialize()
        {
            base.Initialize();

            collisionHandler = new CollisionHandler(this);

            pause = new PauseScene(this);
            pause.Initialize();
        }
    }
}
