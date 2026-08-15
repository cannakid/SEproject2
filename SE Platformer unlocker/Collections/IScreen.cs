using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;
using System.Collections.Generic;


namespace SE_Platformer_unlocker.Collections
{
    internal interface IScreen<T>
    {
        public List<T> Objects { get; set; }

        public bool Loaded { get; set; }

        public void Load(Game game);

        public void Draw(SpriteBatch batch);

        public void Update();
    }
}
