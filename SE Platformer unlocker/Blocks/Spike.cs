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
    internal class Spike : Block
    {
        public Spike(int x, int y, int width, int height) : base(x, y, width, height)
        {
            
        }

        public override InteractionType Interact(InteractionDirection direction)
        {
            if (direction == InteractionDirection.TOP || direction == InteractionDirection.WITHIN)
            {
                return InteractionType.HIT;
            }
            return InteractionType.NONE;
        }
    }
}
