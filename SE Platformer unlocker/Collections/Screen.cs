using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Collections
{
    internal class Screen : IScreen
    {
        public List<IGameObject> Objects { get; set; }
        public bool Loaded { get; set; }

        public void Load(Game game)
        {
            if (Loaded)
            {
                return;
            }
            Texture2D brown = game.Content.Load<Texture2D>("brown");


        }

        public void Draw(SpriteBatch batch)
        {
            foreach (IGameObject go in Objects)
            {
                if (go is IVisible vis)
                {
                    vis.Draw(batch);
                }
            }
        }

        public void Update()
        {

        }
    }
}
