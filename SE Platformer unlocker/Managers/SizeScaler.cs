using Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Managers
{
    public class SizeScaler
    {
        private const int DEV_WIDTH = 2560;
        private const int DEV_HEIGHT = 1440;

        private Rectangle screen = Core.GraphicsDevice.PresentationParameters.Bounds;

        public int Width => screen.Width;
        public int Height => screen.Height;

        public int CenterWidth => Width / 2;

        public int CenterHeight => Height / 2;

        public int ScaledWidth(int width)
        {
            return (int)(width * ((float)Width / DEV_WIDTH));
        }

        public int ScaledHeight(int height)
        {
            return (int)(height * ((float)Height / DEV_HEIGHT));
        }
    }
}
