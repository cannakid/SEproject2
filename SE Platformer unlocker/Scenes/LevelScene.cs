using Library.Scenes;
using Microsoft.Xna.Framework;
using SE_Platformer_unlocker.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Scenes
{
    public abstract class LevelScene : Scene
    {
        public bool isPauseOpen { get; set; }
        protected Scene pause;

        public List<IInteractable> Interactables { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            pause = new PauseScene(this);
            pause.Initialize();

            Interactables = new List<IInteractable>();
        }
    }
}
