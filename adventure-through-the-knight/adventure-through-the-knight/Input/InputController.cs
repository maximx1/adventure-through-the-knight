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
        private bool Pause;
        private bool Up;
        private bool Down;
        private bool Left;
        private bool Right;

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

            InputKeyboard = InputType == InputDeviceType.KEYBOARD ? Keyboard.GetState() : new KeyboardState();

            //Add Gamepad code using the same format. Can't remember off the top of my head.
            //InputGamepad = InputType == InputDeviceType.GAMEPAD ? (new G
        }

        /// <summary>
        /// Gets the state of the user's input device.
        /// </summary>
        public void GetState()
        {
            if (InputType == InputDeviceType.KEYBOARD)
            {
                InputKeyboard = Keyboard.GetState();
                Pause = InputKeyboard.IsKeyDown(Keys.Escape) ? true : false;
                Up = InputKeyboard.IsKeyDown(Keys.W) ? true : false;
                Down = InputKeyboard.IsKeyDown(Keys.S) ? true : false;
                Left = InputKeyboard.IsKeyDown(Keys.A) ? true : false;
                Right = InputKeyboard.IsKeyDown(Keys.D) ? true : false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is PAUS.
        /// </summary>
        /// <value>
        /// <c>true</c> if PAUS; otherwise, <c>false</c>.
        /// </value>
        public bool PAUSE { get { return Pause; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is FORWAR.
        /// </summary>
        /// <value>
        /// <c>true</c> if FORWAR; otherwise, <c>false</c>.
        /// </value>
        public bool UP { get { return Up; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is BACKWARD.
        /// </summary>
        /// <value>
        /// <c>true</c> if BACKWARD; otherwise, <c>false</c>.
        /// </value>
        public bool DOWN { get { return Down; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is LEF.
        /// </summary>
        /// <value>
        /// <c>true</c> if LEF; otherwise, <c>false</c>.
        /// </value>
        public bool LEFT { get { return Left; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is RIGH.
        /// </summary>
        /// <value>
        /// <c>true</c> if RIGH; otherwise, <c>false</c>.
        /// </value>
        public bool RIGHT { get { return Right; } }
    }
}
