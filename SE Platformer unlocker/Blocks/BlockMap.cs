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
                if (_tiles[i] != 15)
                {
                    int col = i % Columns;
                    int row = i / Columns;
                    blocks.Add(new Block((int)(col * TileWidth), (int)(row * TileHeight), (int)TileWidth, (int)TileHeight));
                }
            }
            return blocks;
        }
    }
}
