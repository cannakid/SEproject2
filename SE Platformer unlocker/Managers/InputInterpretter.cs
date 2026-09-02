

using Library;
using Library.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SE_Platformer_unlocker.Managers
{
    public class InputInterpretter
    {
        public InputInterpretter(InputManager manager) 
        {
            this.manager = manager;

            keyBinds.Add(Actions.Movement1, Keys.A);
            keyBinds.Add(Actions.Movement1, Keys.D);

            buttonBinds.Add(Actions.Movement1, Buttons.LeftThumbstickLeft);
            buttonBinds.Add(Actions.Movement1, Buttons.LeftThumbstickRight);

            keyBinds.Add(Actions.Gravity1, Keys.W);
            keyBinds.Add(Actions.Gravity2, Keys.S);

            buttonBinds.Add(Actions.Movement1, Buttons.A);
            buttonBinds.Add(Actions.Movement1, Buttons.B);

            keyBinds.Add(Actions.Attack1, Keys.E);
            keyBinds.Add(Actions.Attack2, Keys.R);

            buttonBinds.Add(Actions.Movement1, Buttons.X);
            buttonBinds.Add(Actions.Movement1, Buttons.Y);

            keyBinds.Add(Actions.Movement1, Keys.LeftShift);
            keyBinds.Add(Actions.Movement1, Keys.LeftControl);

            buttonBinds.Add(Actions.Movement1, Buttons.LeftTrigger);
            buttonBinds.Add(Actions.Movement1, Buttons.RightTrigger);

            FindGamePad();
        }
        

        private InputManager manager;

        private Dictionary<Actions, Keys> keyBinds;

        private Dictionary<Actions, Buttons> buttonBinds;

        private bool isGamePadConnected;

        private PlayerIndex connectedGamePad;

        private TimeSpan searchTimer;

        private static TimeSpan searchDelay = TimeSpan.FromSeconds(1);


        public bool wasActionJustPerformed(Actions action)
        {
            // use gamepad keybind if connected else use keyboard keybind
            if (isGamePadConnected)
                return manager.GamePads[(int)connectedGamePad].WasButtonJustPressed(buttonBinds[action]);
            return manager.Keyboard.WasKeyJustPressed(keyBinds[action]);
        }

        public bool isActionBeingPerformed(Actions action)
        {
            // use gamepad keybind if connected else use keyboard keybind
            if (isGamePadConnected)
                return manager.GamePads[(int)connectedGamePad].IsButtonDown(buttonBinds[action]);
            return manager.Keyboard.IsKeyDown(keyBinds[action]);
        }

        public void Update(GameTime gameTime)
        {
            searchTimer += gameTime.ElapsedGameTime;
            if (searchTimer > searchDelay)
            {
                searchTimer -= searchDelay;
                // only find gamepad if it wasn't connected or has just lost connection
                if (!isGamePadConnected || !manager.GamePads[(int)connectedGamePad].IsConnected)
                {
                    FindGamePad();
                }
            }
            
        }

        private void FindGamePad()
        {
            foreach (GamePadInfo gamePad in manager.GamePads)
            {
                if (gamePad.IsConnected)
                {
                    connectedGamePad = gamePad.PlayerIndex;
                    isGamePadConnected = true;
                    break;
                }
            }
            isGamePadConnected = false;
        }
    }
}

