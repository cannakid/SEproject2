using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Input
{
    public class KeyboardInfo
    {
        public KeyboardInfo()
        {
            PreviousState = new KeyboardState();
            CurrentState = Keyboard.GetState();
        }


        public KeyboardState PreviousState { get; private set; }

        public KeyboardState CurrentState { get; private set; }


        public void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return CurrentState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return CurrentState.IsKeyUp(key);
        }

        public bool WasKeyJustPressed(Keys key)
        {
            return CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);
        }

        public bool WasKeyJustReleased(Keys key)
        {
            return CurrentState.IsKeyUp(key) && PreviousState.IsKeyDown(key);
        }
    }
}
