using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Input
{
    public class InputManager
    {
        public InputManager()
        {
            Keyboard = new KeyboardInfo();
            Mouse = new MouseInfo();

            GamePads = new GamePadInfo[4];
            for (int i = 0; i < 4; i++)
            {
                GamePads[i] = new GamePadInfo((PlayerIndex)i);
            }
        }


        public KeyboardInfo Keyboard { get; private set; }

        public MouseInfo Mouse { get; private set; }

        public GamePadInfo[] GamePads { get; private set; }


        public void Update(GameTime gameTime)
        {
            Keyboard.Update();
            Mouse.Update();

            for (int i = 0; i < 4; i++)
            {
                GamePads[i].Update(gameTime);
            }
        }
    }
}
