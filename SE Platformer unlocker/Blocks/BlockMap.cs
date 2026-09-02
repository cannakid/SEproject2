using Library.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Blocks
{
    internal class BlockMap : TileMap
    {
        public BlockMap(TileMap map) : base(map)
        {

        }

        public List<Block> CreateBlocks()
        {
            List<Block> blocks = new List<Block>();
            for (int i = 0; i < _tiles.Length; i++)
            {
                int col = i % Columns;
                int row = i / Columns;
                if (_tiles[i] < 8)
                {
                    blocks.Add(new SolidBlock((int)(col * TileWidth), (int)(row * TileHeight), (int)TileWidth, (int)TileHeight));
                }
                else if (_tiles[i] == 8)
                {
                    blocks.Add(new Spike((int)(col * TileWidth), (int)(row * TileHeight + 2), (int)TileWidth, (int)TileHeight - 2));
                }
            }
            return blocks;
        }
    }
}
