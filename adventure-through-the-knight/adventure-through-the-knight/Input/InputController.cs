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
        private KeyboardState InputKeyboard;

        //TODO: Add state for xbox 360 controller

        /// <summary>
        /// Initializes a new instance of the <see cref="RPGGame.InputController"/> class.
        /// </summary>
        public InputController()
        {
            InputKeyboard = Keyboard.GetState();

            Pause = InputKeyboard.IsKeyDown(Keys.Escape) ? true : false;
            Up = InputKeyboard.IsKeyDown(Keys.W) ? true : false;
            Down = InputKeyboard.IsKeyDown(Keys.S) ? true : false;
            Left = InputKeyboard.IsKeyDown(Keys.A) ? true : false;
            Right = InputKeyboard.IsKeyDown(Keys.D) ? true : false;
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
