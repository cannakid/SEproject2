using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Base;
using SE_Platformer_unlocker.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Blocks
{
    internal class Spike : Sprite,  IInteractable
    {
        public Spike(Texture2D texture, Point pos, Point size)
        {
            //this.textureFile = textureFile;
            Texture = texture;
            TextureRect = new Rectangle(pos, size);
        }
        public Texture2D Texture { get; set; }

        public Rectangle TextureRect { get; protected set; }

        public Rectangle HitBox => TextureRect;

        public void Draw(SpriteBatch batch)
        {
            if (Texture != null)
            {
                batch.Draw(Texture, TextureRect, Color.White);
            }
        }

        public InteractionType Interact(InteractionDirection direction)
        {
            if (direction == InteractionDirection.TOP)
            {
                return InteractionType.HIT;
            }
            return InteractionType.NONE;
        }
    }
}
