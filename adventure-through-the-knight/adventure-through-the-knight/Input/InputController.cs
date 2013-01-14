using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace adventure_through_the_knight.Input
{
    class InputController
    {
        //Active input functions
        private bool[] ButtonList;

        private float SpeedX;
        private float SpeedY;

        //Constant states for all input devices
        private InputDeviceType InputType;
        private KeyboardState InputKeyboard;
        private GamePadState InputGamepad;

        //Input Type register
        public enum InputDeviceType { KEYBOARD, GAMEPAD };

        //TODO: Add state for xbox 360 controller

        /// <summary>
        /// Initializes a new instance of the <see cref="RPGGame.InputController"/> class.
        /// </summary>
        /// <param name="inputType">The input device type. Defaults the keyboard if you send empty or null.</param>
        public InputController(InputDeviceType inputType)
        {
            InputType = inputType;

            ButtonList = new bool[] { false, false, false, false, false };

            //First Player keyboard
            InputKeyboard = InputType == InputDeviceType.KEYBOARD ? Keyboard.GetState() : new KeyboardState();
            //First Player gamepad
            InputGamepad = InputType == InputDeviceType.GAMEPAD ? GamePad.GetState(PlayerIndex.One) : new GamePadState();
            
        }

        /// <summary>
        /// Gets the state of the user's input device.
        /// </summary>
        public void GetState()
        {
            if (InputType == InputDeviceType.KEYBOARD)
            {
                InputKeyboard = Keyboard.GetState();
                ButtonList[0] = InputKeyboard.IsKeyDown(Keys.Escape) ? true : false;
                ButtonList[1] = InputKeyboard.IsKeyDown(Keys.W) ? true : false;
                ButtonList[2] = InputKeyboard.IsKeyDown(Keys.S) ? true : false;
                ButtonList[3] = InputKeyboard.IsKeyDown(Keys.A) ? true : false;
                ButtonList[4] = InputKeyboard.IsKeyDown(Keys.D) ? true : false;

                //Set the speed quantity to max for the keyboard
                SpeedY = ButtonList[1] ? 1 : 0;
                SpeedY = ButtonList[2] ? -1 : 0;
                SpeedX = ButtonList[3] ? 1 : 0;
                SpeedX = ButtonList[4] ? 1 : 0;
            }

            if (InputType == InputDeviceType.GAMEPAD)
            {
                InputGamepad = GamePad.GetState(PlayerIndex.One);

                ButtonList[0] = InputGamepad.Buttons.Start == ButtonState.Pressed ? true : false;
                if (InputGamepad.ThumbSticks.Left.X > .1f)
                {
                    ButtonList[1] = true;
                    SpeedX = InputGamepad.ThumbSticks.Left.X;
                }
                else if (InputGamepad.ThumbSticks.Left.X < -.1f)
                {
                    ButtonList[2] = true;
                    SpeedX = InputGamepad.ThumbSticks.Left.X;
                }
                else if (InputGamepad.ThumbSticks.Left.X < -.1f)
                {
                    ButtonList[3] = true;
                    SpeedY = InputGamepad.ThumbSticks.Left.Y;
                }
                else if (InputGamepad.ThumbSticks.Left.X > .1f)
                {
                    ButtonList[4] = true;
                    SpeedX = InputGamepad.ThumbSticks.Left.Y;
                }
            }
        }

        /// <summary>
        /// Detects if a specific key is pressed.
        /// </summary>
        /// <param name="key">Key to be detected.</param>
        /// <returns>true if the key is pressed.</returns>
        public bool IsPressed(G_key.G_KEY key)
        {
            return ButtonList[(int)key];
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is PAUS.
        /// </summary>
        /// <value>
        /// <c>true</c> if PAUS; otherwise, <c>false</c>.
        /// </value>
        public bool PAUSE { get { return ButtonList[0]; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is FORWAR.
        /// </summary>
        /// <value>
        /// <c>true</c> if FORWAR; otherwise, <c>false</c>.
        /// </value>
        public bool UP { get { return ButtonList[1]; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is BACKWARD.
        /// </summary>
        /// <value>
        /// <c>true</c> if BACKWARD; otherwise, <c>false</c>.
        /// </value>
        public bool DOWN { get { return ButtonList[2]; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is LEF.
        /// </summary>
        /// <value>
        /// <c>true</c> if LEF; otherwise, <c>false</c>.
        /// </value>
        public bool LEFT { get { return ButtonList[3]; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is RIGH.
        /// </summary>
        /// <value>
        /// <c>true</c> if RIGH; otherwise, <c>false</c>.
        /// </value>
        public bool RIGHT { get { return ButtonList[4]; } }

        /// <summary>
        /// Gets the degree of speed for the x direction
        /// </summary>
        public float SPEEDX { get { return SpeedX; } }

        /// <summary>
        /// Gets the degree of speed for the y direction
        /// </summary>
        public float SPEEDY { get { return SpeedY; } }
    }
}
