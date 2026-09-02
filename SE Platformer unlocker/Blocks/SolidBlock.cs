using SE_Platformer_unlocker.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Blocks
{
    public class SolidBlock : Block
    {
        public SolidBlock(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }

        public override InteractionType Interact(InteractionDirection direction)
        {
            return InteractionType.BLOCK;
        }
    }
}
